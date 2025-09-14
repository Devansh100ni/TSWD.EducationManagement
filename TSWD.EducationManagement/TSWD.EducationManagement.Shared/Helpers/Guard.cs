namespace TSWD.EducationManagement.Shared.Helpers
{
    public static class Guard
    {
        public static void AgainstNull(object? value, string message)
        {
            if (value == null) throw new ArgumentNullException(message);
        }
    }
}
