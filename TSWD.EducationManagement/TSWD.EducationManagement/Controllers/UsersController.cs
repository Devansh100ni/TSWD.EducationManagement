using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.Users;
using TSWD.EducationManagement.Domain.DTOs.Users;
using TSWD.EducationManagement.Permissions;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Permission(PermissionConstent.Users.Default)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Permission(PermissionConstent.Users.Default)]
        public async Task<IActionResult> GetUsersAsync([FromQuery] Guid? tenantId, PagedRequest input)
        {
            return Ok(await userService.GetAllUsersAsync(input, tenantId));
        }

        [HttpPost("[action]")]
        [Permission(PermissionConstent.Users.Create)]
        public async Task<IActionResult> CreateUpdateUserAsync([FromBody] CreateUpdateUsersDto input)
        {
            var result = await userService.CreateUpdateUserAsync(input);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
