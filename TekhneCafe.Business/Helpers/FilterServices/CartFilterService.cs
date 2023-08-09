using ECommerce.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.Filters.AppRole;
using TekhneCafe.Core.Filters.Cart;
using TekhneCafe.Core.ResponseHeaders;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Helpers.FilterServices
{
    public class CartFilterService
    {
        public CartResponseFilter<List<Cart>> FilterCarts(IQueryable<Cart> carts, CartRequestFilter filters)
        {
            var filteredCarts = carts.Skip(filters.Page * filters.Size).Take(filters.Size).ToList();
            Metadata metadata = new()
            {
                CurrentPage = filters.Page,
                PageSize = filters.Size,
                TotalEntities = carts.Count(),
                TotalPages = carts.Count() / filters.Size + 1,
            };
            var header = new CustomHeaders().AddPaginationHeader(metadata);

            return new()
            {
                ResponseValue = filteredCarts,
                Headers = header
            };
        }
    }
}
