namespace TSWD.EducationManagement.Permissions
{
    public class PermissionConstent
    {
        public const string GroupName = "EducationManagement";

        public static class Users
        {
            public const string Default = GroupName + ".Users";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Students
        {
            public const string Default = GroupName + ".Students";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

    }
}
