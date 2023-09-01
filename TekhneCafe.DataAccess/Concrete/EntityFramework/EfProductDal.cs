using Microsoft.EntityFrameworkCore;
using TekhneCafe.Core.DataAccess.Concrete.EntityFramework;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Concrete.EntityFramework.Context;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, EfTekhneCafeContext>, IProductDal
    {
        public EfProductDal(EfTekhneCafeContext context) : base(context)
        {

        }
        public async Task<Product> GetProductIncludeAttributeAsync(string id)
            => await _dbContext.Product.Include(_ => _.ProductAttributes).ThenInclude(pa => pa.Attribute).FirstAsync(_ => _.Id == Guid.Parse(id));

    }
}
