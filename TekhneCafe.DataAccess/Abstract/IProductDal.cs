using TekhneCafe.Core.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        Task<Product> GetProductIncludeAttributeAsync(string id);
    }
}
