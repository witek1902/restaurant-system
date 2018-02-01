namespace OrderManagementSystem.Models.Product
{
    using System;
    using NHibernate;
    using Infrastructure.Exception;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie kategorii produktu po ID
    /// </summary>
    public class GetProductCategoryQuery : Query<ProductCategoryForm>
    {
        private readonly Guid productCategoryId;

        public GetProductCategoryQuery(Guid productCategoryId)
        {
            this.productCategoryId = productCategoryId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
        public override ProductCategoryForm Execute(ISession session)
        {
            var productCategory = session.Get<Domain.Product.ProductCategory>(productCategoryId);
            if(productCategory == null)
                throw new TechnicalException(String.Format("Nie można znaleźć klienta o podanym id: {0}", productCategoryId));

            return ProductMapper.MapProductCategoryToForm(productCategory);
        }
    }
}