using System;
using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppFineRule : BaseEntityWithMandatoryFields, MultiTenant
    {
        public Guid Id { get; set; }
        public string FineType { get; set; } = null!; 
        public decimal Value { get; set; } 
        
        public Guid? UserId { get; set; }
        public Guid? TenantId { get; set; }
    }
}
