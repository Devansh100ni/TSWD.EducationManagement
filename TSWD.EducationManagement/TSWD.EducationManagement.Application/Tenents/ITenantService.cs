using TSWD.EducationManagement.Domain.DTOs.Tanent;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Application.Tenents
{
    public interface ITenantService
    {
        Task<PagedResult<TenantDto>> ListOfTenants(int pageNumber, int pageSize);

        Task<Result<TenantDto>> CreateUpdateTenant(CreateUpdateTenantDto tenantDto);

        Task<Result<TenantDto>> GetById(Guid id);
    }
}
