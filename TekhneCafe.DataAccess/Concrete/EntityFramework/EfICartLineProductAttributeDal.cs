using TekhneCafe.Core.DataAccess.Concrete.EntityFramework;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Concrete.EntityFramework.Context;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.Concrete.EntityFramework
{
    public class EfICartLineProductAttributeDal : EfEntityRepositoryBase<CartLineProductAttribute, EfTekhneCafeContext>, ICartLineProductAttributeDal
    {
        public EfICartLineProductAttributeDal()
        {

        }

        public async Task<bool> CartLineProductAttributeExistsAsync(string id)
        {
            return await base.GetByIdAsync(Guid.Parse(id)) != null
                ? true
                : false;
        }
    }
}
