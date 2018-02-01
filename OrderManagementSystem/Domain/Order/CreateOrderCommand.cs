namespace OrderManagementSystem.Domain.Order
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Order;

    /// <summary>
    /// Utworzenie zamówienia
    /// </summary>
    public class CreateOrderCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly OrderForm orderForm;
        private OrderBuilder orderBuilder;

        public CreateOrderCommand(OrderForm orderForm)
        {
            this.orderForm = orderForm;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Guid Execute()
        {
            var order = orderBuilder.ConstructOrderEntity(orderForm);

            Session.Save(order);

            return order.Id;
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
        /// Sesja NHibernate.
        /// </summary>
        public ISession Session { get; set; }
    }
}