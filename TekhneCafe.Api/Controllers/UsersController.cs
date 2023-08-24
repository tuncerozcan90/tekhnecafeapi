using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
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
        public async Task<IActionResult> GetUsers([FromRoute] string id)
        {
            var users = await _userService.GetUserByIdAsync(id);
            return Ok(users);
        }

        [HttpPost("updatephone")]
        public async Task<IActionResult> UpdatePhone([FromBody] string phone)
        {
            await _userService.UpdateUserPhoneAsync(phone);
            return Ok();
        }
    }
}
