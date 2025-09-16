using TSWD.EducationManagement.Domain.DTOs.Auth;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Application.Authentication
{
    public interface IAuthService
    {
        Task<Result<string>> LoginAsync(LoginRequest request);
    }
}
