using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TSWD.EducationManagement.Domain.DTOs.Auth;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;

namespace TSWD.EducationManagement.Application.Authentication
{
    public class CurrentSessionService : ICurrentSessionService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRepository<AppUser> userRepository;
        private readonly IRepository<AppTenant> tenantRepository;

        public CurrentSessionService(
            IHttpContextAccessor httpContextAccessor,
            IRepository<AppUser> userRepository,
            IRepository<AppTenant> tenantRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
            this.tenantRepository = tenantRepository;
        }

        public async Task<CurrentUserDto> GetCurrentUserAsync(
            CancellationToken cancellationToken = default)
        {
            var httpContext = httpContextAccessor.HttpContext
                ?? throw new UnauthorizedAccessException("HttpContext not available.");

            var userId = GetGuidClaim(httpContext.User, ClaimTypes.NameIdentifier);
            var tenantId = GetGuidClaim(httpContext.User, ClaimTypes.UserData);

            var user = await userRepository.GetByIdAsync(userId)
                ?? throw new KeyNotFoundException("User not found.");

            var tenant = await tenantRepository.GetByIdAsync(tenantId)
                ?? throw new KeyNotFoundException("Tenant not found.");

            return new CurrentUserDto
            {
                UserId = userId,
                User = user,
            };
        }

        private static Guid GetGuidClaim(ClaimsPrincipal user, string claimType)
        {
            var claimValue = user.FindFirst(claimType)?.Value;

            if (string.IsNullOrWhiteSpace(claimValue))
                throw new UnauthorizedAccessException($"Missing claim: {claimType}");

            return Guid.Parse(claimValue);
        }

        public async Task<CurrentTenantDto> GetCurrentTenantAsync(CancellationToken cancellationToken = default)
        {
            var httpContext = httpContextAccessor.HttpContext
                 ?? throw new UnauthorizedAccessException("HttpContext not available.");

            var tenantId = GetGuidClaim(httpContext.User, ClaimTypes.UserData);

            var tenant = await tenantRepository.GetByIdAsync(tenantId)
                ?? throw new KeyNotFoundException("Tenant not found.");

            return new CurrentTenantDto
            {
                TenantId = tenantId,
                Tenant = tenant,
            };
        }
    }
}
