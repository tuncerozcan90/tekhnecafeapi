using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.Authentication;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Log into account
        /// </summary>
        /// <param name="user">Enter user email and password</param>
        /// <returns>jwt token information</returns>
        /// <response code="200">User successful login</response>
        /// <response code="400">Invalid credentials</response>
        /// <response code="404">User not found</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            var token = await _authService.Login(user);
            return Ok(token);
        }
    }
}
