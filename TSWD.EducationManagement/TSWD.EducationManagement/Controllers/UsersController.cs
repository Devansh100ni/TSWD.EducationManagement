using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.Users;
using TSWD.EducationManagement.Controllers.Base;
using TSWD.EducationManagement.Domain.DTOs.Users;
using TSWD.EducationManagement.Permissions;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Permission(PermissionConstent.Users.Default)]
    public class UsersController : CommonControllerBase
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
            return await ExecuteAsync(async ct =>
            {
                var respoonse = await userService.GetAllUsersAsync(input, tenantId);
                return respoonse;
            });
        }

        [HttpPost("[action]")]
        [Permission(PermissionConstent.Users.Create)]
        public async Task<IActionResult> CreateUpdateUserAsync([FromBody] CreateUpdateUsersDto input)
        {
            return await ExecuteAsync(async ct =>
            {
                var result = await userService.CreateUpdateUserAsync(input);
                return result;
            });
        }

        [HttpGet("[action]")]
        [Permission(PermissionConstent.Users.Default)]
        public async Task<IActionResult> GetUserByIdAsync([FromQuery] Guid id)
        {
            return await ExecuteAsync(async ct =>
            {
                var result = await userService.GetUserByIdAsync(id);
                return result;
            });
        }

    }
}
