-- Create AppPermissions if not exists
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AppPermissions' AND xtype='U')
BEGIN
    CREATE TABLE AppPermissions (
        Id UNIQUEIDENTIFIER PRIMARY KEY,

        Name NVARCHAR(100) NOT NULL,
        Description NVARCHAR(255),

        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL
    );

    -- Index on Name for fast lookups
    CREATE UNIQUE INDEX IX_AppPermissions_Name ON AppPermissions(Name);
END
GO

-- Create AppRolePermissions if not exists
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='AppRolePermissions' AND xtype='U')
BEGIN
    CREATE TABLE AppRolePermissions (
        Id UNIQUEIDENTIFIER PRIMARY KEY,

        RoleId UNIQUEIDENTIFIER NOT NULL,
        PermissionId UNIQUEIDENTIFIER NOT NULL,
        TenantId UNIQUEIDENTIFIER NOT NULL,

        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,

        CONSTRAINT FK_AppRolePermissions_Role FOREIGN KEY (RoleId) REFERENCES AppRoles(Id),
        CONSTRAINT FK_AppRolePermissions_Permission FOREIGN KEY (PermissionId) REFERENCES AppPermissions(Id),
        CONSTRAINT FK_AppRolePermissions_Tenant FOREIGN KEY (TenantId) REFERENCES AppTenants(Id)
    );

    -- Indexes for faster joins
    CREATE INDEX IX_AppRolePermissions_RoleId ON AppRolePermissions(RoleId);
    CREATE INDEX IX_AppRolePermissions_PermissionId ON AppRolePermissions(PermissionId);
    CREATE INDEX IX_AppRolePermissions_TenantId ON AppRolePermissions(TenantId);

    -- Ensure no duplicate Role-Permission per Tenant
    CREATE UNIQUE INDEX UX_AppRolePermissions_Role_Permission_Tenant
        ON AppRolePermissions(RoleId, PermissionId, TenantId);
END
GO
