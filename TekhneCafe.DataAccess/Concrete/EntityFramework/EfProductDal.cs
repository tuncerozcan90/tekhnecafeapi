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
        public async Task<Product> GetProductIncludeAllAsync(string id)
            => await _dbContext.Product.Include(_ => _.ProductAttributes).ThenInclude(pa => pa.Attribute).Include(_ => _.Category).FirstAsync(_ => _.Id == Guid.Parse(id));

        public List<Product> GetProductsByCategory(string categoryId)
            => _dbContext.Product.Where(p => p.CategoryId == Guid.Parse(categoryId)).Include(_ => _.ProductAttributes).ThenInclude(_ => _.Attribute).Include(_ => _.Category).ToList();
    }
}
