namespace OrderManagementSystem.Domain.User
{
    using Infrastructure.Security;
    using NHibernate;
    using Infrastructure.Service;
    using Models.Customer;

    /// <summary>
    /// Builder dla klientów
    /// </summary>
    public class CustomerBuilder : BusinessService
    {
        /// <summary>
        /// Tworzy nową instancje usługi, oczekuje wstrzyknięcia sesji NHibernate
        /// </summary>
        public CustomerBuilder(ISession session) : base(session)
        {
        }

        /// <summary>
        /// Tworzenie klienta
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
        /// Aktualizacja klienta z view modelu
        /// </summary>
        /// <param name="customer">Encja</param>
        /// <param name="customerForm">Formularz</param>
        public void UpdateCustomerEntity(Customer customer, CustomerForm customerForm)
        {
            customer.Firstname = customerForm.Firstname;
        }
    }
}