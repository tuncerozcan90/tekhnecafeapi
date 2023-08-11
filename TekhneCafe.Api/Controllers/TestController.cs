using Microsoft.AspNetCore.Mvc;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public IActionResult Index()
        {


            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            return Ok();
        }
    }
}
