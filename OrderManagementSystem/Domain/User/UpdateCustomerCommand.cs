namespace OrderManagementSystem.Domain.User
{
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Customer;

    /// <summary>
    /// The client update command
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
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Customer Execute()
        {
            var customer = Session.Load<Customer>(customerForm.CustomerId);
            customerBuilder.UpdateCustomerEntity(customer, customerForm);
            Session.Update(customer);

            return customer;
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