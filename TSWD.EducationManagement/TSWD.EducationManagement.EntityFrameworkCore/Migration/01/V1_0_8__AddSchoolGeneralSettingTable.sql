IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppSchoolGeneralSettings')
BEGIN
    CREATE TABLE [dbo].[AppSchoolGeneralSettings](
        [Id] UNIQUEIDENTIFIER NOT NULL,
        [TenantId] UNIQUEIDENTIFIER NULL, -- optional, if not same as Id

        [SchoolName] NVARCHAR(250) NOT NULL,
        [SchoolCode] NVARCHAR(50) NOT NULL,
        [Address] NVARCHAR(500) NULL,
        [Email] NVARCHAR(150) NULL,
        [Phone] NVARCHAR(50) NULL,
        [LogoUrl] NVARCHAR(300) NULL,
        [TimeZone] NVARCHAR(50) NULL,
        [DateFormat] NVARCHAR(20) NULL,

        -- Mandatory shared fields
        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,

        -- Soft delete support
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppSchools_IsDeleted DEFAULT (0),
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2(7) NULL,

        CONSTRAINT [PK_AppSchoolGeneralSettings] PRIMARY KEY CLUSTERED ([Id] ASC)
    );

    CREATE INDEX IX_AppSchoolGeneralSettings_TenantId ON [AppSchoolGeneralSettings] ([TenantId]);

    ALTER TABLE [dbo].[AppSchoolGeneralSettings] ADD CONSTRAINT [FK_AppSchoolGeneralSettings_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants]([Id]);

END
GO