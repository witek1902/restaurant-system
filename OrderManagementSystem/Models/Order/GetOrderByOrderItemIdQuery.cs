namespace OrderManagementSystem.Models.Order
{
    using NHibernate.Util;
    using Infrastructure.Exception;
    using System;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Get order by line item id
    /// </summary>
    public class GetOrderByOrderItemIdQuery : Query<OrderForm>
    {
        private readonly Guid orderItemId;

        public GetOrderByOrderItemIdQuery(Guid orderItemId)
        {
            this.orderItemId = orderItemId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
        public override OrderForm Execute(ISession session)
        {
            var order = session
                .CreateQuery("select o from Order o join o.OrderItems oi where oi.Id = :orderItemId")
                .SetGuid("orderItemId", orderItemId)
                .List<Domain.Order.Order>()
                .Single();

            if (order == null)
                throw new TechnicalException(String.Format("Nie można znaleźć zamówienia o podanym id elementu zamówienia: {0}", orderItemId));

            var productIds = order.OrderItems.Select(x => x.Product.Id).ToList();

            foreach (var productId in productIds)
            {
                var product = session.Get<Domain.Product.Product>(productId);
                order.OrderItems.Where(x => x.Product.Id == productId).ForEach(x => x.Product = product);
            }

            return OrderMapper.MapOrderToForm(order);
        }
    }
}