using Microsoft.EntityFrameworkCore;
using TekhneCafe.Core.DataAccess.Concrete.EntityFramework;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Concrete.EntityFramework.Context;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.Concrete.EntityFramework
{
    public class EfAppRoleDal : EfEntityRepositoryBase<AppRole, EfTekhneCafeContext>, IAppRoleDal
    {
        public async Task<AppRole> GetRoleByNameAsync(string name)
            => await _dbContext.AppRole.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
    }
}
