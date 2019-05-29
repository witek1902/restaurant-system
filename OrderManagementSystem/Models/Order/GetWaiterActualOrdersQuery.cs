namespace OrderManagementSystem.Models.Order
{
    using System.Linq;
    using Domain.Order;
    using System.Collections.Generic;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading current orders for the waiter
    /// </summary>
    public class GetWaiterActualOrdersQuery : Query<List<OrderForm>>
    {
        private readonly int userId;

        public GetWaiterActualOrdersQuery(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
        public override List<OrderForm> Execute(ISession session)
        {
            var orders = session
                .CreateQuery(@"
                                from Order o
                                left join fetch o.OrderItems oi
                                where 
                                    o.Waiter.AppUser.UserId = :userId
                                    and (o.OrderStatus = :assignToWaiterOrderStatusId or o.OrderStatus = :closedOrderStatusId)")
                .SetInt32("assignToWaiterOrderStatusId", (int)OrderStatus.AssignedToWaiter)
                .SetInt32("closedOrderStatusId", (int)OrderStatus.Closed)
                .SetInt32("userId", userId)
                .List<Order>()
                .Distinct()
                .ToList();

            return orders.Select(OrderMapper.MapOrderToForm).ToList();
        }
    }
}