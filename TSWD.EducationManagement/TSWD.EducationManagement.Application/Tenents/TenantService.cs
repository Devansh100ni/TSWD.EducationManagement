using TSWD.EducationManagement.Dapper.Tanent;
using TSWD.EducationManagement.Domain.DTOs.Tanent;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Application.Tenents
{
    public class TenantService : ITenantService
    {
        private readonly IRepository<AppTenant> repository;
        private readonly ITenantDapperService tenantDapperService;

        public TenantService(IRepository<AppTenant> repository,
                                ITenantDapperService tenantDapperService)
        {
            this.repository = repository;
            this.tenantDapperService = tenantDapperService;
        }

        public async Task<Result<TenantDto>> CreateUpdateTenant(CreateUpdateTenantDto tenantDto)
        {
            if (tenantDto == null)
                return Result<TenantDto>.Fail("Tenant data is required");

            AppTenant entity;

            if (tenantDto.Id.HasValue) // ✅ Update existing tenant
            {
                entity = await repository.GetByIdAsync(tenantDto.Id.Value);

                if (entity == null)
                    return Result<TenantDto>.Fail("Tenant not found");

                // manual mapping from DTO to entity (update fields)
                entity.Name = tenantDto.Name;

                await repository.UpdateAsync(entity);
            }
            else // ✅ Create new tenant
            {
                entity = new AppTenant
                {
                    Id = Guid.NewGuid(),
                    Name = tenantDto.Name,
                    NormalizedName = tenantDto.Name.ToUpper()
                };

                await repository.AddAsync(entity);
            }

            // manual mapping from entity to DTO
            var dto = new TenantDto
            {
                Id = entity.Id,
                Name = entity.Name,
            };

            return Result<TenantDto>.Ok(dto);
        }

        public async Task<Result<TenantDto>> GetById(Guid id)
        {
            var tenant = await repository.GetByIdAsync(id);

            var dto = new TenantDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
            };

            return Result<TenantDto>.Ok(dto);
        }

        public async Task<PagedResult<TenantDto>> ListOfTenants(int pageNumber, int pageSize)
        {
            return await tenantDapperService.GetTenantsAsync(pageNumber, pageSize);
        }


    }
}
