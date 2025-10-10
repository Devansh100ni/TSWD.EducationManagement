using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.Roles;
using TSWD.EducationManagement.Domain.DTOs.Role;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Get([FromQuery] Guid? tenantId, PagedRequest input)
        {
            var result = await roleService.Get(input, tenantId);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPermissions()
        {
            var result = await roleService.GetPermissionGroups();
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUpdate([FromBody] CreateUpdateRoleDto input)
        {
            await roleService.CreateUpdate(input);
            return Ok();
        }

    }
}
