using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Permissions
{
    public interface IPermissionDefinitionProvider
    {
        void Define(PermissionDefinitionContext context);
    }
}
