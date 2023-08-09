using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(Guid orderId);
        Task<List<Product>> GetAllProductsAsync();
        Task CreateOrderAsync(Product product);
        Task UpdateOrderAsync(Product product);
        Task DeleteOrderAsync(Guid productId);
    }
}
