using TSWD.EducationManagement.Domain.DTOs.SchoolAcademicSettings;

namespace TSWD.EducationManagement.Application.SchoolAcademicSettings
{
    public interface ISchoolAcademicSettings
    {
        Task<AcademicSettingsDto> GetAcademicSettingsAsync(Guid? tenantId, CancellationToken cancellationToken = default);

        Task CreateUpdateRules(Guid? tenantId, CreateUpdateAcademicSettingsDto createUpdateAcademicSettingsDto, CancellationToken cancellationToken = default);

        Task CreateUpdateFilters(Guid? tenantId, List<SchoolFilterDto> filters, CancellationToken cancellationToken = default);
    }
}
