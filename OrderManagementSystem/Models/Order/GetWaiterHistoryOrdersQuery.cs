namespace OrderManagementSystem.Models.Order
{
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading orders that Waiter handled
    /// </summary>
    public class GetWaiterHistoryOrdersQuery : Query<List<OrderForm>>
    {
        private readonly int userId;

        public GetWaiterHistoryOrdersQuery(int userId)
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
                .CreateQuery("from Order o where o.Waiter.AppUser.UserId = :userId")
                .SetInt32("userId", userId)
                .List<Domain.Order.Order>()
                .ToList();

            return orders.Select(OrderMapper.MapOrderToForm).ToList();
        }
    }
}