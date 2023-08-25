using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.Extensions;
using TekhneCafe.Entity.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Business.Concrete
{
    public class OrderHistoryManager : IOrderHistoryService
    {
        private readonly IHttpContextAccessor _httpContext;

        public OrderHistoryManager(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void SetOrderHistoryForOrder(Order order, OrderStatus orderStatus)
            => order.OrderHistories = new List<OrderHistory>() { new OrderHistory(orderStatus, Guid.Parse(_httpContext.HttpContext.User.ActiveUserId())) };
    }
}
