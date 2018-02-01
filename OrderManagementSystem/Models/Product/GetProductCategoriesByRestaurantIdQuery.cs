namespace OrderManagementSystem.Models.Product
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Domain.Product;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie wszystkich kategorii produktu dla danej restauracji
    /// </summary>
    public class GetProductCategoriesByRestaurantIdQuery : Query<List<ProductCategoryForm>>
    {
        private readonly Guid restaurantId;

        public GetProductCategoriesByRestaurantIdQuery(Guid restaurantId)
        {
            this.restaurantId = restaurantId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
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