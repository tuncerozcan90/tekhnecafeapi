using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.Consts;
using TekhneCafe.Core.DTOs.Authentication;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IHttpContextAccessor _httpContext;

        public AuthenticationController(IAuthenticationService authService, IHttpContextAccessor httpContext)
        {
            _authService = authService;
            _httpContext = httpContext;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            var token = await _authService.Login(user.Email, user.Password);
            return Ok(token);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = $"{RoleConsts.CafeAdmin}")]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}
