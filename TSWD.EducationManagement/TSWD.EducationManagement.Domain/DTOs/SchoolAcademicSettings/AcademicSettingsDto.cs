namespace TSWD.EducationManagement.Domain.DTOs.SchoolAcademicSettings
{
    public class AcademicSettingsDto
    {
        public List<GradePolicyDto> GradePolicies { get; set; } = new();
    }

    public class GradePolicyDto
    {
        public Guid Id { get; set; }
        public int FromPercent { get; set; }
        public int ToPercent { get; set; }
        public string Grade { get; set; } = null!;
    }


}
