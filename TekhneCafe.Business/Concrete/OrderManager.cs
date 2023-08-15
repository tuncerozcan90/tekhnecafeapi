using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Extensions;
using TekhneCafe.Business.Helpers.FilterServices;
using TekhneCafe.Business.Helpers.HeaderServices;
using TekhneCafe.Core.DTOs.Cart;
using TekhneCafe.Core.DTOs.Order;
using TekhneCafe.Core.Exceptions.Order;
using TekhneCafe.Core.Filters.Order;
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
        private readonly ICartService _cartService;

        public OrderManager(IOrderDal orderDal, IMapper mapper, IHttpContextAccessor httpContext, ICartService cartService)
        {
            _orderDal = orderDal;
            _mapper = mapper;
            _httpContext = httpContext;
            _cartService = cartService;
        }

        public List<OrderListDto> GetOrders(OrderRequestFilter filters = null)
        {
            var filteredResult = new OrderFilterService().FilterOrders(_orderDal.GetAll(), filters);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return _mapper.Map<List<OrderListDto>>(filteredResult.ResponseValue);
        }

        public async Task<OrderListDto> GetOrderByIdAsync(string id)
        {
            var order = await GetOrderById(id);
            return _mapper.Map<OrderListDto>(order);
        }

        public async Task CreateOrderAsync(CartAddDto cartAddDto)
        {
            Cart cart = _mapper.Map<Cart>(cartAddDto);
            cart = await _cartService.GetValidCart(cart);
            float totalPrice = _cartService.GetTotalPriceOfCart(cart);
            Guid activeUserId = Guid.Parse(_httpContext.HttpContext.User.ActiveUserId());
            Order order = new()
            {
                Cart = cart,
                TotalPrice = totalPrice,
                OrderHistory = new List<OrderHistory>()
                {
                    new OrderHistory()
                    {
                        ActiveAuthorizedId = activeUserId,
                        OrderStatus = OrderStatus.Ordered,
                    }
                },
                TransactionHistory = new List<TransactionHistory>()
                {
                    new()
                    {
                        AppUserId = activeUserId,
                        Amount = totalPrice,
                        Description = "Sipariş verildi.",
                        TransactionType = TransactionType.Order,
                    }
                },
                OrderStatus = OrderStatus.Ordered,
            };

            await _orderDal.AddAsync(order);
        }

        public async Task ConfirmOrderAsync(string orderId)
        {

        }


        private async Task<Order> GetOrderById(string id)
        {
            Order order = await _orderDal.GetByIdAsync(Guid.Parse(id));
            if (order is null)
                throw new OrderNotFoundException();

            return order;
        }
    }
}
