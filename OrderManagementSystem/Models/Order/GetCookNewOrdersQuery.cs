namespace OrderManagementSystem.Models.Order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Domain.Order;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading new orders for the cook
    /// </summary>
    public class GetCookNewOrdersQuery : Query<List<OrderForm>>
    {
        private readonly Guid restaurantId;

        public GetCookNewOrdersQuery(Guid restaurantId)
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