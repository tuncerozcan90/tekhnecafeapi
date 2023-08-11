using TekhneCafe.Core.DTOs.Order;
using TekhneCafe.Core.Filters.Order;

namespace TekhneCafe.Business.Abstract
{
    public interface IOrderService
    {
        List<OrderListDto> GetOrders(OrderRequestFilter filters = null);
        Task<OrderListDto> GetOrderByIdAsync(string id);
        Task CreateOrderAsync(OrderAddDto orderAddDto);
        Task RemoveOrderAsync(string id);
        Task UpdateOrderAsync(OrderUpdateDto orderUpdateDto);
    }
}
