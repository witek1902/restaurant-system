namespace OrderManagementSystem.Domain.Restaurant
{
    using System;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading the menu by specifying the product ID
    /// </summary>
    public class GetMenuIdByProductId : Query<Guid>
    {
        private readonly Guid productId;

        public GetMenuIdByProductId(Guid productId)
        {
            this.productId = productId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
        public override Guid Execute(ISession session)
        {
            return session
                .CreateQuery("select m.Id from Menu m join m.Products p where p.Id = :productId")
                .SetGuid("productId", productId)
                .List<Guid>()
                .Single();
        }
    }
}