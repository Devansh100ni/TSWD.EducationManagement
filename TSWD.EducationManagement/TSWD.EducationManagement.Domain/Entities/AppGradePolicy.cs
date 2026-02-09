using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppGradePolicy : BaseEntityWithMandatoryFields, MultiTenant
    {
        public Guid Id { get; set; }
        public int FromPercent { get; set; }
        public int ToPercent { get; set; }
        public string Grade { get; set; } = null!;
        public Guid? UserId { get; set; }
        public Guid? TenantId { get; set; }
    }
}
