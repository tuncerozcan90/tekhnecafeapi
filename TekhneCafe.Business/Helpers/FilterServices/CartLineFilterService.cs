using TekhneCafe.Core.Filters;
using TekhneCafe.Core.Filters.CartLine;
using TekhneCafe.Core.ResponseHeaders;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Helpers.FilterServices
{
    public class CartLineFilterService
    {
        public CartLineResponseFilter<List<CartLine>> FilterRoles(IQueryable<CartLine> cartLines, CartLineRequestFilter filters)
        {
            var filteredCartLines = cartLines.Skip(filters.Page * filters.Size).Take(filters.Size).ToList();
            Metadata metadata = new()
            {
                CurrentPage = filters.Page,
                PageSize = filters.Size,
                TotalEntities = cartLines.Count(),
                TotalPages = cartLines.Count() / filters.Size + 1,
            };
            var header = new CustomHeaders().AddPaginationHeader(metadata);

            return new()
            {
                ResponseValue = filteredCartLines,
                Headers = header
            };
        }
    }
}
