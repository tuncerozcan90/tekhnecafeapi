using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Extensions;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Business.Concrete
{
    public class OrderHistoryManager : IOrderHistoryService
    {
        private readonly IOrderHistoryDal _orderHistoryDal;
        private readonly IHttpContextAccessor _httpContext;

        public OrderHistoryManager(IOrderHistoryDal orderHistoryDal, IHttpContextAccessor httpContext)
        {
            _orderHistoryDal = orderHistoryDal;
            _httpContext = httpContext;
        }

        public async Task CreateOrderHistoryAsync(OrderStatus orderStatus)
        {
            var orderHistory = new OrderHistory()
            {
                AppUserId = Guid.Parse(_httpContext.HttpContext.User.ActiveUserId()),
                OrderStatus = orderStatus,
            };
            await _orderHistoryDal.AddAsync(orderHistory);
        }

        public async Task<List<OrderHistory>> GetAllOrderHistoryAsync()
        {
            return await _orderHistoryDal.GetAll().ToListAsync();
        }

        public async Task<OrderHistory> GetOrderHistoryByIdAsync(Guid orderHistoryId)
        {
            return await GetOrderHistoryByIdAsync(orderHistoryId);
        }

        public OrderHistory GetNewOrderHistory(OrderStatus orderStatus)
            => new()
            {
                AppUserId = Guid.Parse(_httpContext.HttpContext.User.ActiveUserId()),
                OrderStatus = orderStatus,
            };

        public void SetOrderHistoryForOrder(Order order)
            => order.OrderHistories = new List<OrderHistory>() { GetNewOrderHistory(OrderStatus.Ordered) };
    }
}
