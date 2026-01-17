using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TSWD.EducationManagement.Application.Authentication;
using TSWD.EducationManagement.Controllers.Base;
using TSWD.EducationManagement.Domain.DTOs.Auth;

namespace TSWD.EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : CommonControllerBase
    {
        private readonly IAuthService authService;

        public AuthenticationController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginRequest request)
        {
            string email = request.Email ?? "No Email Provided";
            string dtoJson = JsonSerializer.Serialize(new
            {
                email,
                Password = "[REDACTED]"
            });

            return await ExecuteAsync(async () =>
            {
                var response = await authService.LoginAsync(request);
                return response;
            }, nameof(authService.LoginAsync), dtoJson);
        }
    }
}
