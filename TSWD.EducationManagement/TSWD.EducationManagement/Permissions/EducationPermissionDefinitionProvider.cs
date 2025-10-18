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

            var students = context.AddGroup(PermissionConstent.Students.Default, "Student Management");
            context.AddGroup(PermissionConstent.Students.Create, "Create Student");
            context.AddGroup(PermissionConstent.Students.Edit, "Edit Student");
            context.AddGroup(PermissionConstent.Students.Delete, "Delete Student");
        }
    }
}
