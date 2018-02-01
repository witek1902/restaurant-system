namespace OrderManagementSystem.Domain.Order.OrderItem
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Oznaczenie elementu zamówienia jako Gotowy
    /// </summary>
    public class MarkAsReadyOrderItemCommand : Command<OrderItem>, INeedSession, INeedAutocommitTransaction
    {
        private readonly Guid orderItemId;
        private IOrderItemStatusService orderItemStatusService;

        public MarkAsReadyOrderItemCommand(Guid orderItemId)
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
            orderItemStatusService.ReadyOrderItem(orderItem);
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
        /// Sesja NHibernate.
        /// </summary>
        public ISession Session { get; set; }
    }
}