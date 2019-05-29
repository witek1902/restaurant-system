namespace OrderManagementSystem.Domain.User
{
    using NHibernate;
    using Infrastructure.Security;
    using Infrastructure.Service;
    using Models.Restaurant;

    /// <summary>
    /// Building an employee of a restaurant employee
    /// </summary>
    public class RestaurantWorkerBuilder : BusinessService
    {
        /// <summary>
        /// Creates a new service instance, expects to inject an NHibernate session
        /// </summary>
        public RestaurantWorkerBuilder(ISession session) : base(session)
        {
        }

        /// <summary>
        /// Constructing a new restaurant employee entity from the form
        /// </summary>
        /// <param name="workerForm"></param>
        /// <returns></returns>
        public RestaurantWorker ConstructRestaurantWorkerEntity(RestaurantWorkerForm workerForm)
        {
            var worker = new RestaurantWorker
            {
                Firstname = workerForm.Firstname,
                Lastname = workerForm.Lastname,
                AppUser = new AppUser
                {
                    UserId = workerForm.AppUserId
                },
                Nick = workerForm.Nick,
                Active = true,
                Position = workerForm.Position,
                Restaurant = workerForm.RestaurantId.HasValue ? new Restaurant.Restaurant
                {
                    Id = workerForm.RestaurantId.Value
                } : null
            };

            return worker;
        }

        /// <summary>
        /// Data update
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="workerForm"></param>
        public void UpdateRestaurantWorkerEntity(RestaurantWorker worker, RestaurantWorkerForm workerForm)
        {
            worker.Active = workerForm.Active;
            worker.Firstname = workerForm.Firstname;
            worker.Lastname = workerForm.Lastname;
            worker.Nick = workerForm.Nick;
            worker.Position = workerForm.Position;
        }
    }
}