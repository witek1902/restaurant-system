namespace OrderManagementSystem.Models.Product
{
    using System;
    using NHibernate;
    using Infrastructure.Exception;
    using Infrastructure.Query;

    /// <summary>
    /// Download product category by ID
    /// </summary>
    public class GetProductCategoryQuery : Query<ProductCategoryForm>
    {
        private readonly Guid productCategoryId;

        public GetProductCategoryQuery(Guid productCategoryId)
        {
            this.productCategoryId = productCategoryId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
        public override ProductCategoryForm Execute(ISession session)
        {
            var productCategory = session.Get<Domain.Product.ProductCategory>(productCategoryId);
            if(productCategory == null)
                throw new TechnicalException(String.Format("The client with the given id can not be found: {0}", productCategoryId));

            return ProductMapper.MapProductCategoryToForm(productCategory);
        }
    }
}