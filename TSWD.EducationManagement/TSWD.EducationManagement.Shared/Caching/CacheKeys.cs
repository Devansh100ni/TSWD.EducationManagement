namespace TSWD.EducationManagement.Shared.Caching
{
    public class CacheKeys
    {
        private static string defaultKey = "DEFAULT";
        public static string SchoolGeneralSettings(Guid? tenantId = null)
        {
            return tenantId.HasValue ? $"SCHOOL_GENERAL_SETTINGS_{tenantId}" : $"SCHOOL_GENERAL_SETTINGS_{defaultKey}";
        }


    }
}
