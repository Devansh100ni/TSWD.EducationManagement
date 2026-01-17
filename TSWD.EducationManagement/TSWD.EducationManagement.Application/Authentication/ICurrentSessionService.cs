using TSWD.EducationManagement.Domain.DTOs.Auth;

namespace TSWD.EducationManagement.Application.Authentication
{
    public interface ICurrentSessionService
    {
        Task<CurrentUserDto> GetCurrentUserAsync(CancellationToken cancellationToken = default);
        Task<CurrentTenantDto> GetCurrentTenantAsync(CancellationToken cancellationToken = default);
    }
}
