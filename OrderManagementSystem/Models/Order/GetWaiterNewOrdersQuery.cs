namespace OrderManagementSystem.Models.Order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Domain.Order;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie nowych zamówień dla kelnerów
    /// </summary>
    public class GetWaiterNewOrdersQuery : Query<List<OrderForm>>
    {
        private readonly Guid restaurantId;

        public GetWaiterNewOrdersQuery(Guid restaurantId)
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
                .CreateQuery(@"
                    select o
                    from Order o 
                    left join fetch o.OrderItems oi
                    where 
                        oi.Product.Menu.Restaurant.Id = :restaurantId
                        and o.OrderStatus = :openOrderStatusId")
                .SetInt32("openOrderStatusId", (int) OrderStatus.Open)
                .SetGuid("restaurantId", restaurantId)
                .List<Order>()
                .Distinct()
                .ToList();

            return orders.Select(OrderMapper.MapOrderToForm).ToList();
        }
    }
}