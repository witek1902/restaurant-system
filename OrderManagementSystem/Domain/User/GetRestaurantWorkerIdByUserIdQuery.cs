namespace OrderManagementSystem.Domain.User
{
    using System;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Downloading the Id of the restaurant employee by providing the id of the currently logged application user
    /// </summary>
    public class GetRestaurantWorkerIdByUserIdQuery : Query<Guid>
    {
        private readonly int userId;

        public GetRestaurantWorkerIdByUserIdQuery(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// A method for constructing and calling a query using the NHibernate session
        /// </summary>
        /// <param name="session">NHibernate session</param>
        public override Guid Execute(ISession session)
        {
            var worker = session
                .CreateQuery("from RestaurantWorker c where c.AppUser.UserId = :userId")
                .SetInt32("userId", userId)
                .List<RestaurantWorker>()
                .Single();

            return worker.Id;
        }
    }
}