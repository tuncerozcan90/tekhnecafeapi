using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.AppRole;
using TekhneCafe.Core.Filters.AppRole;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IAppRoleService _roleService;

        public RolesController(IAppRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetRoles([FromQuery] AppRoleRequestFilter filters)
        {
            var roles = _roleService.GetRoles(filters);
            return Ok(roles);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoles([FromQuery] string roleId)
        {
            var role = await _roleService.GetRoleByIdAsync(roleId);
            return Ok(role);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRole([FromBody] AppRoleAddDto role)
        {
            await _roleService.CreateRoleAsync(role);
            return Ok();
        }

        [HttpPost("update")]
        public IActionResult UpdateRole(AppRole role)
        {
            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            await _roleService.RemoveRoleAsync(id);
            return Ok();
        }
    }
}
