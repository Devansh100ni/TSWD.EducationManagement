namespace TSWD.EducationManagement.Domain.DTOs.Users
{
    public class CreateUpdateUsersDto
    {
        public Guid? Id { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? RoleId { get; set; }

        private string _userName = null!;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NormalizedUserName = value?.ToUpperInvariant() ?? string.Empty;
            }
        }
        public string NormalizedUserName { get; set; } = null!;
        public string? Name { get; set; }
        public string? Surname { get; set; }

        private string _email = null!;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                NormalizedEmail = value?.ToUpperInvariant() ?? string.Empty;
            }
        }

        public string NormalizedEmail { get; set; } = null!;
        public bool EmailConfirmed { get; set; } = false;
        public string? PasswordHash { get; set; }
        public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();
        public bool IsExternal { get; set; } = false;
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; } = false;
        public bool IsActive { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; } = false;
        public int AccessFailedCount { get; set; }
        public bool ShouldChangePasswordOnNextLogin { get; set; } = false;
        public int? EntityVersion { get; set; }
        public DateTimeOffset? LastPasswordChangeTime { get; set; }
    }
}
