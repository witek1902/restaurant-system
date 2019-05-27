namespace OrderManagementSystem.Infrastructure.Security
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISecurityProvider
    {
        /// <summary>
        /// Sprawdza czy aktualny użytkownik jest zautoryzowany
        /// </summary>
        /// <returns>Prawda, jeśli aktualny użytkownik jest zautoryzowany</returns>
        bool IsUserAuthenticated();

        /// <summary>
        /// Dane aktualnego użytkownika
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
        /// Sprawdza czy użytkownik posiada przekazaną rolę
        /// </summary>
        bool IsUserInRole(string role);

        /// <summary>
        /// Zwraca wszystkie role użytkownika
        /// </summary>
        /// <returns></returns>
        string[] GetRolesForUser();

        /// <summary>
        /// Log in za pomocą nazwy użytkownika i hasła
        /// </summary>
        /// <param name="userName">Login</param>
        /// <param name="password">Password </param>
        /// <param name="persistSecurityCookie">Czy przechowywać ciasteczka</param>
        /// <returns>Prawda, jeśli OK</returns>
        bool Login(string userName, string password, bool persistSecurityCookie = false);

        /// <summary>
        /// Wylogowywanie użytkownika
        /// </summary>
        void Logout();
    }
}