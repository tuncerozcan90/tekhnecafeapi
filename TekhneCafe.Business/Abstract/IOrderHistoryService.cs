using TekhneCafe.Entity.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Business.Abstract
{
    public interface IOrderHistoryService
    {
        void SetOrderHistoryForOrder(Order order);
        Task<OrderHistory> GetOrderHistoryByIdAsync(Guid orderHistoryId);
        Task<List<OrderHistory>> GetAllOrderHistoryAsync();
        Task CreateOrderHistoryAsync(OrderStatus orderStatus);
        OrderHistory GetNewOrderHistory(OrderStatus orderStatus);
    }
}
