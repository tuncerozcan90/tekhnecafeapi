using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Extensions;
using TekhneCafe.Core.DTOs.Order;
using TekhneCafe.Core.Exceptions;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAppUserDal _userDal;
        private readonly IOrderHistoryService _orderHistoryService;
        private readonly IOrderProductService _orderProductService;

        public OrderManager(IOrderDal orderDal, IMapper mapper, IHttpContextAccessor httpContext, IAppUserDal userDal,
                IOrderHistoryService orderHistoryService, IOrderProductService orderProductService)
        {
            _orderDal = orderDal;
            _mapper = mapper;
            _httpContext = httpContext;
            _userDal = userDal;
            _orderHistoryService = orderHistoryService;
            _orderProductService = orderProductService;
        }

        public async Task CreateOrderAsync(OrderAddDto orderAddDto)
        {
            Order order = _mapper.Map<Order>(orderAddDto);
            var validOrder = await GetValidOrderAsync(order);
            if (validOrder is null)
                throw new BadRequestException("Geçersiz sepet!");
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
            float orderTotalPrice = GetOrderTotalPrice(order);
            order.AppUserId = Guid.Parse(_httpContext.HttpContext.User.ActiveUserId());
            order.TotalPrice = orderTotalPrice;
            order.OrderHistories = new List<OrderHistory>()
            {
                _orderHistoryService.CreateOrderHistory(OrderStatus.Ordered),
            };
            order.TransactionHistories = new List<TransactionHistory>()
            {
                new TransactionHistory()
                {
                    Amount = orderTotalPrice,
                    TransactionType = TransactionType.Order,
                    Description = $"Sipariş verildi.",
                    AppUserId = order.AppUserId,
                }
            };
            await _orderDal.AddAsync(order);
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

        public async Task ConfirmOrderAsync(string id)
        {
            var order = await _orderDal.GetByIdAsync(Guid.Parse(id));
            if (order is null)
                throw new BadRequestException("Sipariş bulunamadı.");
            order.OrderStatus = OrderStatus.OrderConfirmed;
            order.OrderHistories = new List<OrderHistory>()
            {
                _orderHistoryService.CreateOrderHistory(OrderStatus.OrderConfirmed)
            };
            //todo Hesaptan para düşürülmeli
            await _orderDal.UpdateAsync(order);
        }

        public async Task<OrderListDto> GetOrderByIdAsync(string id)
        {
            Order order = await _orderDal.GetOrderIncludeProductsAsync(id);
            return _mapper.Map<OrderListDto>(order);
        }


        //public List<OrderListDto> GetOrders(OrderStatus orderStatus = OrderStatus.Ordered, OrderRequestFilter filters = null)
        //{
        //    var filteredResult = new OrderFilterService().FilterOrders(_orderDal.GetAll(_ => _.OrderStatus == orderStatus), filters);
        //    new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
        //    return _mapper.Map<List<OrderListDto>>(filteredResult.ResponseValue);
        //}
    }
}
