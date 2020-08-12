namespace OrderManagementSystem.Domain.User
{
    using System.Linq;
    using System.Web.Security;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using WebMatrix.WebData;

    /// <summary>
    /// Creating AppUser
    /// </summary>
    public class CreateAppUserCommand : Command<int>, INeedSession, INeedAutocommitTransaction
    {
        private readonly string login;
        private readonly string password;
        private readonly string role;

        public CreateAppUserCommand(string login, string password, string role)
        {
            this.login = login;
            this.password = password;
            this.role = role;
        }

        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override int Execute()
        {
            WebSecurity.CreateUserAndAccount(login, password);
            Roles.AddUserToRole(login, role);

            return Session
                .CreateQuery("select a.UserId from AppUser a where a.Login = :login")
                .SetString("login", login)
                .List<int>()
                .Single();
        }

        /// <summary>
        /// Adding custom dependencies to the command.
        /// </summary>
        /// <param name="container">IoC container</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
        }

        /// <summary>
        /// NHibernate session.
        /// </summary>
        public ISession Session { get; set; }
    }
}