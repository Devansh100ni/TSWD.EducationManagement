using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSWD.EducationManagement.Domain.Extentions
{
    public interface MultiTenant
    {
        public Guid? TenantId { get; set; }
    }
}
