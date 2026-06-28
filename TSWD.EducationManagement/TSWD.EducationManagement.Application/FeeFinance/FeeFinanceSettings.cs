using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TSWD.EducationManagement.Domain.DTOs.FeeFinance;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;

namespace TSWD.EducationManagement.Application.FeeFinance
{
    public class FeeFinanceSettings : IFeeFinanceSettings
    {
        private readonly IRepository<AppFeeType> feeTypeRepository;
        private readonly IRepository<AppFineRule> fineRuleRepository;
        private readonly IRepository<AppFeeReminder> feeReminderRepository;

        public FeeFinanceSettings(
            IRepository<AppFeeType> feeTypeRepository,
            IRepository<AppFineRule> fineRuleRepository,
            IRepository<AppFeeReminder> feeReminderRepository)
        {
            this.feeTypeRepository = feeTypeRepository;
            this.fineRuleRepository = fineRuleRepository;
            this.feeReminderRepository = feeReminderRepository;
        }

        public async Task<FeeFinanceSettingsDto> GetFeeFinanceSettingsAsync(Guid? tenantId, CancellationToken cancellationToken = default)
        {
            var feeTypes = await feeTypeRepository.GetAllAsync(x => x.TenantId == tenantId, x => new FeeTypeDto { Id = x.Id, FeeName = x.FeeName, Frequency = x.Frequency }, cancellationToken);
            var fineRules = await fineRuleRepository.GetAllAsync(x => x.TenantId == tenantId, x => new FineRuleDto { Id = x.Id, FineType = x.FineType, Value = x.Value }, cancellationToken);
            var feeReminders = await feeReminderRepository.GetAllAsync(x => x.TenantId == tenantId, x => new FeeReminderDto { Id = x.Id, ReminderFrequencyDays = x.ReminderFrequencyDays, IsActive = x.IsActive }, cancellationToken);

            return new FeeFinanceSettingsDto
            {
                FeeTypes = feeTypes.ToList(),
                FineRules = fineRules.ToList(),
                FeeReminders = feeReminders.ToList()
            };
        }

        public async Task CreateUpdateFeeFinanceSettingsAsync(Guid? tenantId, FeeFinanceSettingsDto dto, CancellationToken cancellationToken = default)
        {
            // Fee Types
            var existingFeeTypes = (await feeTypeRepository.GetAllAsync(cancellationToken)).Where(x => x.TenantId == tenantId).ToList();
            var feeTypesToRemove = existingFeeTypes.Where(e => !dto.FeeTypes.Any(c => c.Id == e.Id)).ToList();
            if (feeTypesToRemove.Any()) await feeTypeRepository.DeleteRangeAsync(feeTypesToRemove);
            foreach (var c in dto.FeeTypes.Where(x => x.Id.HasValue && existingFeeTypes.Any(e => e.Id == x.Id)))
            {
                var e = existingFeeTypes.First(x => x.Id == c.Id);
                e.FeeName = c.FeeName;
                e.Frequency = c.Frequency;
                await feeTypeRepository.UpdateAsync(e, cancellationToken);
            }
            var newFeeTypes = dto.FeeTypes.Where(x => !x.Id.HasValue || !existingFeeTypes.Any(e => e.Id == x.Id))
                .Select(x => new AppFeeType { Id = Guid.NewGuid(), FeeName = x.FeeName, Frequency = x.Frequency, TenantId = tenantId }).ToList();
            if (newFeeTypes.Any()) await feeTypeRepository.AddRangeAsync(newFeeTypes, cancellationToken);

            // Fine Rules
            var existingFineRules = (await fineRuleRepository.GetAllAsync(cancellationToken)).Where(x => x.TenantId == tenantId).ToList();
            var fineRulesToRemove = existingFineRules.Where(e => !dto.FineRules.Any(c => c.Id == e.Id)).ToList();
            if (fineRulesToRemove.Any()) await fineRuleRepository.DeleteRangeAsync(fineRulesToRemove);
            foreach (var c in dto.FineRules.Where(x => x.Id.HasValue && existingFineRules.Any(e => e.Id == x.Id)))
            {
                var e = existingFineRules.First(x => x.Id == c.Id);
                e.FineType = c.FineType;
                e.Value = c.Value;
                await fineRuleRepository.UpdateAsync(e, cancellationToken);
            }
            var newFineRules = dto.FineRules.Where(x => !x.Id.HasValue || !existingFineRules.Any(e => e.Id == x.Id))
                .Select(x => new AppFineRule { Id = Guid.NewGuid(), FineType = x.FineType, Value = x.Value, TenantId = tenantId }).ToList();
            if (newFineRules.Any()) await fineRuleRepository.AddRangeAsync(newFineRules, cancellationToken);

            // Fee Reminders
            var existingFeeReminders = (await feeReminderRepository.GetAllAsync(cancellationToken)).Where(x => x.TenantId == tenantId).ToList();
            var feeRemindersToRemove = existingFeeReminders.Where(e => !dto.FeeReminders.Any(c => c.Id == e.Id)).ToList();
            if (feeRemindersToRemove.Any()) await feeReminderRepository.DeleteRangeAsync(feeRemindersToRemove);
            foreach (var c in dto.FeeReminders.Where(x => x.Id.HasValue && existingFeeReminders.Any(e => e.Id == x.Id)))
            {
                var e = existingFeeReminders.First(x => x.Id == c.Id);
                e.ReminderFrequencyDays = c.ReminderFrequencyDays;
                e.IsActive = c.IsActive;
                await feeReminderRepository.UpdateAsync(e, cancellationToken);
            }
            var newFeeReminders = dto.FeeReminders.Where(x => !x.Id.HasValue || !existingFeeReminders.Any(e => e.Id == x.Id))
                .Select(x => new AppFeeReminder { Id = Guid.NewGuid(), ReminderFrequencyDays = x.ReminderFrequencyDays, IsActive = x.IsActive, TenantId = tenantId }).ToList();
            if (newFeeReminders.Any()) await feeReminderRepository.AddRangeAsync(newFeeReminders, cancellationToken);
        }
    }
}
