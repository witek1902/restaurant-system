namespace OrderManagementSystem.Models.Restaurant
{
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie restauracji po ID pracownika
    /// </summary>
    public class GetRestaurantByUserIdQuery : Query<RestaurantForm>
    {
        private readonly int userId;

        public GetRestaurantByUserIdQuery(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
        public override RestaurantForm Execute(ISession session)
        {
            var restaurantId = session
                .CreateQuery(@"
                    select 
                        rw.Restaurant.Id
                    from RestaurantWorker rw
                    where rw.AppUser.UserId = :userId")
                .SetInt32("userId", userId)
                .List<System.Guid>()
                .Single();

            var restaurant = session.Get<Domain.Restaurant.Restaurant>(restaurantId);

            return RestaurantMapper.MapToForm(restaurant);
        }
    }
}