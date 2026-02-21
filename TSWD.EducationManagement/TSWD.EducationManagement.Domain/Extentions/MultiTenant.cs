namespace TSWD.EducationManagement.Domain.Extentions
{
    public interface MultiTenant
    {
        public Guid? TenantId { get; set; }
    }
}
