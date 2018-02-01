namespace OrderManagementSystem.Domain.Order
{
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Order;

    /// <summary>
    /// Komenda obsługująca ocenę zamówienia
    /// </summary>
    public class RateOrderCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly RateOrderForm orderForm;

        public RateOrderCommand(RateOrderForm orderForm)
        {
            this.orderForm = orderForm;
        }

        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Guid Execute()
        {
            var order = Session.Load<Order>(orderForm.OrderId);

            order.Rate = orderForm.RateStars;
            order.RateDetails = orderForm.RateDetails;

            Session.Save(order);

            return order.Id;
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