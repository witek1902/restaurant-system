namespace OrderManagementSystem.Models.Restaurant
{
    using Domain.Restaurant;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Gets all menus available in your restaurant
    /// </summary>
    public class GetMenusByRestaurantIdQuery : Query<List<MenuForm>>
    {
        private readonly Guid restaurantId;

        public GetMenusByRestaurantIdQuery(Guid restaurantId)
        {
            this.restaurantId = restaurantId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
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