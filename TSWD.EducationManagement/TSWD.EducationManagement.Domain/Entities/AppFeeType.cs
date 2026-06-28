using System;
using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppFeeType : BaseEntityWithMandatoryFields, MultiTenant
    {
        public Guid Id { get; set; }
        public string FeeName { get; set; } = null!;
        public string Frequency { get; set; } = null!;
        
        public Guid? UserId { get; set; }
        public Guid? TenantId { get; set; }
    }
}
