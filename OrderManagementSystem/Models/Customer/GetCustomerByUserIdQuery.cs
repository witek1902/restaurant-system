namespace OrderManagementSystem.Models.Customer
{
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading the client after the currently logged in user's id
    /// </summary>
    public class GetCustomerByUserIdQuery : Query<CustomerForm>
    {
        private readonly int userId;

        public GetCustomerByUserIdQuery(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
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