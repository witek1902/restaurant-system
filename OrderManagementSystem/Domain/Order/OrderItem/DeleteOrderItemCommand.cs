namespace OrderManagementSystem.Domain.Order.OrderItem
{
    using System;
    using System.Linq;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Infrastructure.ExtensionMethods;

    /// <summary>
    /// Usunięcie elementu zamówienia
    /// </summary>
    public class DeleteOrderItemCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly Guid orderItemId;

        public DeleteOrderItemCommand(Guid orderItemId)
        {
            this.orderItemId = orderItemId;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Guid Execute()
        {
            var orderId = Session
                .CreateQuery("select o.Id from Order o join o.OrderItems oi where oi.Id = :orderItemId")
                .SetGuid("orderItemId", orderItemId)
                .List<Guid>()
                .Single();

            Session.Delete<OrderItem>(orderItemId);

            return orderId;
        }

        /// <summary>
        /// Dodawanie własnych zależności do komendy.
        /// </summary>
        /// <param name="container">Kontener IoC</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
        }

        /// <summary>
        /// Sesja NHibernate.
        /// </summary>
        public ISession Session { get; set; }
    }
}