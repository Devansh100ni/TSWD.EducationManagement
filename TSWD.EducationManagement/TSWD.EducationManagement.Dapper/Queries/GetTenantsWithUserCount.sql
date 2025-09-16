-- Get paginated tenant list with user count
SELECT t.Id, 
       t.Name, 
       t.NormalizedName,
       ISNULL(COUNT(u.Id), 0) AS UserCount
FROM AppTenants t
LEFT JOIN AppUsers u ON u.TenantId = t.Id
GROUP BY t.Id, t.Name, t.NormalizedName
ORDER BY t.Name
OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;

-- Get total count for pagination
SELECT COUNT(*) FROM AppTenants;