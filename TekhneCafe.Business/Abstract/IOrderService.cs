using TekhneCafe.Core.DTOs.Order;

namespace TekhneCafe.Business.Abstract
{
    public interface IOrderService
    {
        //List<OrderListDto> GetOrders(OrderStatus orderStatus = OrderStatus.Ordered, OrderRequestFilter filters = null);
        Task<OrderListDto> GetOrderDetailById(string id);
        Task CreateOrderAsync(OrderAddDto orderAddDto);
        Task ConfirmOrderAsync(string id);
    }
}
