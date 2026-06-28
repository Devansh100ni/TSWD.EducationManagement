namespace TSWD.EducationManagement.Domain.DTOs.SchoolAcademicSettings
{
    public class AcademicSettingsDto
    {
        public List<GradePolicyDto> GradePolicies { get; set; } = new();
        public List<SchoolPromotionRuleDto> SchoolPromotionRules { get; set; } = new();

        public List<SchoolClassesDto> SchoolClasses { get; set; } = new();
        public List<SchoolSectionDto> SchoolSections { get; set; } = new();
        public List<SchoolSubjectDto> SchoolSubjects { get; set; } = new();
        public List<SchoolRuleDto> SchoolRules { get; set; } = new();
        public List<SchoolFilterDto> SchoolFilters { get; set; } = new();

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

    public class SchoolClassesDto
    {
        public Guid Id { get; set; }
        public string ClassName { get; set; } = null!;
    }

    public class SchoolSubjectDto
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; } = null!;

        public Guid ClassId { get; set; }

        public string ClassName { get; set; } = null!;

        public bool IsOptional { get; set; } = false;
    }

    public class SchoolSectionDto
    {
        public Guid Id { get; set; }
        public string AppClassId { get; set; } = null!;
        public string SectionName { get; set; } = null!;
        public int SectionOrder { get; set; }
        public string Description { get; set; } = null!;
    }

    public class SchoolRuleDto
    {
        public Guid Id { get; set; }
        public string RuleName { get; set; } = null!;
    }

    public class SchoolFilterDto
    {
        public Guid Id { get; set; }
        public string FilterKey { get; set; } = null!;
        public string FilterValue { get; set; } = null!;
    }
}
