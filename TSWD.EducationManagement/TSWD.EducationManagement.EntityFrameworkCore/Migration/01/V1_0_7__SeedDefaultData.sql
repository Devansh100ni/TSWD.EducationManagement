BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @TenantId UNIQUEIDENTIFIER = NULL;  -- Set your tenant ID if needed, or leave NULL for host
    DECLARE @AdminRoleId UNIQUEIDENTIFIER;
    DECLARE @AdminUserId UNIQUEIDENTIFIER;
    DECLARE @PasswordHash NVARCHAR(512) = 'QCnjOZANpHLWY22dSwS2OVuxR6kfg2hwBoYVOEXc1pPoIeGWcAKHS3WQuy4NqTS/';

    ----------------------------------------------------------
    -- 1️⃣ Ensure "Administrator" Role Exists
    ----------------------------------------------------------
    IF NOT EXISTS (
        SELECT 1 FROM [dbo].[AppRoles]
        WHERE [Name] = 'ApplicationAdministrator'
          AND (@TenantId IS NULL OR [TenantId] = @TenantId)
    )
    BEGIN
        SET @AdminRoleId = NEWID();
        INSERT INTO [dbo].[AppRoles] (
            [Id], [TenantId], [Name], [Description],
            [CreationTime], [IsDeleted], [ExtraProperties]
        )
        VALUES (
            @AdminRoleId, @TenantId, 'ApplicationAdministrator', 'System Administrator Role',
            GETDATE(), 0, '{}'
        );
    END
    ELSE
    BEGIN
        SELECT @AdminRoleId = [Id]
        FROM [dbo].[AppRoles]
        WHERE [Name] = 'ApplicationAdministrator'
          AND (@TenantId IS NULL OR [TenantId] = @TenantId);
    END;

    ----------------------------------------------------------
    -- 2️⃣ Ensure "ApplicationAdministrator" User Exists
    ----------------------------------------------------------
    IF NOT EXISTS (
        SELECT 1 FROM [dbo].[AppUsers]
        WHERE [UserName] = 'admin'
          AND (@TenantId IS NULL OR [TenantId] = @TenantId)
    )
    BEGIN
        SET @AdminUserId = NEWID();
        INSERT INTO [dbo].[AppUsers] (
            [Id], [TenantId], [UserName], [NormalizedUserName],
            [Email], [NormalizedEmail], [EmailConfirmed],
            [PasswordHash], [RoleId],
            [CreationTime], [IsDeleted], [ExtraProperties], [SecurityStamp], [IsActive],
            [ShouldChangePasswordOnNextLogin], [ConcurrencyStamp]
        )
        VALUES (
            @AdminUserId, @TenantId, 'admin', 'ADMIN',
            'devansh.soni@tswd.com', 'DEVANSH.SONI@TSWD.COM', 1,
            @PasswordHash, @AdminRoleId,
            GETDATE(), 0, '{}', NEWID(), 1, 0, NEWID()
        );
    END
    ELSE
    BEGIN
        SELECT @AdminUserId = [Id]
        FROM [dbo].[AppUsers]
        WHERE [UserName] = 'admin'
          AND (@TenantId IS NULL OR [TenantId] = @TenantId);
    END;

    ----------------------------------------------------------
    -- 3️⃣ Assign ALL Permissions to the "Administrator" Role
    ----------------------------------------------------------
    INSERT INTO [dbo].[AppRolePermissions] (
        [Id], [RoleId], [PermissionId], [TenantId],
        [CreationTime], [ExtraProperties], [ConcurrencyStamp]
    )
    SELECT
        NEWID(),
        @AdminRoleId,
        P.[Id],
        @TenantId,
        GETDATE(),
        '{}',
        NEWID()
    FROM [dbo].[AppPermissions] P
    WHERE NOT EXISTS (
        SELECT 1 FROM [dbo].[AppRolePermissions] RP
        WHERE RP.[RoleId] = @AdminRoleId AND RP.[PermissionId] = P.[Id]
    );

    ----------------------------------------------------------
    -- ✅ Commit if everything works
    ----------------------------------------------------------
    COMMIT TRANSACTION;

    PRINT '✅ Transaction committed successfully.';
    PRINT 'Administrator Role ID: ' + CAST(@AdminRoleId AS NVARCHAR(100));
    PRINT 'Administrator User ID: ' + CAST(@AdminUserId AS NVARCHAR(100));
    PRINT 'All missing permissions assigned successfully.';

END TRY
BEGIN CATCH
    ----------------------------------------------------------
    -- ❌ Rollback and show error if anything fails
    ----------------------------------------------------------
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
    SELECT
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

    PRINT '❌ Transaction rolled back due to error:';
    PRINT @ErrorMessage;

    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
