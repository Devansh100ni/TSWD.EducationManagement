using System;
using System.Collections.Generic;
using System.Text;
using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppPromotionRule : BaseEntityWithMandatoryFields, MultiTenant
    {
        public Guid Id { get; set; }
        public Guid RuleId { get; set; }
        public Guid FilterId { get; set; }
        public string RuleValue { get; set; } = null!;
        public bool IsPercent { get; set; }
        public Guid? UserId { get; set; }
        public Guid? TenantId { get; set; }
    }
}
