namespace TSWD.EducationManagement.Dapper
{
    public static class SqlResourceHelper
    {
        public static async Task<string> GetSqlAsync<T>(string sqlFileName)
        {
            var assembly = typeof(T).Assembly;

            // Build full resource name dynamically
            var resourceName = assembly
                .GetManifestResourceNames()
                .FirstOrDefault(r => r.EndsWith(sqlFileName, StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
                throw new Exception($"Embedded SQL resource '{sqlFileName}' not found in assembly '{assembly.FullName}'.");

            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream!);
            return await reader.ReadToEndAsync();
        }
    }
}
