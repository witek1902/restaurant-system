namespace OrderManagementSystem.Domain.Order
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Infrastructure.ExtensionMethods;

    /// <summary>
    /// Usuniecie zamówienia
    /// </summary>
    public class DeleteOrderCommand : Command<bool>, INeedSession, INeedAutocommitTransaction
    {
        private readonly Guid orderId;

        public DeleteOrderCommand(Guid orderId)
        {
            this.orderId = orderId;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
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