using System;
using System.Collections.Generic;
using System.Text;
using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppSubject : BaseEntityWithMandatoryFields, MultiTenant
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; } = null!;

        public Guid ClassId { get; set; }

        public bool  IsOptional { get; set; } = false;

        public Guid? UserId { get; set; }
        public Guid? TenantId { get; set; }
    }
}
