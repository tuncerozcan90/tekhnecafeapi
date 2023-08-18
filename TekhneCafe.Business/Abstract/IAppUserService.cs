using TekhneCafe.Core.DTOs.AppUser;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface IAppUserService
    {
        List<AppUserListDto> GetUserList();
        Task<AppUser> GetUserByLdapIdAsync(string id);
        Task CreateUserAsync(AppUser roleDto);
        Task<AppUser> GetUserByIdAsync(string id);
        Task UpdateUserAsync(AppUser user);
    }
}
