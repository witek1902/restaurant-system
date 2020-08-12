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
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
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