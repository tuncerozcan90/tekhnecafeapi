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

        public async Task<List<OrderHistory>> GetAllOrderHistoryAsync()
        {
            return await _orderHistoryDal.GetAll().ToListAsync();
        }

        public void SetOrderHistoryForOrder(Order order, OrderStatus orderStatus)
            => order.OrderHistories = new List<OrderHistory>() { new OrderHistory(orderStatus, Guid.Parse(_httpContext.HttpContext.User.ActiveUserId())) };
    }
}
