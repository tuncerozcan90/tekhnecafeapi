namespace TekhneCafe.Business.Helpers.FilterServices
{
    //public class AppRoleFilterService
    //{
    //    public AppRoleResponseFilter<List<AppRole>> FilterRoles(IQueryable<AppRole> roles, AppRoleRequestFilter filters)
    //    {
    //        var filteredRoles = roles.Skip(filters.Page * filters.Size).Take(filters.Size).ToList();
    //        Metadata metadata = new()
    //        {
    //            CurrentPage = filters.Page,
    //            PageSize = filters.Size,
    //            TotalEntities = roles.Count(),
    //            TotalPages = roles.Count() / filters.Size + 1,
    //        };
    //        var header = new CustomHeaders().AddPaginationHeader(metadata);

    //        return new()
    //        {
    //            ResponseValue = filteredRoles,
    //            Headers = header
    //        };
    //    }
    //}
}
