using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Api.ActionFilters;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Consts;
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
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(IAppUserService userService, IImageService imageService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _imageService = imageService;
            _httpContextAccessor = httpContextAccessor;
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
        public async Task<IActionResult> UpdateImage()
        {
            string imagePath = await _userService.UpdateUserImageAsync(MinioBuckets.UserImage);
            return Ok(imagePath);
        }
    }
}
