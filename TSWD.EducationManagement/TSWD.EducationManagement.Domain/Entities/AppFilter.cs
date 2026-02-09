using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppFilter : BaseEntityWithMandatoryFields, MultiTenant
    {
        public Guid Id { get; set; }

        public string FilterKey { get; set; } = null!;

        public string FilterValue { get; set; } = null!;

        public Guid? UserId { get; set; }

        public Guid? TenantId { get; set; }
    }
}
