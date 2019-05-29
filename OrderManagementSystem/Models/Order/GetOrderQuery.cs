namespace OrderManagementSystem.Models.Order
{
    using System.Linq;
    using NHibernate.Util;
    using System;
    using NHibernate;
    using Infrastructure.Exception;
    using Infrastructure.Query;

    /// <summary>
    /// Order collection by ID
    /// </summary>
    public class GetOrderQuery : Query<OrderForm>
    {
        private readonly Guid orderId;

        public GetOrderQuery(Guid orderId)
        {
            this.orderId = orderId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
        public override OrderForm Execute(ISession session)
        {
            var order = session.Get<Domain.Order.Order>(orderId);

            if (order == null)
                throw new TechnicalException($"Nie można znaleźć zamówienia o podanym id: {orderId}");

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