using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface IOrderHistoryService
    {
        Task<OrderHistory> GetOrderHistoryByIdAsync(Guid orderHistoryId);
        Task<List<OrderHistory>> GetAllOrderHistoryAsync();
        Task CreateOrderHistoryAsync(OrderHistory orderHistory);
        Task UpdateOrderHistoryAsync(OrderHistory orderHistory);
        Task DeleteOrderHistoryAsync(Guid orderHistoryId);
    }
}
