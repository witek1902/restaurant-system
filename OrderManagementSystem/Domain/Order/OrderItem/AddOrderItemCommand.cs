namespace OrderManagementSystem.Domain.Order.OrderItem
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Order;

    /// <summary>
    /// Dodanie elementu zamówienia
    /// </summary>
    public class AddOrderItemCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly OrderItemForm orderItemForm;
        private OrderBuilder orderBuilder;

        public AddOrderItemCommand(OrderItemForm orderItemForm)
        {
            this.orderItemForm = orderItemForm;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Guid Execute()
        {
            var orderItem = orderBuilder.ConstructOrderItemEntity(orderItemForm);

            Session.Save(orderItem);

            return orderItem.Id;
        }

        /// <summary>
        /// Dodawanie własnych zależności do komendy.
        /// </summary>
        /// <param name="container">Kontener IoC</param>
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