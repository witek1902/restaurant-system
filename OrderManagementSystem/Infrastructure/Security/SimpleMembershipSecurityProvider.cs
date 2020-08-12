namespace OrderManagementSystem.Infrastructure.Security
{
    using System.Web.Security;
    using WebMatrix.Data;
    using WebMatrix.WebData;

    /// <summary>
    /// Implementation of ISecurityProcider using SimpleMembershipProvider
    /// </summary>
    public class SimpleMembershipSecurityProvider : ISecurityProvider
    {
        /// <summary>
        /// Checks whether the current user is
        /// </summary>
        /// <returns>true if current user is uthenticated</returns>
        public bool IsUserAuthenticated()
        {
            return WebSecurity.IsAuthenticated;
        }

        /// <summary>
        /// Gives access to current user data
        /// </summary>
        public IAppUser CurrentUser
        {
            get
            {
                using (var db = Database.Open("OMSDb"))
                {
                    var usr = db.QuerySingle("SELECT UserId, Login FROM [dbo].[AppUser]  WHERE UserId = @0 ", CurrentUserId);
                    return new AppUser(CurrentUserId, CurrentUserName);
                }
            }
        }

        /// <summary>
        /// id of current user
        /// </summary>
        public int CurrentUserId
        {
            get { return WebSecurity.CurrentUserId; }
        }

        /// <summary>
        /// Name of current user
        /// </summary>
        public string CurrentUserName
        {
            get
            {
                var currentUserName = WebSecurity.CurrentUserName;
                return !string.IsNullOrWhiteSpace(currentUserName) ? currentUserName : "Anonymous";
            }
        }

        /// <summary>
        /// Checks if current user is in given role
        /// </summary>
        public bool IsUserInRole(string role)
        {
            return Roles.IsUserInRole(role);
        }

        /// <summary>
        /// Return all roles for current user
        /// </summary>
        /// <returns></returns>
        public string[] GetRolesForUser()
        {
            return Roles.GetRolesForUser();
        }

        /// <summary>
        /// Performs login for given username and password
        /// </summary>
        /// <param name="userName">login</param>
        /// <param name="password">password</param>
        /// <param name="persistSecurityCookie">persist security cookie</param>
        /// <returns>true if login succesfull otherwise false</returns>
        public bool Login(string userName, string password, bool persistSecurityCookie = false)
        {
            return WebSecurity.Login(userName, password, persistSecurityCookie);
        }

        /// <summary>
        /// Logs user out
        /// </summary>
        public void Logout()
        {
            WebSecurity.Logout();
        }
    }
}