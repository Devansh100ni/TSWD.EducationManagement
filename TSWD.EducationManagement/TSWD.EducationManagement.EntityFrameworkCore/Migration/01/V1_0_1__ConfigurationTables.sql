IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppTenants')
BEGIN
    CREATE TABLE [dbo].[AppTenants](
        [Id] UNIQUEIDENTIFIER NOT NULL,
        [Name] NVARCHAR(64) NOT NULL,
        [NormalizedName] NVARCHAR(64) NOT NULL,
        [EntityVersion] INT NOT NULL,

        -- Mandatory default fields
        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,

        -- Soft delete support
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppTenants_IsDeleted DEFAULT (0),
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2(7) NULL,

        CONSTRAINT [PK_AppTenants] PRIMARY KEY CLUSTERED ([Id] ASC)
    );

    -- Indexes
    CREATE UNIQUE INDEX IX_AppTenants_Name ON [AppTenants] ([NormalizedName]);
END
GO

-- Create Roles table if it does not exist
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppRoles')
BEGIN
    CREATE TABLE [dbo].[AppRoles] (
        [Id] UNIQUEIDENTIFIER NOT NULL,
        [TenantId] UNIQUEIDENTIFIER NULL,

        [Name] NVARCHAR(128) NOT NULL,
        [Description] NVARCHAR(256) NULL,        

         -- Mandatory default fields
        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,

        CONSTRAINT [PK_AppRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
   
    ALTER TABLE [dbo].[AppRoles]
    ADD CONSTRAINT [FK_AppRoles_Tenants] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants]([Id]);

    -- Create indexes
    CREATE NONCLUSTERED INDEX [IX_AppRoles_TenantId] ON [dbo].[AppRoles] ([TenantId]);
    CREATE NONCLUSTERED INDEX [IX_AppRoles_Name] ON [dbo].[AppRoles] ([Name]);
    
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppUsers')
BEGIN
    CREATE TABLE [dbo].[AppUsers](
        [Id] UNIQUEIDENTIFIER NOT NULL,
        [TenantId] UNIQUEIDENTIFIER NULL,
        
        [UserName] NVARCHAR(256) NOT NULL,
        [NormalizedUserName] NVARCHAR(256) NOT NULL,
        [Name] NVARCHAR(64) NULL,
        [Surname] NVARCHAR(64) NULL,
        [Email] NVARCHAR(256) NOT NULL,
        [NormalizedEmail] NVARCHAR(256) NOT NULL,
        [EmailConfirmed] BIT NOT NULL CONSTRAINT DF_AppUsers_EmailConfirmed DEFAULT (0),
        [PasswordHash] NVARCHAR(256) NULL,
        [SecurityStamp] NVARCHAR(256) NOT NULL,
        [IsExternal] BIT NOT NULL CONSTRAINT DF_AppUsers_IsExternal DEFAULT (0),
        [PhoneNumber] NVARCHAR(16) NULL,
        [PhoneNumberConfirmed] BIT NOT NULL CONSTRAINT DF_AppUsers_PhoneNumberConfirmed DEFAULT (0),
        [IsActive] BIT NOT NULL,
        [RoleId] UNIQUEIDENTIFIER NULL,
        [TwoFactorEnabled] BIT NOT NULL CONSTRAINT DF_AppUsers_TwoFactorEnabled DEFAULT (0),
        [LockoutEnd] DATETIMEOFFSET(7) NULL,
        [LockoutEnabled] BIT NOT NULL CONSTRAINT DF_AppUsers_LockoutEnabled DEFAULT (0),
        [AccessFailedCount] INT NOT NULL CONSTRAINT DF_AppUsers_AccessFailedCount DEFAULT (0),
        [ShouldChangePasswordOnNextLogin] BIT NOT NULL,
        [EntityVersion] INT NOT NULL,
        [LastPasswordChangeTime] DATETIMEOFFSET(7) NULL,

        -- Mandatory shared fields
        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,

        -- Soft delete support
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppUsers_IsDeleted DEFAULT (0),
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2(7) NULL,

        CONSTRAINT [PK_AppUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
    );

    -- Indexes
    CREATE UNIQUE INDEX IX_AppUsers_UserName ON [AppUsers] ([NormalizedUserName]);
    CREATE UNIQUE INDEX IX_AppUsers_Email ON [AppUsers] ([NormalizedEmail]);
    CREATE INDEX IX_AppUsers_TenantId ON [AppUsers] ([TenantId]);
    CREATE INDEX IX_AppUsers_RoleId ON [AppUsers] ([RoleId]);

    ALTER TABLE [dbo].[AppUsers] ADD CONSTRAINT [FK_AppUsers_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants]([Id]);
    ALTER TABLE [dbo].[AppUsers] ADD CONSTRAINT [FK_AppUsers_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AppRoles]([Id]);
END
GO
