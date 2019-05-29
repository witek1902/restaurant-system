namespace OrderManagementSystem.Infrastructure.Security
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Basic information about the user
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
        /// Password 
        /// </summary>
        string Password { get; set; }
    }

    /// <summary>
    /// Implementation of basic information about the user
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
        [Display(Name="User Name")]
        public virtual string Login { get; set; }
        /// <summary>
        /// Password 
        /// </summary>
        [Display(Name="Password")]
        public virtual string Password { get; set; }

        /// <summary>
        ///Constructor for NHibernate
        /// </summary>
        public AppUser() { }

        /// <summary>
        /// Creating a new user
        /// </summary>
        public AppUser(int userId, string login)
        {
            this.UserId = userId;
            this.Login = login;
        }
    }
}