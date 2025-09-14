-- 1️⃣ Insert the Application Administrator role if it does not exist
DECLARE @AdminRoleId UNIQUEIDENTIFIER;

IF NOT EXISTS (SELECT 1 FROM [dbo].[AppRoles] WHERE [Name] = 'ApplicationAdministrator')
BEGIN
    SET @AdminRoleId = NEWID();

    INSERT INTO [dbo].[AppRoles] 
        ([Id], [TenantId], [Name], [Description], [ExtraProperties], [ConcurrencyStamp])
    VALUES 
        (@AdminRoleId, NULL, 'ApplicationAdministrator', 'Default application admin role', '{}', NEWID());
END
ELSE
BEGIN
    -- Get the existing RoleId
    SELECT @AdminRoleId = [Id] FROM [dbo].[AppRoles] WHERE [Name] = 'ApplicationAdministrator';
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[AppUsers] WHERE [NormalizedUserName] = 'ADMINISTRATOR')
BEGIN
    INSERT INTO [dbo].[AppUsers]
        ([Id], [TenantId], [UserName], [NormalizedUserName], [Name], [Surname], [Email], [NormalizedEmail], 
         [EmailConfirmed], [PasswordHash], [SecurityStamp], [IsExternal], [PhoneNumber], [PhoneNumberConfirmed],
         [IsActive], [RoleId], [TwoFactorEnabled], [LockoutEnabled], [AccessFailedCount], [ShouldChangePasswordOnNextLogin],
         [EntityVersion], [ExtraProperties], [ConcurrencyStamp], [CreationTime])
    VALUES
        (NEWID(), 
         NULL, 
         'Administrator', 
         UPPER('Administrator'),        -- NormalizedUserName
         'System', 
         'Admin', 
         'admin@tswd.com', 
         UPPER('admin@tswd.com'),   -- NormalizedEmail
         1,                             -- EmailConfirmed
         'YourHashedPasswordHere',      -- Replace with actual hashed password
         NEWID(),                       -- SecurityStamp
         0, NULL, 0, 1, 
         @AdminRoleId, 
         0, 0, 0, 1, 0, 
         '{}', 
         NEWID(), 
         SYSUTCDATETIME());
END
