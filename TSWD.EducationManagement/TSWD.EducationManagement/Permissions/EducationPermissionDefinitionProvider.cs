namespace TSWD.EducationManagement.Permissions
{
    public class EducationPermissionDefinitionProvider : IPermissionDefinitionProvider
    {
        public void Define(PermissionDefinitionContext context)
        {
            var users = context.AddGroup(PermissionConstent.Users.Default, "User Management");
            context.AddGroup(PermissionConstent.Users.Create, "Create User");
            context.AddGroup(PermissionConstent.Users.Edit, "Edit User");
            context.AddGroup(PermissionConstent.Users.Delete, "Delete User");
        }
    }
}
