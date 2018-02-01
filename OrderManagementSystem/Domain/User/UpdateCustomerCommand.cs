namespace OrderManagementSystem.Domain.User
{
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Customer;

    /// <summary>
    /// Komenda do aktualizacji klienta
    /// </summary>
    public class UpdateCustomerCommand : Command<Customer>, INeedSession, INeedAutocommitTransaction
    {
        private readonly CustomerForm customerForm;
        private CustomerBuilder customerBuilder;

        public UpdateCustomerCommand(CustomerForm customerForm)
        {
            this.customerForm = customerForm;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Customer Execute()
        {
            var customer = Session.Load<Customer>(customerForm.CustomerId);
            customerBuilder.UpdateCustomerEntity(customer, customerForm);
            Session.Update(customer);

            return customer;
        }

        /// <summary>
        /// Dodawanie własnych zależności do komendy.
        /// </summary>
        /// <param name="container">Kontener IoC</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            customerBuilder = container.Resolve<CustomerBuilder>();
        }

        /// <summary>
        /// Sesja NHibernate.
        /// </summary>
        public ISession Session { get; set; }
    }
}