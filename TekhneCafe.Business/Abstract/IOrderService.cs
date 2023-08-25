using TekhneCafe.Core.DTOs.Order;
using TekhneCafe.Core.Filters.Order;

namespace TekhneCafe.Business.Abstract
{
    public interface IOrderService
    {
        Task<OrderDetailDto> GetOrderDetailById(string id);
        Task CreateOrderAsync(OrderAddDto orderAddDto);
        Task ConfirmOrderAsync(string id);
        Task<List<OrderListDto>> GetOrdersAsync(OrderRequestFilter filters);
    }
}
