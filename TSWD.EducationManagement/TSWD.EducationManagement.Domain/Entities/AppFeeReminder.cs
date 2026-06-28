using System;
using TSWD.EducationManagement.Domain.Extentions;

namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppFeeReminder : BaseEntityWithMandatoryFields, MultiTenant
    {
        public Guid Id { get; set; }
        public int ReminderFrequencyDays { get; set; }
        public bool IsActive { get; set; } = true;

        public Guid? UserId { get; set; }
        public Guid? TenantId { get; set; }
    }
}
