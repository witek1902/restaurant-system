namespace OrderManagementSystem.Models.Customer
{
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie klienta po id aktualnie zalogowane użytkownika
    /// </summary>
    public class GetCustomerByUserIdQuery : Query<CustomerForm>
    {
        private readonly int userId;

        public GetCustomerByUserIdQuery(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
        public override CustomerForm Execute(ISession session)
        {
            var customer = session
                .CreateQuery("from Customer c where c.AppUser.UserId = :userId")
                .SetInt32("userId", userId)
                .List<Domain.User.Customer>()
                .Single();

            return CustomerMapper.MapToForm(customer);
        }
    }
}