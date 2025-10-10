using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppPermission
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string? Description { get; set; }
        public string ExtraProperties { get; set; } = null!;
        public string ConcurrencyStamp { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }

    }
}
