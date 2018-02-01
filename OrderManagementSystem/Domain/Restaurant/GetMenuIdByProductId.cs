namespace OrderManagementSystem.Domain.Restaurant
{
    using System;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie menu poprzez podanie Id produktu
    /// </summary>
    public class GetMenuIdByProductId : Query<Guid>
    {
        private readonly Guid productId;

        public GetMenuIdByProductId(Guid productId)
        {
            this.productId = productId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
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