using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSWD.EducationManagement.Domain.DTOs.Tanent
{
    public class CreateUpdateTenantDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
