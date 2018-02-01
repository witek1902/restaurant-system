namespace OrderManagementSystem.Domain.Order
{
    using System.Linq;
    using System;
    using OrderItem;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;
    using Models.Order;

    /// <summary>
    /// Komenda do składania zamówienia (po wstępnym wyborze produktów)
    /// </summary>
    public class PlaceOrderCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly OrderForm orderForm;
        private IOrderItemStatusService orderItemStatusService;

        public PlaceOrderCommand(OrderForm orderForm)
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

            order.Comments = orderForm.OrderComments;
            order.TableNumber = orderForm.TableNumber;

            foreach (var orderItem in order.OrderItems.Where(x => x.OrderItemStatus == OrderItemStatus.New).ToList())
                orderItemStatusService.ApproveOrderItem(orderItem);

            Session.Update(order);

            return order.Id;
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