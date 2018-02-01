namespace OrderManagementSystem.Domain.Restaurant
{
    using System;
    using System.Linq;
    using NHibernate;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie Id restauracji po podaniu Id aktualnie zalogowanego pracownika restauracji
    /// </summary>
    public class GetRestaurantIdByUserIdQuery : Query<Guid>
    {
        private readonly int userId;

        public GetRestaurantIdByUserIdQuery(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
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