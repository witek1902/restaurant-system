namespace OrderManagementSystem.Models.Restaurant
{
    using Domain.Restaurant;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Pobiera wszystkie Menu dostępne w danej restauracji
    /// </summary>
    public class GetMenusByRestaurantIdQuery : Query<List<MenuForm>>
    {
        private readonly Guid restaurantId;

        public GetMenusByRestaurantIdQuery(Guid restaurantId)
        {
            this.restaurantId = restaurantId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
        public override List<MenuForm> Execute(ISession session)
        {
            var menusEntity = session
                .CreateQuery(@"
                    select m
                    from Menu m 
                        left join fetch m.Products p
                        left join fetch p.ProductCategory
                    where m.Restaurant.Id = :restaurantId and m.Active = 1")
                .SetGuid("restaurantId", restaurantId)
                .List<Menu>()
                .Distinct()
                .ToList();

            return menusEntity.Select(MenuMapper.MapToForm).ToList();
        }
    }
}