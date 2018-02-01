namespace OrderManagementSystem.Domain.Order
{
    using User;
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Oznaczenie zamówienia jako 'Przypisane do kelnera'
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
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
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
        /// Dodawanie własnych zależności do komendy.
        /// </summary>
        /// <param name="container">Kontener IoC</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            orderStatusService = container.Resolve<IOrderStatusService>();
        }

        /// <summary>
        /// Sesja NHibernate.
        /// </summary>
        public ISession Session { get; set; }
    }
}