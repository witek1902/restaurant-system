namespace OrderManagementSystem.Domain.Restaurant
{
    using System;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading the restaurant Id after providing the Id of the currently logged restaurant employee
    /// </summary>
    public class GetRestaurantIdByUserIdQuery : Query<Guid>
    {
        private readonly int userId;

        public GetRestaurantIdByUserIdQuery(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
    public override Guid Execute(ISession session)
        {
            var restaurantId = session
                .CreateQuery("select rw.Restaurant.Id from RestaurantWorker rw where rw.AppUser.UserId = :userId")
                .SetInt32("userId", userId)
                .List<Guid>()
                .Single();

            return restaurantId;
        }
    }
}