namespace TSWD.EducationManagement.Domain.DTOs.SchoolGeneralSettings
{
    public class SchoolGeneralSettingDto
    {
        public Guid Id { get; set; }

        public Guid? TenantId { get; set; }

        public string SchoolName { get; set; } = null!;

        public string SchoolCode { get; set; } = null!;

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? LogoUrl { get; set; }

        public string? TimeZone { get; set; }

        public string? DateFormat { get; set; }
    }
}
