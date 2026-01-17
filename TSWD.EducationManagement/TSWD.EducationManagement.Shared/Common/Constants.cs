using System;
using System.Collections.Generic;
using System.Text;

namespace TSWD.EducationManagement.Shared.Common
{
    public static class Constants
    {
        public static class DataErrors
        {
            public const string AccessDenied = "ACCESS_DENIED";
            public const string NotFound = "NOT_FOUND";
            public const string InvalidRequest = "INVALID_REQUEST";
            public const string Unauthorized = "UNAUTHORIZED";
            public const string InternalServerError = "INTERNAL_SERVER_ERROR";
            public const string ChangeConflict = "The resource was modified by another process.";
        }
    }
}
