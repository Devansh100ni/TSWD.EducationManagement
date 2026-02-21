namespace TSWD.EducationManagement.Domain.DTOs.SchoolGeneralSettings
{
    public class UpdateSchoolGeneralSettingDto
    {
        public Guid Id { get; set; }

        public string SchoolName { get; set; } = null!;

        public string SchoolCode { get; set; } = null!;

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? LogoUrl { get; set; }
    }
}
