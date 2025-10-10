using TSWD.EducationManagement.Domain.DTOs.Role;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Application.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<AppRole> repository;

        public RoleService(IRepository<AppRole> repository)
        {
            this.repository = repository;
        }

        public async Task CreateUpdate(CreateUpdateRoleDto input)
        {
            if (input.Id.HasValue)
            {
                var role = await repository.GetByIdAsync(input.Id.Value);

                if (role != null)
                {
                    role.Name = input.Name;
                    await repository.UpdateAsync(role);
                }
            }
            else
            {
                AppRole appRole = new AppRole
                {
                    Name = input.Name,
                    TenantId = input.TenantId
                };  

                await repository.AddAsync(appRole);
            }
        }

        public async Task<PagedResult<RoleDto>> Get(PagedRequest input, Guid? tenantId = null)
        {
            var query = await repository.GetAllAsync();

            if (tenantId.HasValue)
                query = query.Where(r => r.TenantId == tenantId.Value);

            int skip = (input.PageNumber - 1) * input.PageSize;

            var totalCount = query.ToList().Count;

            var roles = query.Select(e => new RoleDto
            {
                Id = e.Id,
                Name = e.Name,
            }).Skip(skip).Take(input.PageSize).ToList();

            if (!roles.Any() && input.PageNumber > 1)
            {
                input.PageNumber--;

                skip = (input.PageNumber - 1) * input.PageSize;

                await Get(input);
            }

            return new PagedResult<RoleDto>(roles.ToList(), totalCount);
        }
    }
}
