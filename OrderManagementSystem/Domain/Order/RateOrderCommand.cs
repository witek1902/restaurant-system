namespace OrderManagementSystem.Domain.Order
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Order;

    /// <summary>
    /// Command that supports order evaluation
    /// </summary>
    public class RateOrderCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly RateOrderForm orderForm;

        public RateOrderCommand(RateOrderForm orderForm)
        {
            this.orderForm = orderForm;
        }

        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Guid Execute()
        {
            var order = Session.Load<Order>(orderForm.OrderId);

            order.Rate = orderForm.RateStars;
            order.RateDetails = orderForm.RateDetails;

            Session.Save(order);

            return order.Id;
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