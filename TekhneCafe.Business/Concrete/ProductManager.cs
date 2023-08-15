using Microsoft.EntityFrameworkCore;
using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
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
            return await _productDal.GetByIdAsync(Guid.Parse(id));
        }
    }
}
