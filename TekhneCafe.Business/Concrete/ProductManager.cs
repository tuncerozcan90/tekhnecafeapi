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

        public async Task CreateOrderAsync(Product product)
        {
            await _productDal.AddAsync(product);
        }

        public async Task DeleteOrderAsync(Guid productId)
        {
            var product = await _productDal.GetByIdAsync(productId);
            if (product != null)
            {
                await _productDal.HardDeleteAsync(product);
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productDal.GetAll().ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid orderId)
        {
            return await _productDal.GetByIdAsync(orderId);
        }

        public async Task UpdateOrderAsync(Product product)
        {
            await _productDal.UpdateAsync(product);
        }
    }
}
