namespace OrderManagementSystem.Infrastructure.Security
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISecurityProvider
    {
        /// <summary>
        /// Checks whether the current user is authorized
        /// </summary>
        /// <returns>True, if the current user is authorized</returns>
        bool IsUserAuthenticated();

        /// <summary>
        /// Data of the current user
        /// </summary>
        IAppUser CurrentUser { get; }

        /// <summary>
        /// Id
        /// </summary>
        int CurrentUserId { get; }

        /// <summary>
        /// Name
        /// </summary>
        string CurrentUserName { get; }

        /// <summary>
        ///Checks whether the user has a delegated role
        /// </summary>
        bool IsUserInRole(string role);

        /// <summary>
        /// Returns all user roles
        /// </summary>
        /// <returns></returns>
        string[] GetRolesForUser();

        /// <summary>
        /// Log in with username and password
        /// </summary>
        /// <param name="userName">Login</param>
        /// <param name="password">Password </param>
        /// <param name="persistSecurityCookie">Do you store cookies</param>
        /// <returns>True, if OK</returns>
        bool Login(string userName, string password, bool persistSecurityCookie = false);

        /// <summary>
        /// User logout
        /// </summary>
        void Logout();
    }
}