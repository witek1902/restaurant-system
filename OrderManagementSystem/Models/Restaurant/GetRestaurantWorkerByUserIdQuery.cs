namespace OrderManagementSystem.Models.Restaurant
{
    using System.Linq;
    using NHibernate;
    using Domain.User;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading the employee after the ID of the current user of the application
    /// </summary>
    public class GetRestaurantWorkerByUserIdQuery : Query<RestaurantWorkerForm>
    {
        private readonly int userId;

        public GetRestaurantWorkerByUserIdQuery(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
        public override RestaurantWorkerForm Execute(ISession session)
        {
            var worker = session
                .CreateQuery("from RestaurantWorker rw where rw.AppUser.UserId = :userId")
                .SetInt32("userId", userId)
                .List<RestaurantWorker>()
                .Single();

            return RestaurantWorkerMapper.MapToForm(worker);
        }
    }
}