namespace OrderManagementSystem.Models.Product
{
    using System;
    using NHibernate;
    using Infrastructure.Exception;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie produktu pod ID
    /// </summary>
    public class GetProductQuery : Query<ProductForm>
    {
        private readonly Guid productId;

        public GetProductQuery(Guid productId)
        {
            this.productId = productId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
        public override ProductForm Execute(ISession session)
        {
            var product = session.Get<Domain.Product.Product>(productId);
            if(product == null)
                throw new TechnicalException(String.Format("Nie można znaleźć produktu o podanym id: {0}", productId));

            return ProductMapper.MapProductToForm(product);
        }
    }
}