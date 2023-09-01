using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Api.ActionFilters;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Models;
using TekhneCafe.Core.Consts;
using TekhneCafe.Core.Filters.AppUser;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IAppUserService _userService;

        public UsersController(IAppUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.CafeAdmin}, {RoleConsts.CafeService}")]
        public IActionResult GetUsers([FromQuery] AppUserRequestFilter filters)
        {
            var users = _userService.GetUserList(filters);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RoleConsts.CafeAdmin}, {RoleConsts.CafeService}")]
        [TypeFilter(typeof(ModelValidationFilterAttribute), Arguments = new object[] { "id" })]
        public async Task<IActionResult> GetUsers([FromRoute] string id)
        {
            var users = await _userService.GetUserByIdAsync(id);
            return Ok(users);
        }

        [HttpPost("updatephone")]
        [TypeFilter(typeof(ModelValidationFilterAttribute), Arguments = new object[] { "phone" })]
        public async Task<IActionResult> UpdatePhone([FromBody] string phone)
        {
            await _userService.UpdateUserPhoneAsync(phone);
            return Ok();
        }

        [HttpPost("updateimage")]
        [TypeFilter(typeof(ModelValidationFilterAttribute), Arguments = new object[] { "request" })]
        public async Task<IActionResult> UpdateImage([FromForm] UploadImageRequest request)
        {

            return Ok();
        }
    }
}
