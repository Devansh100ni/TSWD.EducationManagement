using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSWD.EducationManagement.Permissions
{
    public class EducationPermissionDefinitionProvider : IPermissionDefinitionProvider
    {
        public void Define(PermissionDefinitionContext context)
        {
            var users = context.AddGroup("Users", "User Management");
            context.AddGroup("Users.Create", "Create User");
            context.AddGroup("Users.Edit", "Edit User");
            context.AddGroup("Users.Delete", "Delete User");
        }
    }
}
