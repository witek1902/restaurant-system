namespace OrderManagementSystem.Models.Order
{
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie zamówień, które obsłużył kelner
    /// </summary>
    public class GetWaiterHistoryOrdersQuery : Query<List<OrderForm>>
    {
        private readonly int userId;

        public GetWaiterHistoryOrdersQuery(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
        public override List<OrderForm> Execute(ISession session)
        {
            var orders = session
                .CreateQuery("from Order o where o.Waiter.AppUser.UserId = :userId")
                .SetInt32("userId", userId)
                .List<Domain.Order.Order>()
                .ToList();

            return orders.Select(OrderMapper.MapOrderToForm).ToList();
        }
    }
}