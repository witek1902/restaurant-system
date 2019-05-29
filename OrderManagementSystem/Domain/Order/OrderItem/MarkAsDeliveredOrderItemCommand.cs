namespace OrderManagementSystem.Domain.Order.OrderItem
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Oznaczenie elementu zamówienia jako Dostarczony
    /// </summary>
    public class MarkAsDeliveredOrderItemCommand : Command<OrderItem>, INeedSession, INeedAutocommitTransaction
    {
        private readonly Guid orderItemId;
        private IOrderItemStatusService orderItemStatusService;

        public MarkAsDeliveredOrderItemCommand(Guid orderItemId)
        {
            this.orderItemId = orderItemId;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override OrderItem Execute()
        {
            var orderItem = Session.Load<OrderItem>(orderItemId);
            orderItemStatusService.DeliveredOrderItem(orderItem);
            Session.Update(orderItem);
            return orderItem;
        }

        /// <summary>
        /// Dodawanie własnych zależności do komendy.
        /// </summary>
        /// <param name="container">Kontener IoC</param>
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