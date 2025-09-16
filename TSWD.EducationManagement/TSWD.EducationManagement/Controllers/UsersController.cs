using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.Users;
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
        public async Task<IActionResult> GetUsersAsync(PagedRequest input, Guid tenantId)
        {
            return Ok(await userService.GetAllUsersAsync(input, tenantId));
        }
    }
}
