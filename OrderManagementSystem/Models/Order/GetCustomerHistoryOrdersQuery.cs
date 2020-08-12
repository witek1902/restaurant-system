namespace OrderManagementSystem.Models.Order
{
    using Domain.Order;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading orders that Customer has placed
    /// </summary>
    public class GetCustomerHistoryOrdersQuery : Query<List<OrderForm>>
    {
        private readonly int userId;

        public GetCustomerHistoryOrdersQuery(int userId)
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
                .CreateQuery("from Order o where o.Customer.AppUser.UserId = :userId and o.OrderStatus != :openStatusId")
                .SetInt32("userId", userId)
                .SetInt32("openStatusId", (int) OrderStatus.Open)
                .List<Order>();

            return orders.Select(OrderMapper.MapOrderToForm).ToList();
        }
    }
}