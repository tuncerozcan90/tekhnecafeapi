using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Core.DTOs.Product;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IValidator<ProductAddDto> _validator;

        public TestController(IValidator<ProductAddDto> validator)
        {
            _validator = validator;
        }
        [HttpGet]
        public IActionResult Index()
        {


            return Ok();
        }

        [HttpPost]
        public IActionResult GetAll(ProductAddDto product)
        {
            var res = _validator.Validate(product);
            var result = ModelState.IsValid;
            return Ok();
        }
    }
}
