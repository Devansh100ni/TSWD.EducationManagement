using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppSchoolRule : BaseEntityWithMandatoryFields, MultiTenant
    {
        public Guid Id { get; set; }
        public string RuleName { get; set; } = null!;
        public Guid? UserId { get; set; }
        public Guid? TenantId { get; set; }
    }
}
