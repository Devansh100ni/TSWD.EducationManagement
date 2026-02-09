using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.Roles;
using TSWD.EducationManagement.Controllers.Base;
using TSWD.EducationManagement.Domain.DTOs.Role;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IRoleService roleService) : CommonControllerBase
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> Get([FromQuery] Guid? tenantId, PagedRequest input)
        {
            return await ExecuteAsync(async ct =>
            {
                var response = await roleService.Get(input, tenantId);
                return response;
            }, nameof(Get));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPermissions()
        {
            return await ExecuteAsync(async ct =>
            {
                var response = await roleService.GetPermissionGroups();
                return response;
            }, nameof(Get));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUpdate([FromBody] CreateUpdateRoleDto input)
        {
            return await ExecuteAsync(async ct =>
            {
                await roleService.CreateUpdate(input);
                return Ok();
            }, nameof(CreateUpdate));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoles([FromQuery] Guid tenantId)
        {
            return await ExecuteAsync(async ct =>
            {
                var resopnse = await roleService.Get(tenantId);
                return resopnse;
            }, nameof(GetRoles));
        }
    }
}
