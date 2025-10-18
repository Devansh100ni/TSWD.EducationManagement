using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppSchoolGeneralSetting : BaseEntityWithMandatoryFields, MultiTenant
    {
        public Guid Id { get; set; }

        public Guid? TenantId { get; set; }

        public string SchoolName { get; set; }

        public string SchoolCode { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? LogoUrl { get; set; }

        public string? TimeZone { get; set; }

        public string? DateFormat { get; set; }
    }
}
