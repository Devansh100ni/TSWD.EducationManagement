namespace TSWD.EducationManagement.Shared.Providers
{
    public interface ITenantProvider
    {
        Guid? TenantId { get; }
        bool IsApplicationAdministrator { get; }
    }
}
