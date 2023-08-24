using Microsoft.EntityFrameworkCore;
using TekhneCafe.Core.Filters;
using TekhneCafe.Core.Filters.Order;
using TekhneCafe.Core.ResponseHeaders;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Helpers.FilterServices
{
    public class OrderFilterService
    {
        public OrderResponseFilter<List<Order>> FilterOrders(IQueryable<Order> orders, OrderRequestFilter filters)
        {
            var filteredOrders = orders
                .Skip(filters.Page * filters.Size).Take(filters.Size)
                .ToList();
            Metadata metadata = new(filters.Page, filters.Size, orders.Count(), orders.Count() / filters.Size + 1);
            var header = new CustomHeaders().AddPaginationHeader(metadata);

            return new()
            {
                ResponseValue = filteredOrders,
                Headers = header
            };
        }
    }
}
