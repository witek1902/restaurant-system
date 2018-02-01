namespace OrderManagementSystem.Models.Restaurant
{
    using System;
    using NHibernate;
    using Infrastructure.Exception;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie restauracji po ID
    /// </summary>
    public class GetRestaurantQuery : Query<RestaurantForm>
    {
        private readonly Guid restaurantId;

        public GetRestaurantQuery(Guid restaurantId)
        {
            this.restaurantId = restaurantId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
        public override RestaurantForm Execute(ISession session)
        {
            var restaurant = session.Get<Domain.Restaurant.Restaurant>(restaurantId);
            if(restaurant == null)
                throw new TechnicalException(String.Format("Nie można znaleźć restauracji o podanym id: {0}", restaurantId));

            return RestaurantMapper.MapToForm(restaurant);
        }
    }
}