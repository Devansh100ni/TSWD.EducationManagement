namespace TSWD.EducationManagement.Domain.DTOs.SchoolAcademicSettings
{
    public class AcademicSettingsDto
    {
        public List<GradePolicyDto> GradePolicies { get; set; } = new();
        public List<SchoolPromotionRuleDto> SchoolPromotionRules { get; set; } = new();
    }

    public class GradePolicyDto
    {
        public Guid Id { get; set; }
        public int FromPercent { get; set; }
        public int ToPercent { get; set; }
        public string Grade { get; set; } = null!;
    }

    public class SchoolPromotionRuleDto
    {
        public Guid PromotionId { get; set; }
        public Guid RuleId { get; set; }
        public Guid FilterId { get; set; }
        public string RuleName { get; set; } = null!;
        public string FilterKey { get; set; } = null!;
        public string FilterValue { get; set; } = null!;
        public string PromotionValue { get; set; } = null!;
        public bool IsPercent { get; set; }
    }


}
