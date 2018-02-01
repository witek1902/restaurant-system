namespace OrderManagementSystem.Domain.Order.OrderItem
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Order;

    /// <summary>
    /// Zmiana ilości elementu zamówienia
    /// </summary>
    public class ChangeQuantityOrderItemCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly OrderItemForm orderItemForm;

        public ChangeQuantityOrderItemCommand(OrderItemForm orderItemForm)
        {
            this.orderItemForm = orderItemForm;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Guid Execute()
        {
            var orderItem = Session.Load<OrderItem>(orderItemForm.OrderItemId);
            orderItem.Quantity = orderItemForm.Quantity;

            Session.Update(orderItem);

            return orderItem.Order.Id;
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