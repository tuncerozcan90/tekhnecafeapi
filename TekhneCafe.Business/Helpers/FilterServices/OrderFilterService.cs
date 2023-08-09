using ECommerce.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.Filters.AppRole;
using TekhneCafe.Core.Filters.Order;
using TekhneCafe.Core.ResponseHeaders;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Helpers.FilterServices
{
    public class OrderFilterService
    {
        public OrderResponseFilter<List<Order>> FilterOrders(IQueryable<Order> orders, OrderRequestFilter filters)
        {
            var filteredOrders = orders.Skip(filters.Page * filters.Size).Take(filters.Size).ToList();
            Metadata metadata = new()
            {
                CurrentPage = filters.Page,
                PageSize = filters.Size,
                TotalEntities = orders.Count(),
                TotalPages = orders.Count() / filters.Size + 1,
            };
            var header = new CustomHeaders().AddPaginationHeader(metadata);

            return new()
            {
                ResponseValue = filteredOrders,
                Headers = header
            };
        }
    }
}
