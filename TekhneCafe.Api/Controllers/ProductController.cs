using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.Product;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            Product product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProduct(ProductAddDto productAddDto)
        {
            await _productService.CreateProductAsync(productAddDto);
            return Ok("Product created successfully.");
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {

            await _productService.DeleteProductAsync(id);
            return (Ok("Product deleted successfully."));

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto productUpdateDto)
        {
            await _productService.UpdateProductAsync(productUpdateDto);
            return Ok("Product updated successfully.");
        }
    }
}
