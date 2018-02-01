namespace OrderManagementSystem.Models.Order
{
    using System.Linq;
    using Domain.Order;
    using System.Collections.Generic;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie aktualnych zamówień dla kelnera
    /// </summary>
    public class GetWaiterActualOrdersQuery : Query<List<OrderForm>>
    {
        private readonly int userId;

        public GetWaiterActualOrdersQuery(int userId)
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