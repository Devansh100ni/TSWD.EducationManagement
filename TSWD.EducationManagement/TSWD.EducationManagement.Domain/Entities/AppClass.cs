using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppClass : BaseEntityWithMandatoryFields, MultiTenant
    {
        public Guid Id { get; set; }
        public string CLassName { get; set; } = null!;
        public Guid? UserId { get; set; }
        public Guid? TenantId { get; set; }
    }
}
