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
