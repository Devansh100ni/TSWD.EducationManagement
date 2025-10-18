using TSWD.EducationManagement.Domain.DTOs.SchoolGeneralSettings;

namespace TSWD.EducationManagement.Application.SchoolGeneralSettings
{
    public interface ISchoolGeneralSettings
    {
        Task<SchoolGeneralSettingDto> GetGenralSettingsAsync(Guid tenantId, CancellationToken cancellationToken = default);

        Task<SchoolGeneralSettingDto> UpdateSchoolGeneralSettingsAsync(UpdateSchoolGeneralSettingDto input, CancellationToken cancellationToken = default);
    }
}
