using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSWD.EducationManagement.Shared.Providers
{
    public interface ITenantProvider
    {
        Guid? TenantId { get; }
        bool IsApplicationAdministrator { get; }
    }
}
