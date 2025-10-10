using System.Data;
using TSWD.EducationManagement.Domain.DTOs.Role;
using TSWD.EducationManagement.Domain.Entities;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;
using TSWD.EducationManagement.Shared.Helpers;

namespace TSWD.EducationManagement.Application.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<AppRole> repository;
        private readonly IRepository<AppRolePermission> rolePermissionRepo;
        private readonly IRepository<AppPermission> permissionRepo;

        public RoleService(IRepository<AppRole> repository,
                           IRepository<AppRolePermission> rolePermissionRepo,
                           IRepository<AppPermission> permissionRepo)
        {
            this.repository = repository;
            this.rolePermissionRepo = rolePermissionRepo;
            this.permissionRepo = permissionRepo;
        }

        public async Task CreateUpdate(CreateUpdateRoleDto input)
        {
            AppRole role;

            if (input.Id.HasValue)
            {
                // Update existing role
                role = await repository.GetByIdAsync(input.Id.Value);

                if (role != null)
                {
                    role.Name = input.Name;
                    await repository.UpdateAsync(role);

                    // Remove old permissions
                    var oldPermissions = (await rolePermissionRepo.GetAllAsync()).Where(p => p.RoleId == role.Id).ToList();
                    foreach (var perm in oldPermissions)
                    {
                        await rolePermissionRepo.DeleteAsync(perm);
                    }
                }
            }
            else
            {
                // Create new role
                role = new AppRole
                {
                    Name = input.Name,
                    TenantId = input.TenantId,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };

                role = await repository.AddAsync(role);
            }

            // Add new permissions
            if (input.Permissions != null)
            {
                foreach (var permId in input.Permissions)
                {
                    await rolePermissionRepo.AddAsync(new AppRolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = permId.PermissionId,
                        TenantId = permId.TenantId,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        ExtraProperties = "{}"
                    });
                }
            }
        }

        public async Task<PagedResult<RoleDto>> Get(PagedRequest input, Guid? tenantId = null)
        {
            var rolesQuery = await repository.GetAllAsync();
            var rolePermissions = await rolePermissionRepo.GetAllAsync();
            var allPermissions = await permissionRepo.GetAllAsync();

            if (tenantId.HasValue)
                rolesQuery = rolesQuery.Where(r => r.TenantId == tenantId.Value);

            var totalCount = rolesQuery.Count();
            int skip = (input.PageNumber - 1) * input.PageSize;

            var pagedRoles = rolesQuery
                .Skip(skip)
                .Take(input.PageSize)
                .ToList();

            var roles = pagedRoles.Select(role =>
            {
                var permissionIds = rolePermissions
                    .Where(rp => rp.RoleId == role.Id)
                    .Select(rp => rp.PermissionId)
                    .ToList();

                var permissionNames = allPermissions
                    .Where(p => permissionIds.Contains(p.Id))
                    .Select(p => p.DisplayName)
                    .ToList();

                return new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Permissions = string.Join(", ", permissionNames) ?? "-"
                };
            }).ToList();

            // If no roles found on this page, go back one page
            if (!roles.Any() && input.PageNumber > 1)
            {
                input.PageNumber--;
                return await Get(input, tenantId);
            }

            return new PagedResult<RoleDto>(roles.ToList(), totalCount);
        }

        public async Task<List<PermissionGroupDto>> GetPermissionGroups()
        {
            var permissionGroups = new List<PermissionGroupDto>();

            // Fetch data
            var role = await repository.GetAllAsync();
            var rolePermissions = await rolePermissionRepo.GetAllAsync();
            var allPermissions = await permissionRepo.GetAllAsync();

            if (role == null)
                throw new Exception("Role not found.");
            
            // Group all permissions by their prefix (e.g., "Users" from "Users.Create")
            permissionGroups = allPermissions
                .GroupBy(p => p.Name.Split('.')[0]) // "Users" or "Students"
                .Select(g => new PermissionGroupDto
                {
                    GroupName = g.Key,
                    Permissions = g.Select(p => new PermissionItemDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Display = p.DisplayName
                    }).ToList()
                })
                .ToList();

            return permissionGroups;
        }
    }
}
