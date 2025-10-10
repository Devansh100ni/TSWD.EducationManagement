using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSWD.EducationManagement.Domain.DTOs.Role
{
    public class AddPermissionDto
    {
        public Guid PermissionId { get; set; }
        public Guid TenantId { get; set; }
    }
}
