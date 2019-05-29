namespace OrderManagementSystem.Models.Restaurant
{
    using System;
    using NHibernate;
    using Infrastructure.Exception;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading the restaurant by ID
    /// </summary>
    public class GetRestaurantQuery : Query<RestaurantForm>
    {
        private readonly Guid restaurantId;

        public GetRestaurantQuery(Guid restaurantId)
        {
            this.restaurantId = restaurantId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
        public override RestaurantForm Execute(ISession session)
        {
            var restaurant = session.Get<Domain.Restaurant.Restaurant>(restaurantId);
            if(restaurant == null)
                throw new TechnicalException(String.Format("You can not find a restaurant with the given id: {0}", restaurantId));

            return RestaurantMapper.MapToForm(restaurant);
        }
    }
}