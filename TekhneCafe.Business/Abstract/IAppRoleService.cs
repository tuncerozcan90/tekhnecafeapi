using TekhneCafe.Core.DTOs.AppRole;
using TekhneCafe.Core.Filters.AppRole;

namespace TekhneCafe.Business.Abstract
{
    public interface IAppRoleService
    {
        List<AppRoleListDto> GetRoles(AppRoleRequestFilter filters = null);
        Task<AppRoleListDto> GetRoleByIdAsync(string id);
        Task CreateRoleAsync(AppRoleAddDto roleDto);
        Task RemoveRoleAsync(string id);
        Task UpdateRoleAsync(AppRoleUpdateDto roleDto);
    }
}
