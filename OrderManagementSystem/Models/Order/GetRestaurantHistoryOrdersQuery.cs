namespace OrderManagementSystem.Models.Order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading historical orders for restaurants
    /// </summary>
    public class GetRestaurantHistoryOrdersQuery : Query<List<OrderForm>>
    {
        private readonly Guid restaurantId;

        public GetRestaurantHistoryOrdersQuery(Guid restaurantId)
        {
            this.restaurantId = restaurantId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
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