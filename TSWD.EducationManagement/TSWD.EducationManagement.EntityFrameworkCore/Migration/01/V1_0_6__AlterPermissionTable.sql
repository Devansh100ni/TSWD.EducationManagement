IF NOT EXISTS (
    SELECT 1
    FROM sys.columns c
    JOIN sys.objects o ON c.object_id = o.object_id
    WHERE o.name = 'AppPermissions'
      AND o.type = 'U'
      AND c.name = 'DisplayName'
)
BEGIN
    ALTER TABLE [AppPermissions]
    ADD [DisplayName] varchar(MAX) NULL;
END


IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE Name = N'TenantId'
      AND Object_ID = OBJECT_ID(N'dbo.AppRolePermissions')
)
BEGIN
    ALTER TABLE [dbo].[AppRolePermissions]
    ALTER COLUMN [TenantId] UNIQUEIDENTIFIER NULL;
END