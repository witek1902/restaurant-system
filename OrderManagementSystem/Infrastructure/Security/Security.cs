namespace OrderManagementSystem.Infrastructure.Security
{
    /// <summary>
    /// Helper class that provides access to security features
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// Tells if current user is authenticated
        /// </summary>
        /// <returns>true if current user is uthenticated</returns>
        public static bool IsUserAuthenticated()
        {
            return GetSecurityProvider().IsUserAuthenticated();
        }

        /// <summary>
        /// Gives access to current user data
        /// </summary>
        public static IAppUser CurrentUser
        {
            get { return GetSecurityProvider().CurrentUser; }
        }

        /// <summary>
        /// id of current user
        /// </summary>
        public static int CurrentUserId
        {
            get { return GetSecurityProvider().CurrentUserId; }
        }

        /// <summary>
        /// Name of current user
        /// </summary>
        public static string CurrentUserName
        {
            get { return GetSecurityProvider().CurrentUserName; }
        }

        /// <summary>
        /// Checks if current user is in given role
        /// </summary>
        public static bool IsUserInRole(string role)
        {
            return GetSecurityProvider().IsUserInRole(role);
        }

        /// <summary>
        /// Return all roles for current user
        /// </summary>
        /// <returns></returns>
        public static string[] GetRolesForUser()
        {
            return GetSecurityProvider().GetRolesForUser();
        }

        /// <summary>
        /// Performs login for given username and password
        /// </summary>
        /// <param name="userName">login</param>
        /// <param name="password">password</param>
        /// <param name="persistSecurityCookie">persist security cookie</param>
        /// <returns>true if login succesfull otherwise false</returns>
        public static bool Login(string userName, string password, bool persistSecurityCookie = false)
        {
            return GetSecurityProvider().Login(userName, password, persistSecurityCookie);
        }

        /// <summary>
        /// Logs user out
        /// </summary>
        public static void Logout()
        {
            GetSecurityProvider().Logout();
        }

        private static ISecurityProvider GetSecurityProvider()
        {
            return MvcApplication.Container.Resolve<ISecurityProvider>();
        }
    }
}