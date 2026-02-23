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
    }

    public class CreateUpdateGradePolicyDto
    {
        public string Id { get; set; }
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
        public string Id { get; set; }
        public string RuleName { get; set; } = null!;
    }
}
