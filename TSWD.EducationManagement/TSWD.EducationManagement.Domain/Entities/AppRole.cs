using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppRole : BaseEntityWithMandatoryFields
    {
        public Guid Id { get; set; }

        public Guid? TenantId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
