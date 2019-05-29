namespace OrderManagementSystem.Domain.User
{
    using Infrastructure.Security;
    using NHibernate;
    using Infrastructure.Service;
    using Models.Customer;

    /// <summary>
    /// Builder for clients
    /// </summary>
    public class CustomerBuilder : BusinessService
    {
        /// <summary>
        /// Creates a new service instance, expects to inject an NHibernate session
        /// </summary>
        public CustomerBuilder(ISession session) : base(session)
        {
        }

        /// <summary>
        /// Creating a client
        /// </summary>
        /// <param name="customerForm"></param>
        /// <returns></returns>
        public Customer ConstructCustomerEntity(CustomerForm customerForm)
        {
            var customer = new Customer
            {
                Firstname = customerForm.Firstname,
                AppUser = new AppUser
                {
                    UserId = customerForm.AppUserId
                }
            };

            return customer;
        }

        /// <summary>
        /// Client update from model view
        /// </summary>
        /// <param name="customer">entity</param>
        /// <param name="customerForm">Form</param>
        public void UpdateCustomerEntity(Customer customer, CustomerForm customerForm)
        {
            customer.Firstname = customerForm.Firstname;
        }
    }
}