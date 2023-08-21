using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.Exceptions.Attribute;
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

        // Diğer action metodları burada olabilir...

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
               Product product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
          
        }
    }
}
