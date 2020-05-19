using System;

namespace Shared.Support.Constants
{
    public static class SystemConst
    {
        public const string SYSTEM_DEFAULT_DATE = "2020-01-01";
        public const string SYSTEM_USER_EMAIL = "system@domain.com";
        public const string SYSTEM_USER = "System";
        public const int SYSTEM_USER_ID = 1;
        
        public const string ADMIN_USER_EMAIL = "admin@domain.com";
        public const string ADMIN_USER = "Administrador";
        public const int ADMIN_USER_PROFILE_ID = 1;

        public static DateTime GetDateDefault()
        {
            return DateTime.Parse(SYSTEM_DEFAULT_DATE);
        }

        public static string[] InternalUsers => new[] { SYSTEM_USER_EMAIL, ADMIN_USER_EMAIL };
    }
}
