using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.Users;
using TSWD.EducationManagement.Domain.DTOs.Users;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> GetUsersAsync([FromQuery] Guid? tenantId, PagedRequest input)
        {
            return Ok(await userService.GetAllUsersAsync(input, tenantId));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUpdateUserAsync(CreateUpdateUsersDto input)
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
