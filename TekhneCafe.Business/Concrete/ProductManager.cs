using Microsoft.EntityFrameworkCore;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.Exceptions.Attribute;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Concrete.EntityFramework;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }


        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productDal.GetAll().ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            Product product = await _productDal.GetByIdAsync(Guid.Parse(id));
            if (product is null)
                throw new AttributeNotFoundException();

            return product;
        }
    }
}
