namespace TSWD.EducationManagement.Permissions
{
    public interface IPermissionChecker
    {
        Task<bool> HasPermissionAsync(Guid userId, string permissionName);
    }
}
