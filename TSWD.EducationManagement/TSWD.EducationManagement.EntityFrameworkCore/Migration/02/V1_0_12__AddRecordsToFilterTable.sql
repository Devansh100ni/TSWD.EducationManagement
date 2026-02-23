ALTER TABLE dbo.AppFilter
ALTER COLUMN FilterKey varchar(50) NOT NULL;

ALTER TABLE dbo.AppFilter
ALTER COLUMN FilterValue varchar(100) NOT NULL;

-- GREATER THAN OR EQUAL
IF NOT EXISTS (
    SELECT 1 FROM dbo.AppFilter 
    WHERE FilterKey = '>=' AND FilterValue = 'IS GREATER THAN OR EQUAL TO'
)
BEGIN
    INSERT [dbo].[AppFilter] ([Id], [FilterKey], [FilterValue], [UserId], [TenantId], [ExtraProperties], [ConcurrencyStamp], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime])
    VALUES (NEWID(), N'>=', N'IS GREATER THAN OR EQUAL TO', NULL, NULL, N'{}', NEWID(), NULL, NULL, NULL, 0, NULL, NULL)
END

-- LESS THAN OR EQUAL
IF NOT EXISTS (
    SELECT 1 FROM dbo.AppFilter 
    WHERE FilterKey = '<=' AND FilterValue = 'IS LESS THAN OR EQUAL TO'
)
BEGIN
    INSERT [dbo].[AppFilter] ([Id], [FilterKey], [FilterValue], [UserId], [TenantId], [ExtraProperties], [ConcurrencyStamp], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime])
    VALUES (NEWID(), N'<=', N'IS LESS THAN OR EQUAL TO', NULL, NULL, N'{}', NEWID(), NULL, NULL, NULL, 0, NULL, NULL)
END

-- CONTAINS
IF NOT EXISTS (
    SELECT 1 FROM dbo.AppFilter 
    WHERE FilterKey = 'LIKE' AND FilterValue = 'CONTAINS'
)
BEGIN
    INSERT [dbo].[AppFilter] ([Id], [FilterKey], [FilterValue], [UserId], [TenantId], [ExtraProperties], [ConcurrencyStamp], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime])
    VALUES (NEWID(), N'LIKE', N'CONTAINS', NULL, NULL, N'{}', NEWID(), NULL, NULL, NULL, 0, NULL, NULL)
END

-- DOES NOT CONTAIN
IF NOT EXISTS (
    SELECT 1 FROM dbo.AppFilter 
    WHERE FilterKey = 'NOT LIKE' AND FilterValue = 'DOES NOT CONTAIN'
)
BEGIN
    INSERT [dbo].[AppFilter] ([Id], [FilterKey], [FilterValue], [UserId], [TenantId], [ExtraProperties], [ConcurrencyStamp], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime])
    VALUES (NEWID(), N'NOT LIKE', N'DOES NOT CONTAIN', NULL, NULL, N'{}', NEWID(), NULL, NULL, NULL, 0, NULL, NULL)
END

-- IN LIST
IF NOT EXISTS (
    SELECT 1 FROM dbo.AppFilter 
    WHERE FilterKey = 'IN' AND FilterValue = 'IN LIST'
)
BEGIN
    INSERT [dbo].[AppFilter] ([Id], [FilterKey], [FilterValue], [UserId], [TenantId], [ExtraProperties], [ConcurrencyStamp], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime])
    VALUES (NEWID(), N'IN', N'IN LIST', NULL, NULL, N'{}', NEWID(), NULL, NULL, NULL, 0, NULL, NULL)
END

-- NOT IN LIST
IF NOT EXISTS (
    SELECT 1 FROM dbo.AppFilter 
    WHERE FilterKey = 'NOT IN' AND FilterValue = 'NOT IN LIST'
)
BEGIN
    INSERT [dbo].[AppFilter] ([Id], [FilterKey], [FilterValue], [UserId], [TenantId], [ExtraProperties], [ConcurrencyStamp], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime])
    VALUES (NEWID(), N'NOT IN', N'NOT IN LIST', NULL, NULL, N'{}', NEWID(), NULL, NULL, NULL, 0, NULL, NULL)
END

-- STARTS WITH
IF NOT EXISTS (
    SELECT 1 FROM dbo.AppFilter 
    WHERE FilterKey = 'STARTS WITH' AND FilterValue = 'STARTS WITH'
)
BEGIN
    INSERT [dbo].[AppFilter] ([Id], [FilterKey], [FilterValue], [UserId], [TenantId], [ExtraProperties], [ConcurrencyStamp], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime])
    VALUES (NEWID(), N'STARTS WITH', N'STARTS WITH', NULL, NULL, N'{}', NEWID(), NULL, NULL, NULL, 0, NULL, NULL)
END

-- ENDS WITH
IF NOT EXISTS (
    SELECT 1 FROM dbo.AppFilter 
    WHERE FilterKey = 'ENDS WITH' AND FilterValue = 'ENDS WITH'
)
BEGIN
    INSERT [dbo].[AppFilter] ([Id], [FilterKey], [FilterValue], [UserId], [TenantId], [ExtraProperties], [ConcurrencyStamp], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime])
    VALUES (NEWID(), N'ENDS WITH', N'ENDS WITH', NULL, NULL, N'{}', NEWID(), NULL, NULL, NULL, 0, NULL, NULL)
END

-- IS NULL
IF NOT EXISTS (
    SELECT 1 FROM dbo.AppFilter 
    WHERE FilterKey = 'IS NULL' AND FilterValue = 'IS NULL'
)
BEGIN
    INSERT [dbo].[AppFilter] ([Id], [FilterKey], [FilterValue], [UserId], [TenantId], [ExtraProperties], [ConcurrencyStamp], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime])
    VALUES (NEWID(), N'IS NULL', N'IS NULL', NULL, NULL, N'{}', NEWID(), NULL, NULL, NULL, 0, NULL, NULL)
END

-- IS NOT NULL
IF NOT EXISTS (
    SELECT 1 FROM dbo.AppFilter 
    WHERE FilterKey = 'IS NOT NULL' AND FilterValue = 'IS NOT NULL'
)
BEGIN
    INSERT [dbo].[AppFilter] ([Id], [FilterKey], [FilterValue], [UserId], [TenantId], [ExtraProperties], [ConcurrencyStamp], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime])
    VALUES (NEWID(), N'IS NOT NULL', N'IS NOT NULL', NULL, NULL, N'{}', NEWID(), NULL, NULL, NULL, 0, NULL, NULL)
END

-- Create Unique Index
IF NOT EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE name = 'UX_AppFilter_FilterKey_FilterValue'
      AND object_id = OBJECT_ID('dbo.AppFilter')
)
BEGIN
    CREATE UNIQUE INDEX UX_AppFilter_FilterKey_FilterValue
    ON dbo.AppFilter (FilterKey, FilterValue)
    WHERE IsDeleted = 0;
END