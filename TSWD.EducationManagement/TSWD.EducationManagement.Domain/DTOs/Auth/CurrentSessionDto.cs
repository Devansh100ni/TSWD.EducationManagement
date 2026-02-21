using TSWD.EducationManagement.Domain.Entities;

namespace TSWD.EducationManagement.Domain.DTOs.Auth
{
    public class CurrentUserDto
    {
        public Guid UserId { get; set; }

        public AppUser User { get; set; } = null!;
    }

    public class CurrentTenantDto
    {
        public Guid TenantId { get; set; }

        public AppTenant Tenant { get; set; } = null!;
    }
}
