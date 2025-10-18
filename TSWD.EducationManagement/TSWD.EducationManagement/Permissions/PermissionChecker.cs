using System;
using TSWD.EducationManagement.EntityFrameworkCore;
using TSWD.EducationManagement.Shared.Providers;

namespace TSWD.EducationManagement.Permissions
{
    public class PermissionChecker : IPermissionChecker
    {
        private readonly EducationDbContext _db;
        private readonly ITenantProvider _tenantProvider;

        public PermissionChecker(EducationDbContext db, ITenantProvider tenantProvider)
        {
            _db = db;
            _tenantProvider = tenantProvider;
        }

        public async Task<bool> HasPermissionAsync(Guid userId, string permissionName)
        {
            // Get user role(s)
            var userRoles = _db.AppUser
                .Where(ur => ur.Id == userId)
                .Select(ur => ur.RoleId)
                .ToList();

            var tenantId = _tenantProvider.TenantId;

            var hasPermission = (
                from rp in _db.AppRolePermissions
                join p in _db.AppPermissions on rp.PermissionId equals p.Id
                join r in _db.AppRole on rp.RoleId equals r.Id
                where userRoles.Contains(rp.RoleId)
                      && (r.TenantId == tenantId || tenantId == null)
                select rp
            ).Any();

            return hasPermission;
        }
    }
}
