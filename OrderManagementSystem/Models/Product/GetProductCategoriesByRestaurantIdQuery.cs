namespace OrderManagementSystem.Models.Product
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Domain.Product;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading all product categories for a given restaurant
    /// </summary>
    public class GetProductCategoriesByRestaurantIdQuery : Query<List<ProductCategoryForm>>
    {
        private readonly Guid restaurantId;

        public GetProductCategoriesByRestaurantIdQuery(Guid restaurantId)
        {
            this.restaurantId = restaurantId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
    public override List<ProductCategoryForm> Execute(ISession session)
        {
            var productCategories = session
                .CreateQuery("from ProductCategory pc where pc.Restaurant.Id = :restaurantId")
                .SetGuid("restaurantId", restaurantId)
                .List<ProductCategory>();

            return productCategories.Select(ProductMapper.MapProductCategoryToForm).ToList();
        }
    }
}