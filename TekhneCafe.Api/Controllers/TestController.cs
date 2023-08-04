using Microsoft.AspNetCore.Mvc;
using TekhneCafe.DataAccess.Abstract;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IAppRoleDal _roleDal;

        public TestController(IAppRoleDal roleDal)
        {
            _roleDal = roleDal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _roleDal.AddAsync(new Entity.Concrete.AppRole
            {
                Id = Guid.NewGuid(),
                Name = "Test",
            });

            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            return Ok(_roleDal.GetAll());
        }
    }
}
