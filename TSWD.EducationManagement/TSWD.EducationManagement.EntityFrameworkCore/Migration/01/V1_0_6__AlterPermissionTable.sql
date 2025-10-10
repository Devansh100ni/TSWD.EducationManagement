IF NOT EXISTS (
    SELECT 1
    FROM sys.columns c
    JOIN sys.objects o ON c.object_id = o.object_id
    WHERE o.name = 'AppPermissions'
      AND o.type = 'U'
      AND c.name = 'DisplayName'
      AND c.is_nullable = 0
)
BEGIN
    ALTER TABLE [AppPermissions]
    ADD [DisplayName] varchar(MAX) NULL;
END