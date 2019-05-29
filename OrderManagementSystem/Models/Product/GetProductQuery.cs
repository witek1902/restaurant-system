namespace OrderManagementSystem.Models.Product
{
    using System;
    using NHibernate;
    using Infrastructure.Exception;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading the product under ID
    /// </summary>
    public class GetProductQuery : Query<ProductForm>
    {
        private readonly Guid productId;

        public GetProductQuery(Guid productId)
        {
            this.productId = productId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
        public override ProductForm Execute(ISession session)
        {
            var product = session.Get<Domain.Product.Product>(productId);
            if(product == null)
                throw new TechnicalException(String.Format("The product with the given id can not be found: {0}", productId));

            return ProductMapper.MapProductToForm(product);
        }
    }
}