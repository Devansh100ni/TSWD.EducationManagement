using System;
using System.Collections.Generic;
using System.Text;

namespace TSWD.EducationManagement.Domain.DTOs.SchoolAcademicSettings
{
    public class CreateUpdateAcademicSettingsDto
    {
        public List<CreateUpdateGradePolicyDto> GradePolicies { get; set; } = new();

        public List<CreateUpdateSchoolPromotionRuleDto> SchoolPromotionRules { get; set; } = new();

        public List<CreateUpdateSchoolRuleDto> SchoolRules { get; set; } = new();

        public List<CreateUpdateSchoolClassesDto> SchoolClasses { get; set; } = new();
        public List<CreateUpdateSchoolSectionDto> SchoolSections { get; set; } = new();
        public List<CreateUpdateSchoolSubjectDto> SchoolSubjects { get; set; } = new();
    }

    public class CreateUpdateGradePolicyDto
    {
        public Guid Id { get; set; }
        public int FromPercent { get; set; }
        public int ToPercent { get; set; }
        public string Grade { get; set; } = null!;
    }

    public class CreateUpdateSchoolPromotionRuleDto
    {
        public Guid PromotionId { get; set; }
        public Guid RuleId { get; set; }
        public Guid FilterId { get; set; }
        public string PromotionValue { get; set; } = null!;
        public bool IsPercent { get; set; }
    }

    public class CreateUpdateSchoolRuleDto
    {
        public Guid? Id { get; set; }
        public string RuleName { get; set; } = null!;
    }

    public class CreateUpdateSchoolClassesDto
    {
        public Guid? Id { get; set; }
        public string ClassName { get; set; } = null!;
    }

    public class CreateUpdateSchoolSectionDto
    {
        public Guid? Id { get; set; }
        public string AppClassId { get; set; } = null!;
        public string SectionName { get; set; } = null!;
        public int SectionOrder { get; set; }
        public string Description { get; set; } = null!;
    }

    public class CreateUpdateSchoolSubjectDto
    {
        public Guid? Id { get; set; }
        public string SubjectName { get; set; } = null!;
        public Guid ClassId { get; set; }
        public bool IsOptional { get; set; }
    }
}
