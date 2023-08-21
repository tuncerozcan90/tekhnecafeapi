using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Extensions;
using TekhneCafe.Core.Consts;
using TekhneCafe.Core.DTOs.Order;
using TekhneCafe.Core.Exceptions;
using TekhneCafe.Core.Exceptions.Order;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Helpers.Transaction;
using TekhneCafe.Entity.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;
        private readonly IOrderHistoryService _orderHistoryService;
        private readonly IWalletService _walletService;
        private readonly IOrderProductService _orderProductService;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly ITransactionManagement _transactionManagement;

        public OrderManager(IOrderDal orderDal, IMapper mapper, IHttpContextAccessor httpContext, IOrderHistoryService orderHistoryService, IWalletService walletService,
            IOrderProductService orderProductService, ITransactionHistoryService transactionHistoryService, ITransactionManagement transactionManagement)
        {
            _orderDal = orderDal;
            _mapper = mapper;
            _httpContext = httpContext.HttpContext;
            _orderHistoryService = orderHistoryService;
            _walletService = walletService;
            _orderProductService = orderProductService;
            _transactionHistoryService = transactionHistoryService;
            _transactionManagement = transactionManagement;
        }

        public async Task CreateOrderAsync(OrderAddDto orderAddDto)
        {
            Order order = _mapper.Map<Order>(orderAddDto);
            var validOrder = await GetValidOrderAsync(order);
            if (validOrder is null)
                throw new OrderBadRequestException("Unproccessable order!");
            await CreateOrderWhenValidAsync(order);
        }

        public async Task<Order> GetValidOrderAsync(Order order)
        {
            await _orderProductService.ValidateOrderProductsAsync(order);
            return order.OrderProducts.Count > 0
                ? order
                : null;
        }

        public async Task CreateOrderWhenValidAsync(Order order)
        {
            using (var result = await _transactionManagement.BeginTransactionAsync())
            {
                float orderTotalPrice = GetOrderTotalPrice(order);
                order.AppUserId = Guid.Parse(_httpContext.User.ActiveUserId());
                order.TotalPrice = orderTotalPrice;
                _transactionHistoryService.SetTransactionHistoryForOrder(order, orderTotalPrice, $"Sipariş verildi.", order.AppUserId);
                _orderHistoryService.SetOrderHistoryForOrder(order);
                try
                {
                    await _orderDal.AddAsync(order);
                    result.Commit();
                }
                catch (Exception ex)
                {
                    throw new InternalServerErrorException("Unexpected error occured! Please try again.");
                }
            }
        }

        public async Task ConfirmOrderAsync(string id)
        {
            var order = await _orderDal.GetByIdAsync(Guid.Parse(id));
            ThrowErrorIfOrderNotFound(order);
            order.OrderStatus = OrderStatus.OrderConfirmed;
            _orderHistoryService.SetOrderHistoryForOrder(order);
            using (var result = await _transactionManagement.BeginTransactionAsync())
            {
                try
                {
                    await _walletService.WithdrawFromWalletAsync(order.AppUserId, order.TotalPrice);
                    await _orderDal.UpdateAsync(order);
                    result.Commit();
                }
                catch (Exception)
                {
                    throw new InternalServerErrorException("Unexpected error occured! Please try again.");
                }
            }
        }

        public async Task<OrderListDto> GetOrderDetailById(string id)
        {
            Order order = await _orderDal.GetOrderIncludeProductsAsync(id);
            ThrowErrorIfOrderNotFound(order);
            if (!IsActiveUsersOrder(order) && !_httpContext.User.IsInAnyRoles(RoleConsts.CafeService, RoleConsts.CafeAdmin))
                throw new ForbiddenException();

            return _mapper.Map<OrderListDto>(order);
        }

        private float GetOrderTotalPrice(Order order)
        {
            float totalPrice = 0;
            foreach (var orderProduct in order.OrderProducts)
            {
                totalPrice += orderProduct.Price * orderProduct.Quantity;
                foreach (var orderProductAttribute in orderProduct.OrderProductAttributes)
                    totalPrice += orderProductAttribute.Price * orderProductAttribute.Quantity;
            }

            return totalPrice;
        }

        private bool IsActiveUsersOrder(Order order)
            => order.AppUserId == Guid.Parse(_httpContext.User.ActiveUserId());

        private void ThrowErrorIfOrderNotFound(Order order)
        {
            if (order is null)
                throw new OrderNotFoundException();
        }
    }
}
