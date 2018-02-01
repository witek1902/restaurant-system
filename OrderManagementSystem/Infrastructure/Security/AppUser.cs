namespace OrderManagementSystem.Infrastructure.Security
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Podstawowe informacje o użytkowniku
    /// </summary>
    public interface IAppUser
    {
        /// <summary>
        /// Id
        /// </summary>
        int UserId { get; set; }
        /// <summary>
        /// Login
        /// </summary>
        string Login { get; set; }
        /// <summary>
        /// Hasło
        /// </summary>
        string Password { get; set; }
    }

    /// <summary>
    /// Implementacja podstawowych informacji o użytkowniku
    /// </summary>
    public class AppUser : IAppUser
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual int UserId { get; set; }
        /// <summary>
        /// Login
        /// </summary>
        [Display(Name="Nazwa użytkownika")]
        public virtual string Login { get; set; }
        /// <summary>
        /// Hasło
        /// </summary>
        [Display(Name="Hasło")]
        public virtual string Password { get; set; }

        /// <summary>
        /// Konstruktor dla NHibernate
        /// </summary>
        public AppUser() { }

        /// <summary>
        /// Tworzenie nowego użytkownika
        /// </summary>
        public AppUser(int userId, string login)
        {
            this.UserId = userId;
            this.Login = login;
        }
    }
}