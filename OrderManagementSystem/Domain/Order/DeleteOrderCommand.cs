namespace OrderManagementSystem.Domain.Order
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Infrastructure.ExtensionMethods;

    /// <summary>
    /// Delete the order
    /// </summary>
    public class DeleteOrderCommand : Command<bool>, INeedSession, INeedAutocommitTransaction
    {
        private readonly Guid orderId;

        public DeleteOrderCommand(Guid orderId)
        {
            this.orderId = orderId;
        }

        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override bool Execute()
        {
            Session
                .CreateQuery("delete from OrderItem oi where oi.Order.Id = :orderId")
                .SetGuid("orderId", orderId)
                .ExecuteUpdate();

            Session.Delete<Order>(orderId);

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