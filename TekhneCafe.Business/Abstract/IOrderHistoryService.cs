using TekhneCafe.Entity.Concrete;
using TekhneCafe.Entity.Enums;

namespace TekhneCafe.Business.Abstract
{
    public interface IOrderHistoryService
    {
        Task<OrderHistory> GetOrderHistoryByIdAsync(Guid orderHistoryId);
        Task<List<OrderHistory>> GetAllOrderHistoryAsync();
        Task CreateOrderHistoryAsync(OrderStatus orderStatus);
        OrderHistory CreateOrderHistory(OrderStatus orderStatus);
    }
}
