namespace OrderManagementSystem.Domain.User
{
    using System;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie Id pracownika restauracji podając id aktualnie zalogowanego usera aplikacji
    /// </summary>
    public class GetRestaurantWorkerIdByUserIdQuery : Query<Guid>
    {
        private readonly int userId;

        public GetRestaurantWorkerIdByUserIdQuery(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
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