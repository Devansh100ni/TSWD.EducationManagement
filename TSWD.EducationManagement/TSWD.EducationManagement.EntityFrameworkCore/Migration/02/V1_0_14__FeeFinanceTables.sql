IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppFeeTypes')
BEGIN
    CREATE TABLE [dbo].[AppFeeTypes] (
        [Id] UNIQUEIDENTIFIER NOT NULL,
        [FeeName] NVARCHAR (MAX) NOT NULL,
        [Frequency] NVARCHAR (MAX) NOT NULL,
        [UserId] UNIQUEIDENTIFIER NULL,
        [TenantId] UNIQUEIDENTIFIER NULL,
        [ExtraProperties] NVARCHAR (MAX) NULL,
        [ConcurrencyStamp] NVARCHAR (40) NULL,
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [CreationTime] DATETIME2 (7) NOT NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2 (7) NULL,
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2 (7) NULL,
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppFeeTypes_IsDeleted DEFAULT (0),

        CONSTRAINT [PK_AppFeeTypes] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_AppFeeTypes_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUsers] ([Id]),
        CONSTRAINT [FK_AppFeeTypes_AppTenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants] ([Id])
    );
    CREATE INDEX IX_AppFeeTypes_UserId ON [dbo].[AppFeeTypes] ([UserId]);
    CREATE INDEX IX_AppFeeTypes_TenantId ON [dbo].[AppFeeTypes] ([TenantId]);
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppFineRules')
BEGIN
    CREATE TABLE [dbo].[AppFineRules] (
        [Id] UNIQUEIDENTIFIER NOT NULL,
        [FineType] NVARCHAR (MAX) NOT NULL,
        [Value] DECIMAL(18, 2) NOT NULL,
        [UserId] UNIQUEIDENTIFIER NULL,
        [TenantId] UNIQUEIDENTIFIER NULL,
        [ExtraProperties] NVARCHAR (MAX) NULL,
        [ConcurrencyStamp] NVARCHAR (40) NULL,
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [CreationTime] DATETIME2 (7) NOT NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2 (7) NULL,
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2 (7) NULL,
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppFineRules_IsDeleted DEFAULT (0),

        CONSTRAINT [PK_AppFineRules] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_AppFineRules_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUsers] ([Id]),
        CONSTRAINT [FK_AppFineRules_AppTenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants] ([Id])
    );
    CREATE INDEX IX_AppFineRules_UserId ON [dbo].[AppFineRules] ([UserId]);
    CREATE INDEX IX_AppFineRules_TenantId ON [dbo].[AppFineRules] ([TenantId]);
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppFeeReminders')
BEGIN
    CREATE TABLE [dbo].[AppFeeReminders] (
        [Id] UNIQUEIDENTIFIER NOT NULL,
        [ReminderFrequencyDays] INT NOT NULL,
        [IsActive] BIT NOT NULL CONSTRAINT DF_AppFeeReminders_IsActive DEFAULT (1),
        [UserId] UNIQUEIDENTIFIER NULL,
        [TenantId] UNIQUEIDENTIFIER NULL,
        [ExtraProperties] NVARCHAR (MAX) NULL,
        [ConcurrencyStamp] NVARCHAR (40) NULL,
        [CreatorId] UNIQUEIDENTIFIER NULL,
        [CreationTime] DATETIME2 (7) NOT NULL,
        [LastModifierId] UNIQUEIDENTIFIER NULL,
        [LastModificationTime] DATETIME2 (7) NULL,
        [DeleterId] UNIQUEIDENTIFIER NULL,
        [DeletionTime] DATETIME2 (7) NULL,
        [IsDeleted] BIT NOT NULL CONSTRAINT DF_AppFeeReminders_IsDeleted DEFAULT (0),

        CONSTRAINT [PK_AppFeeReminders] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_AppFeeReminders_AppUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUsers] ([Id]),
        CONSTRAINT [FK_AppFeeReminders_AppTenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants] ([Id])
    );
    CREATE INDEX IX_AppFeeReminders_UserId ON [dbo].[AppFeeReminders] ([UserId]);
    CREATE INDEX IX_AppFeeReminders_TenantId ON [dbo].[AppFeeReminders] ([TenantId]);
END
GO
