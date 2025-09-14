using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppUser : BaseEntityWithMandatoryFields
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? RoleId { get; set; }

        public string UserName { get; set; } = null!;
        public string NormalizedUserName { get; set; } = null!;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string Email { get; set; } = null!;
        public string NormalizedEmail { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string SecurityStamp { get; set; } = null!;
        public bool IsExternal { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool IsActive { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public bool ShouldChangePasswordOnNextLogin { get; set; }
        public int EntityVersion { get; set; }
        public DateTimeOffset? LastPasswordChangeTime { get; set; }
    }
}
