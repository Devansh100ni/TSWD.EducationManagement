using TSWD.EducationManagement.Domain.DTOs.SchoolAcademicSettings;

namespace TSWD.EducationManagement.Application.SchoolAcademicSettings
{
    public interface ISchoolAcademicSettings
    {
        Task<AcademicSettingsDto> GetAcademicSettingsAsync(Guid tenantId, CancellationToken cancellationToken = default);
    }
}
