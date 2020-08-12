namespace OrderManagementSystem.Domain.User
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Customer;

    /// <summary>
    /// Creating a client
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
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Guid Execute()
        {
            var customer = customerBuilder.ConstructCustomerEntity(customerForm);
            Session.Save(customer);

            return customer.Id;
        }

        /// <summary>
        /// Adding custom dependencies to the command.
        /// </summary>
        /// <param name="container">IoC container</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            customerBuilder = container.Resolve<CustomerBuilder>();
        }

        /// <summary>
        /// NHibernate session.
        /// </summary>
        public ISession Session { get; set; }
    }
}