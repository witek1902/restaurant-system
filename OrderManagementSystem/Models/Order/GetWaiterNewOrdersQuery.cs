namespace OrderManagementSystem.Models.Order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Domain.Order;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading new orders for waiters
    /// </summary>
    public class GetWaiterNewOrdersQuery : Query<List<OrderForm>>
    {
        private readonly Guid restaurantId;

        public GetWaiterNewOrdersQuery(Guid restaurantId)
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