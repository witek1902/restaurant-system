namespace OrderManagementSystem.Models.Restaurant
{
    using System.Linq;
    using NHibernate;
    using Domain.User;
    using Infrastructure.Query;

    /// <summary>
    /// Pobranie pracownika po ID aktualnego użytkownika aplikacji
    /// </summary>
    public class GetRestaurantWorkerByUserIdQuery : Query<RestaurantWorkerForm>
    {
        private readonly int userId;

        public GetRestaurantWorkerByUserIdQuery(int userId)
        {
            this.userId = userId;
        }

        /// <summary>
        /// Metoda do konstruowania i wywoływania zapytania za pomocą sesji NHibernate
        /// </summary>
        /// <param name="session">Sesja NHibernate</param>
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