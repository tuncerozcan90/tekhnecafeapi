using TekhneCafe.Core.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.Abstract
{
    public interface IAppRoleDal : IEntityRepository<AppRole>
    {
        Task<AppRole> GetRoleByNameAsync(string name);
    }
}
