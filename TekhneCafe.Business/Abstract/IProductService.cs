using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(string id);
        Task<List<Product>> GetAllProductsAsync();
    }
}
