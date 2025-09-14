IF COL_LENGTH('dbo.AppRoles', 'IsDeleted') IS NULL
BEGIN
    ALTER TABLE [dbo].[AppRoles] ADD [IsDeleted] BIT NOT NULL DEFAULT 0;
END;

IF COL_LENGTH('dbo.AppRoles', 'DeleterId') IS NULL
BEGIN
    ALTER TABLE [dbo].[AppRoles] ADD [DeleterId] UNIQUEIDENTIFIER NULL;
END;

IF COL_LENGTH('dbo.AppRoles', 'DeletionTime') IS NULL
BEGIN
    ALTER TABLE [dbo].[AppRoles] ADD [DeletionTime] DATETIME2(7) NULL;
END;
