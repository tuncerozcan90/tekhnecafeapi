using TekhneCafe.Core.DTOs.Product;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(string id);
        List<ProductListDto> GetAllProducts();
        Task CreateProductAsync(ProductAddDto productAddDto);
        Task DeleteProductAsync(string id);
        Task UpdateProductAsync(ProductUpdateDto productUpdateDto);
    }
}
