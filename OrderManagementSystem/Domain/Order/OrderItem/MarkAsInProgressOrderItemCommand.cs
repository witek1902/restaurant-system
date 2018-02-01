namespace OrderManagementSystem.Domain.Order.OrderItem
{
    using User;
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Oznaczenie elementu zamówienia jako 'W przygotowaniu'
    /// </summary>
    public class MarkAsInProgressOrderItemCommand : Command<OrderItem>, INeedSession, INeedAutocommitTransaction
    {
        private readonly Guid orderItemId;
        private readonly Guid restaurantWorkerId;
        private IOrderItemStatusService orderItemStatusService;

        public MarkAsInProgressOrderItemCommand(Guid orderItemId, Guid restaurantWorkerId)
        {
            this.orderItemId = orderItemId;
            this.restaurantWorkerId = restaurantWorkerId;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override OrderItem Execute()
        {
            var orderItem = Session.Load<OrderItem>(orderItemId);
            orderItemStatusService.InProgressOrderItem(orderItem);
            if(orderItem.Order.Cook == null)
                orderItem.Order.Cook = new RestaurantWorker
                {
                    Id = restaurantWorkerId
                };

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