namespace OrderManagementSystem.Domain.User
{
    using System.Linq;
    using System.Web.Security;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using WebMatrix.WebData;

    /// <summary>
    /// Tworzenie AppUsera
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
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
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
        /// Dodawanie własnych zależności do komendy.
        /// </summary>
        /// <param name="container">Kontener IoC</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
        }

        /// <summary>
        /// Sesja NHibernate.
        /// </summary>
        public ISession Session { get; set; }
    }
}