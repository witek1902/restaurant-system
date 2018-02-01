namespace OrderManagementSystem.Domain.User
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Customer;

    /// <summary>
    /// Tworzenie klienta
    /// </summary>
    public class CreateCustomerCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly CustomerForm customerForm;
        private CustomerBuilder customerBuilder;

        public CreateCustomerCommand(CustomerForm customerForm)
        {
            this.customerForm = customerForm;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Guid Execute()
        {
            var customer = customerBuilder.ConstructCustomerEntity(customerForm);
            Session.Save(customer);

            return customer.Id;
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