using TekhneCafe.Core.DTOs.Product;

namespace TekhneCafe.Business.Abstract
{
    public interface IProductService
    {
        Task<ProductListDto> GetProductByIdAsync(string id);
        List<ProductListDto> GetAllProducts();
        Task CreateProductAsync(ProductAddDto productAddDto);
        Task DeleteProductAsync(string id);
        Task UpdateProductAsync(ProductUpdateDto productUpdateDto);
    }
}
