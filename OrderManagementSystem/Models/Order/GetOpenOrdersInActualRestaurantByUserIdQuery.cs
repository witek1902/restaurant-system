namespace OrderManagementSystem.Models.Order
{
    using System.Linq;
    using Infrastructure.Query;
    using System;
    using NHibernate;
    using Domain.Order;

    /// <summary>
    /// Pobranie otwartych zamówień w danej restauracji
    /// </summary>
    public class GetOpenOrdersInActualRestaurantByUserIdQuery : Query<OrderForm>
    {
        private readonly int userId;
        private readonly Guid restaurantId;

        public GetOpenOrdersInActualRestaurantByUserIdQuery(int userId, Guid restaurantId)
        {
            this.userId = userId;
            this.restaurantId = restaurantId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
        public override OrderForm Execute(ISession session)
        {
            var order = session
                .CreateQuery(@"
                    select o 
                    from Order o 
                        join fetch o.OrderItems oi 
                        join fetch o.Customer c
                        join fetch c.AppUser au
                        join fetch oi.Product p
                        join fetch p.Menu m
                        join fetch m.Restaurant r
                    where 
                        au.UserId = :userId 
                        and (o.OrderStatus = :openStatusId or o.OrderStatus = :assignToWaiterStatusId or o.OrderStatus = :closedStatusId)
                        and r.Id = :restaurantId")
                .SetInt32("userId", userId)
                .SetInt32("openStatusId", (int) OrderStatus.Open)
                .SetInt32("assignToWaiterStatusId", (int) OrderStatus.AssignedToWaiter)
                .SetInt32("closedStatusId", (int)OrderStatus.Closed)
                .SetGuid("restaurantId", restaurantId)
                .List<Order>();

            return !order.Any() ? null : OrderMapper.MapOrderToForm(order.Distinct().First());
        }
    }
}