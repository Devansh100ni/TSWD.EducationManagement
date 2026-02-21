using TSWD.EducationManagement.Domain.Entities;

namespace TSWD.EducationManagement.Permissions
{
    public class PermissionDefinitionContext
    {
        private readonly List<AppPermission> _permissions = new();

        public AppPermission AddGroup(string groupName, string displayName)
        {
            var group = new AppPermission
            {
                Name = groupName,
                DisplayName = displayName,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                ExtraProperties = "{}"
            };
            _permissions.Add(group);
            return group;
        }

        public IReadOnlyList<AppPermission> GetPermissions() => _permissions;
    }
}
