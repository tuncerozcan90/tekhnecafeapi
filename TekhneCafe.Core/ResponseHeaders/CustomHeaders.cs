using ECommerce.Core.Filters;

namespace TekhneCafe.Core.ResponseHeaders
{
    public class CustomHeaders
    {
        public Dictionary<string, string> AddPaginationHeader(Metadata metadata)
            => metadata != null
            ? new Dictionary<string, string>(){
                { "X-Pagination", $"TotalPages:{metadata.TotalPages};CurrentPage:{metadata.CurrentPage};PageSize:{metadata.PageSize};TotalEntities:{metadata.TotalEntities};" }
            }
            : throw new Exception();
    }
}
