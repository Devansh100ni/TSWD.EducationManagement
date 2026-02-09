IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppGradePolicies')
BEGIN
    CREATE TABLE [dbo].[AppGradePolicies] (
        [Id] UNIQUEIDENTIFIER NOT NULL,

        [FromPercent] INT NOT NULL DEFAULT 0,
        [ToPercent] INT NOT NULL DEFAULT 0,
        [Grade] VARCHAR(10) NOT NULL,

        
        [UserId] UNIQUEIDENTIFIER NULL,
        [TenantId] UNIQUEIDENTIFIER NULL,

        -- Mandatory default fields
        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,

        -- Soft delete support
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppGradePolicies_IsDeleted DEFAULT (0),
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2(7) NULL,

        CONSTRAINT [PK_AppGradePolicies] PRIMARY KEY CLUSTERED ([Id] ASC),

        -- Foreign Keys
        CONSTRAINT [FK_AppGradePolicies_AppUsers_UserId]
            FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUsers] ([Id]),

        CONSTRAINT [FK_AppGradePolicies_AppTenants_TenantId]
            FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants] ([Id])
    );

    -- Indexes
    CREATE INDEX IX_AppGradePolicies_UserId ON [dbo].[AppGradePolicies] ([UserId]);
    CREATE INDEX IX_AppGradePolicies_TenantId ON [dbo].[AppGradePolicies] ([TenantId]);
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppSchoolRules')
BEGIN
    CREATE TABLE [dbo].[AppSchoolRules] (
        [Id] UNIQUEIDENTIFIER NOT NULL,
        
        [RuleName] varchar(MAX) NOT NULL, --Attendance, --Percentage
        
        [UserId] UNIQUEIDENTIFIER NULL,
        [TenantId] UNIQUEIDENTIFIER NULL,

        -- Mandatory default fields
        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,

        -- Soft delete support
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppSchoolRules_IsDeleted DEFAULT (0),
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2(7) NULL,

        CONSTRAINT [PK_AppSchoolRules] PRIMARY KEY CLUSTERED ([Id] ASC),

        -- Foreign Keys
        CONSTRAINT [FK_AppSchoolRules_AppUsers_UserId]
            FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUsers] ([Id]),

        CONSTRAINT [FK_AppSchoolRules_AppTenants_TenantId]
            FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants] ([Id])
    );

    -- Indexes
    CREATE INDEX IX_AppSchoolRules_UserId ON [dbo].[AppSchoolRules] ([UserId]);
    CREATE INDEX IX_AppSchoolRules_TenantId ON [dbo].[AppSchoolRules] ([TenantId]);
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppFilter')
BEGIN
    CREATE TABLE [dbo].[AppFilter] (
        [Id] UNIQUEIDENTIFIER NOT NULL,

        [FilterKey] varchar(MAX) NOT NULL, -- > , < , == 
        [FilterValue] varchar(MAX) NOT NULL, --IS GRATER THEN
        
        [UserId] UNIQUEIDENTIFIER NULL,
        [TenantId] UNIQUEIDENTIFIER NULL,

        -- Mandatory default fields
        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,

        -- Soft delete support
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppFilter_IsDeleted DEFAULT (0),
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2(7) NULL,

        CONSTRAINT [PK_AppFilter] PRIMARY KEY CLUSTERED ([Id] ASC),

        -- Foreign Keys
        CONSTRAINT [FK_AppFilter_AppUsers_UserId]
            FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUsers] ([Id]),

        CONSTRAINT [FK_AppFilter_AppTenants_TenantId]
            FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants] ([Id])
    );

    -- Indexes
    CREATE INDEX IX_AppFilter_UserId ON [dbo].[AppFilter] ([UserId]);
    CREATE INDEX IX_AppFilter_TenantId ON [dbo].[AppFilter] ([TenantId]);
END
GO


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppPromotionRules')
BEGIN
    CREATE TABLE [dbo].[AppPromotionRules] (
        [Id] UNIQUEIDENTIFIER NOT NULL,

        [RuleId] UNIQUEIDENTIFIER NOT NULL,
        [FilterId] UNIQUEIDENTIFIER NOT NULL,
        [RuleValue] varchar(MAX) NOT NULL,
        [IsPercent] varchar(MAX) NOT NULL,
        
        [UserId] UNIQUEIDENTIFIER NULL,
        [TenantId] UNIQUEIDENTIFIER NULL,

        -- Mandatory default fields
        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,

        -- Soft delete support
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppPromotionRules_IsDeleted DEFAULT (0),
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2(7) NULL,

        CONSTRAINT [PK_AppPromotionRules] PRIMARY KEY CLUSTERED ([Id] ASC),

        -- Foreign Keys
        CONSTRAINT [FK_AppPromotionRules_AppUsers_UserId]
            FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUsers] ([Id]),

        CONSTRAINT [FK_AppPromotionRules_AppTenants_TenantId]
            FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants] ([Id]),

        CONSTRAINT [FK_AppPromotionRules_AppSchoolRules_RuleId]
            FOREIGN KEY ([RuleId]) REFERENCES [dbo].[AppSchoolRules] ([Id]),

        CONSTRAINT [FK_AppPromotionRules_AppFilter_FilterId]
            FOREIGN KEY ([FilterId]) REFERENCES [dbo].[AppFilter] ([Id])
    );

    -- Indexes
    CREATE INDEX IX_AppPromotionRules_UserId ON [dbo].[AppPromotionRules] ([UserId]);
    CREATE INDEX IX_AppPromotionRules_TenantId ON [dbo].[AppPromotionRules] ([TenantId]);
