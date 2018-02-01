namespace OrderManagementSystem.Domain.User
{
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    public class GetCustomerByNameQuery : Query<Customer>
    {
        private string customerLogin;

        public GetCustomerByNameQuery(string customerLogin)
        {
            this.customerLogin = customerLogin;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
        public override Customer Execute(ISession session)
        {
            return session
                .CreateQuery("from AppUser a where a.Login = :login")
                .SetString("login", customerLogin)
                .List<Customer>()
                .FirstOrDefault();
        }
    }
}