IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AppApiRequestLogs')
BEGIN
    CREATE TABLE [dbo].[AppApiRequestLogs] (
        [Id] INT IDENTITY(1,1) NOT NULL,

        [Path] NVARCHAR(500) NULL,
        [Method] NVARCHAR(20) NULL,
        [QueryString] NVARCHAR(MAX) NULL,
        [Headers] NVARCHAR(MAX) NULL,
        [Body] NVARCHAR(MAX) NULL,

        [Timestamp] DATETIME2(7) NOT NULL CONSTRAINT DF_AppApiRequestLogs_Timestamp DEFAULT SYSUTCDATETIME(),

        [Succeeded] BIT NOT NULL,
        [StatusCode] INT NOT NULL,

        [ErrorMessage] NVARCHAR(MAX) NULL,
        [InnerException] NVARCHAR(MAX) NULL,
        [StackTrace] NVARCHAR(MAX) NULL,

        [DurationMs] BIGINT NOT NULL,

        [UserId] UNIQUEIDENTIFIER NULL,
        [TenantId] UNIQUEIDENTIFIER NULL,

        CONSTRAINT [PK_AppApiRequestLogs] PRIMARY KEY CLUSTERED ([Id] ASC),

        -- Foreign Keys
        CONSTRAINT [FK_AppApiRequestLogs_AppUsers_UserId]
            FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUsers] ([Id]),

        CONSTRAINT [FK_AppApiRequestLogs_AppTenants_TenantId]
            FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AppTenants] ([Id])
    );

    -- Indexes
    CREATE INDEX IX_AppApiRequestLogs_Timestamp ON [dbo].[AppApiRequestLogs] ([Timestamp]);
    CREATE INDEX IX_AppApiRequestLogs_StatusCode ON [dbo].[AppApiRequestLogs] ([StatusCode]);
    CREATE INDEX IX_AppApiRequestLogs_Succeeded ON [dbo].[AppApiRequestLogs] ([Succeeded]);
    CREATE INDEX IX_AppApiRequestLogs_UserId ON [dbo].[AppApiRequestLogs] ([UserId]);
    CREATE INDEX IX_AppApiRequestLogs_TenantId ON [dbo].[AppApiRequestLogs] ([TenantId]);
END
GO
