namespace OrderManagementSystem.Domain.Order
{
    using User;
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Marking the order as 'Assigned to the waiter'
    /// </summary>
    public class AssignOrderCommand : Command<Order>, INeedSession, INeedAutocommitTransaction
    {
        private readonly Guid orderId;
        private readonly Guid restaurantWorkerId;
        private IOrderStatusService orderStatusService;

        public AssignOrderCommand(Guid orderId, Guid restaurantWorkerId)
        {
            this.orderId = orderId;
            this.restaurantWorkerId = restaurantWorkerId;
        }

        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Order Execute()
        {
            var order = Session.Load<Order>(orderId);
            order.Waiter = new RestaurantWorker
            {
                Id = restaurantWorkerId
            };
            orderStatusService.AssignToWaiter(order);
            Session.Update(order);

            return order;
        }

        /// <summary>
        /// Adding custom dependencies to the command.
        /// </summary>
        /// <param name="container">IoC container</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            orderStatusService = container.Resolve<IOrderStatusService>();
        }

        /// <summary>
        /// NHibernate session.
        /// </summary>
        public ISession Session { get; set; }
    }
}