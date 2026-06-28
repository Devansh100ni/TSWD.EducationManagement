using System;
using System.Collections.Generic;

namespace TSWD.EducationManagement.Domain.DTOs.FeeFinance
{
    public class FeeFinanceSettingsDto
    {
        public List<FeeTypeDto> FeeTypes { get; set; } = new();
        public List<FineRuleDto> FineRules { get; set; } = new();
        public List<FeeReminderDto> FeeReminders { get; set; } = new();
    }

    public class FeeTypeDto
    {
        public Guid? Id { get; set; }
        public string FeeName { get; set; } = null!;
        public string Frequency { get; set; } = null!;
    }

    public class FineRuleDto
    {
        public Guid? Id { get; set; }
        public string FineType { get; set; } = null!;
        public decimal Value { get; set; }
    }

    public class FeeReminderDto
    {
        public Guid? Id { get; set; }
        public int ReminderFrequencyDays { get; set; }
        public bool IsActive { get; set; }
    }
}
