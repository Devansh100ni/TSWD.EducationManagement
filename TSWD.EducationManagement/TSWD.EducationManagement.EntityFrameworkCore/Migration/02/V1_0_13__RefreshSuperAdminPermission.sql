IF EXISTS (
    SELECT 1
    FROM AppPermissions p
    CROSS JOIN AppRoles r
    WHERE r.Name = 'ApplicationAdministrator'
      AND NOT EXISTS (
          SELECT 1
          FROM AppRolePermissions arp
          WHERE arp.RoleId = r.Id
            AND arp.PermissionId = p.Id
            AND arp.TenantId IS NULL
      )
)
BEGIN
    INSERT INTO AppRolePermissions (Id, RoleId, PermissionId, TenantId, ExtraProperties, ConcurrencyStamp)
    SELECT 
        NEWID() AS Id,
        r.Id AS RoleId,
        p.Id AS PermissionId,
        NULL AS TenantId,           -- No tenant
        '{}' AS ExtraProperties,    -- Default empty JSON
        NEWID() AS ConcurrencyStamp
    FROM AppPermissions p
    CROSS JOIN AppRoles r
    WHERE r.Name = 'ApplicationAdministrator'
      AND NOT EXISTS (
          SELECT 1
          FROM AppRolePermissions arp
          WHERE arp.RoleId = r.Id
            AND arp.PermissionId = p.Id
            AND arp.TenantId IS NULL
      );
END