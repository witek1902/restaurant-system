namespace OrderManagementSystem.Models.Order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie historycznych zamówień dla restauracji
    /// </summary>
    public class GetRestaurantHistoryOrdersQuery : Query<List<OrderForm>>
    {
        private readonly Guid restaurantId;

        public GetRestaurantHistoryOrdersQuery(Guid restaurantId)
        {
            this.restaurantId = restaurantId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
        public override List<OrderForm> Execute(ISession session)
        {
            var orders = session
                .CreateQuery("select o from Order o where o.Waiter.Restaurant.Id = :restaurantId")
                .SetGuid("restaurantId", restaurantId)
                .List<Domain.Order.Order>()
                .ToList();

            return orders.Select(OrderMapper.MapOrderToForm).ToList();
        }
    }
}