using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<List<Order>> GetAllOrdersAsync();
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Guid orderId);
    }
}
