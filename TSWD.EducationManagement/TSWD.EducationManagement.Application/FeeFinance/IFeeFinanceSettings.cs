using System;
using System.Threading;
using System.Threading.Tasks;
using TSWD.EducationManagement.Domain.DTOs.FeeFinance;

namespace TSWD.EducationManagement.Application.FeeFinance
{
    public interface IFeeFinanceSettings
    {
        Task<FeeFinanceSettingsDto> GetFeeFinanceSettingsAsync(Guid? tenantId, CancellationToken cancellationToken = default);
        Task CreateUpdateFeeFinanceSettingsAsync(Guid? tenantId, FeeFinanceSettingsDto dto, CancellationToken cancellationToken = default);
    }
}
