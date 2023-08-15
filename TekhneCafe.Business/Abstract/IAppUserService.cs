using TekhneCafe.Core.DTOs.AppUser;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface IAppUserService
    {
        List<AppUserListDto> GetUserList();
        Task<AppUser> GetUserByLdapIdAsync(string id);
        Task CreateUserAsync(AppUser roleDto);
    }
}
