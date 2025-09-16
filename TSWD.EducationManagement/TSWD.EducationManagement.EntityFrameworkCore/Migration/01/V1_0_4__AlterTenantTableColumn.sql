IF EXISTS (
    SELECT 1
    FROM sys.columns c
    JOIN sys.objects o ON c.object_id = o.object_id
    WHERE o.name = 'AppTenants'
      AND o.type = 'U'
      AND c.name = 'EntityVersion'
      AND c.is_nullable = 0
)
BEGIN
    ALTER TABLE [EducationDb].[dbo].[AppTenants]
    ALTER COLUMN [EntityVersion] INT NULL;
END