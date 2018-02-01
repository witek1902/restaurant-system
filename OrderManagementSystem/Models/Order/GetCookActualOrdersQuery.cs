namespace OrderManagementSystem.Models.Order
{
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Domain.Order;
    using Infrastructure.Query;
    
    /// <summary>
    /// Pobranie aktualnych zamówień dla kucharza 
    /// </summary>
    public class GetCookActualOrdersQuery : Query<List<OrderForm>>
    {
        private readonly int userId;

        public GetCookActualOrdersQuery(int userId)
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
                                select o
                                from Order o 
                                left join fetch o.OrderItems oi
                                where 
                                    o.Cook.AppUser.UserId = :userId
                                    and o.OrderStatus = :assignToWaiterStatusId")
                .SetInt32("assignToWaiterStatusId", (int)OrderStatus.AssignedToWaiter)
                .SetInt32("userId", userId)
                .List<Order>()
                .Distinct()
                .ToList();

            return orders.Select(OrderMapper.MapOrderToForm).ToList();
        }
    }
}