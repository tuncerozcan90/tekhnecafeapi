using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.DTOs.AppRole;
using TekhneCafe.Core.DTOs.Order;
using TekhneCafe.Core.Filters.AppRole;
using TekhneCafe.Core.Filters.Order;
using TekhneCafe.Entity.Concrete;

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
