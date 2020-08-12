namespace OrderManagementSystem.Domain.Order
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Order;

    /// <summary>
    /// Order creation
    /// </summary>
    public class CreateOrderCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly OrderForm orderForm;
        private OrderBuilder orderBuilder;

        public CreateOrderCommand(OrderForm orderForm)
        {
            this.orderForm = orderForm;
        }

        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Guid Execute()
        {
            var order = orderBuilder.ConstructOrderEntity(orderForm);

            Session.Save(order);

            return order.Id;
        }

        /// <summary>
        /// Adding custom dependencies to the command.
        /// </summary>
        /// <param name="container">IoC container</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            orderBuilder = container.Resolve<OrderBuilder>();
        }

        /// <summary>
        /// NHibernate session.
        /// </summary>
        public ISession Session { get; set; }
    }
}