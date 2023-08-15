using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.Product;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IValidator<ProductAddDto> _validator;
        private readonly IAppUserService _userService;

        public TestController(IValidator<ProductAddDto> validator, IAppUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _validator = validator;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Index()
        {

            var users = _userService.GetUserList();
            return Ok(users);
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
