using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface IAppUserService
    {
        Task<AppUser> GetUserByLdapIdAsync(string id);
        Task CreateUserAsync(AppUser roleDto);
    }
}
