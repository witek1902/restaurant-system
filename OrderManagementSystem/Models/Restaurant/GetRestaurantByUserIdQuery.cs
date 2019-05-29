namespace OrderManagementSystem.Models.Restaurant
{
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading the restaurant for the employee's ID
    /// </summary>
    public class GetRestaurantByUserIdQuery : Query<RestaurantForm>
    {
        private readonly int userId;

        public GetRestaurantByUserIdQuery(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
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