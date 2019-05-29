namespace OrderManagementSystem.Models.Product
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading all products from the menu
    /// </summary>
    public class GetProductsByMenuQuery : Query<List<ProductForm>>
    {
        private readonly Guid menuId;

        public GetProductsByMenuQuery(Guid menuId)
        {
            this.menuId = menuId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
    public override List<ProductForm> Execute(ISession session)
        {
            var products = session
                .CreateQuery(@"
                    select p
                    from Product p 
                        left join fetch p.ProductCategory pc
                        left join fetch p.ProductDetails pd
                    where p.Menu.Id = :menuId order by p.ProductCategory.Name")
                .SetGuid("menuId", menuId)
                .List<Domain.Product.Product>()
                .ToList();

            return products.Select(ProductMapper.MapProductToForm).ToList();
        }
    }
}