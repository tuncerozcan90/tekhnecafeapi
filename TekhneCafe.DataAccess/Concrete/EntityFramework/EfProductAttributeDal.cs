using TekhneCafe.Core.DataAccess.Concrete.EntityFramework;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Concrete.EntityFramework.Context;

namespace TekhneCafe.DataAccess.Concrete.EntityFramework
{
    public class EfProductAttributeDal : EfEntityRepositoryBase<Entity.Concrete.Attribute, EfTekhneCafeContext>, IProductAttributeDal
    {
    }
}
