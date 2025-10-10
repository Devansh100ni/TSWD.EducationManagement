using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSWD.EducationManagement.Domain.DTOs.Role;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Application.Roles
{
    public interface IRoleService
    {
        Task<PagedResult<RoleDto>> Get(PagedRequest input, Guid? tenantId = null);

        Task<List<PermissionGroupDto>> GetPermissionGroups();

        Task CreateUpdate(CreateUpdateRoleDto input);
    }
}
