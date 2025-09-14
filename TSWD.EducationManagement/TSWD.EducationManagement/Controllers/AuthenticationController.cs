using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSWD.EducationManagement.Application.Authentication;
using TSWD.EducationManagement.Domain.DTOs.Auth;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthenticationController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginRequest request)
        {
            try
            {
                var user = await authService.LoginAsync(request);
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(401);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
