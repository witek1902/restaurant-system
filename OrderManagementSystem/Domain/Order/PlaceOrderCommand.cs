namespace OrderManagementSystem.Domain.Order
{
    using System.Linq;
    using System;
    using OrderItem;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Order;

    /// <summary>
    /// Command for ordering (after the initial selection of products)
    /// </summary>
    public class PlaceOrderCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly OrderForm orderForm;
        private IOrderItemStatusService orderItemStatusService;

        public PlaceOrderCommand(OrderForm orderForm)
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

            order.Comments = orderForm.OrderComments;
            order.TableNumber = orderForm.TableNumber;

            foreach (var orderItem in order.OrderItems.Where(x => x.OrderItemStatus == OrderItemStatus.New).ToList())
                orderItemStatusService.ApproveOrderItem(orderItem);

            Session.Update(order);

            return order.Id;
        }

        /// <summary>
        /// Adding custom dependencies to the command.
        /// </summary>
        /// <param name="container">IoC container</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            orderItemStatusService = container.Resolve<IOrderItemStatusService>();
        }

        /// <summary>
        /// NHibernate session.
        /// </summary>
        public ISession Session { get; set; }
    }
}