END
GO


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppClasses')
BEGIN
    CREATE TABLE [dbo].[AppClasses] (
        [Id] UNIQUEIDENTIFIER NOT NULL,

        [ClassName] VARcHAR(MAX) NOT NULL,
        
        [UserId] UNIQUEIDENTIFIER NULL,
        [TenantId] UNIQUEIDENTIFIER NULL,

        -- Mandatory default fields
        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,

        -- Soft delete support
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppClasses_IsDeleted DEFAULT (0),
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2(7) NULL,

        CONSTRAINT [PK_AppClasses] PRIMARY KEY CLUSTERED ([Id] ASC),

        -- Foreign Keys
        CONSTRAINT [FK_AppClasses_AppUsers_UserId]
            FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUsers] ([Id]),

        CONSTRAINT [FK_AppClasses_AppTenants_TenantId]
            FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants] ([Id])
        
    );

    -- Indexes
    CREATE INDEX IX_AppClasses_UserId ON [dbo].[AppClasses] ([UserId]);
    CREATE INDEX IX_AppClasses_TenantId ON [dbo].[AppClasses] ([TenantId]);
END
GO


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppSections')
BEGIN
    CREATE TABLE [dbo].[AppSections] (
        [Id] UNIQUEIDENTIFIER NOT NULL,

        -- FK to AppClasses (Case)
        [AppClassId] UNIQUEIDENTIFIER NOT NULL,

        [SectionName] NVARCHAR(256) NOT NULL,
        [SectionOrder] INT NOT NULL DEFAULT (0),
        [Description] NVARCHAR(MAX) NULL,

        -- Mandatory default fields
        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,

        -- Soft delete support
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppSections_IsDeleted DEFAULT (0),
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2(7) NULL,

        CONSTRAINT [PK_AppSections] PRIMARY KEY CLUSTERED ([Id] ASC),

        CONSTRAINT [FK_AppSections_AppClasses_AppClassId]
            FOREIGN KEY ([AppClassId])
            REFERENCES [dbo].[AppClasses] ([Id])
            ON DELETE CASCADE
    );

    -- Indexes
    CREATE INDEX IX_AppSections_AppClassId ON [dbo].[AppSections] ([AppClassId]);
    CREATE INDEX IX_AppSections_AppClassId_SectionOrder
        ON [dbo].[AppSections] ([AppClassId], [SectionOrder]);
END
GO



IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppSubjects')
BEGIN
    CREATE TABLE [dbo].[AppSubjects] (
        [Id] UNIQUEIDENTIFIER NOT NULL,

        [SubjectName] VARcHAR(MAX) NOT NULL,
        [ClassId] UNIQUEIDENTIFIER NOT NULL,
        [IsOptional] BIT NOT NULL DEFAULT 0,
        
        [UserId] UNIQUEIDENTIFIER NULL,
        [TenantId] UNIQUEIDENTIFIER NULL,

        -- Mandatory default fields
        [ExtraProperties] NVARCHAR(MAX) NOT NULL,
        [ConcurrencyStamp] NVARCHAR(40) NOT NULL,
        [CreationTime] DATETIME2(7) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2(7) NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,

        -- Soft delete support
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppSubjects DEFAULT (0),
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2(7) NULL,

        CONSTRAINT [PK_AppSubjects] PRIMARY KEY CLUSTERED ([Id] ASC),

        -- Foreign Keys
        CONSTRAINT [FK_AppSubjects_AppUsers_UserId]
            FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUsers] ([Id]),

        CONSTRAINT [FK_AppSubjects_AppTenants_TenantId]
            FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants] ([Id])
        
    );

    -- Indexes
    CREATE INDEX IX_AppSubjects_UserId ON [dbo].[AppSubjects] ([UserId]);
    CREATE INDEX IX_AppSubjects_TenantId ON [dbo].[AppSubjects] ([TenantId]);
END
GO
