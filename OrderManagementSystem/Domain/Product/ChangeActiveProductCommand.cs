namespace OrderManagementSystem.Domain.Product
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Changes the activity of the product (whether it is only historical or not)
    /// </summary>
    public class ChangeActiveProductCommand : Command<bool>, INeedSession, INeedAutocommitTransaction
    {
        private readonly Guid productId;
        private readonly bool active;

        public ChangeActiveProductCommand(Guid productId, bool active)
        {
            this.productId = productId;
            this.active = active;
        }

        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override bool Execute()
        {
            var product = Session.Load<Product>(productId);
            product.Active = active;
            Session.Update(product);

            return true;
        }

        /// <summary>
        /// Adding custom dependencies to the command.
        /// </summary>
        /// <param name="container">IoC container</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
        }

        /// <summary>
        /// NHibernate session.
        /// </summary>
        public ISession Session { get; set; }
    }
}