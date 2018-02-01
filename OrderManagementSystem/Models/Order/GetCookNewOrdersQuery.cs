namespace OrderManagementSystem.Models.Order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Domain.Order;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie nowych zamówień dla kucharza
    /// </summary>
    public class GetCookNewOrdersQuery : Query<List<OrderForm>>
    {
        private readonly Guid restaurantId;

        public GetCookNewOrdersQuery(Guid restaurantId)
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
                                    and o.Cook is null
                                    and o.OrderStatus = :assignedToWaiterStatusId")
                .SetInt32("assignedToWaiterStatusId", (int)OrderStatus.AssignedToWaiter)
                .SetGuid("restaurantId", restaurantId)
                .List<Order>()
                .Distinct()
                .ToList();

            return orders.Select(OrderMapper.MapOrderToForm).ToList();
        }
    }
}