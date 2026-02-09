using System;
using System.Collections.Generic;
using System.Text;
using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppSection : BaseEntityWithMandatoryFields, MultiTenant
    {
        public Guid Id { get; set; }
        public string AppClassId { get; set; } = null!;
        public string SectionName { get; set; } = null!;
        public int SectionOrder { get; set; } = 0;
        public string Description { get; set; } = null!;

        public Guid? UserId { get; set; }
        public Guid? TenantId { get; set; }
    }
}
