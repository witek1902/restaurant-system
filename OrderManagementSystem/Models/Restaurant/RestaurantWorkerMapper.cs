namespace OrderManagementSystem.Models.Restaurant
{
    using System;
    using Domain.User;

    /// <summary>
    /// Mapping restaurant employees
    /// </summary>
    public static class RestaurantWorkerMapper
    {
        /// <summary>
        /// Mapping the role to enum position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string MapPositionEnumToRole(Position position)
        {
            switch(position)
            {
                case Position.Waiter:
                    return "waiters";
                case Position.Cook:
                    return "cooks";
                case Position.Manager:
                    return "managers";
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }

        public static RestaurantWorkerForm MapToForm(RestaurantWorker worker)
        {
            return new RestaurantWorkerForm
            {
                RestaurantWorkerId = worker.Id,
                Position = worker.Position,
                Firstname = worker.Firstname,
                Lastname = worker.Lastname,
                Nick = worker.Nick,
                RestaurantId = worker.Restaurant?.Id,
                RestaurantName = worker.Restaurant != null ? worker.Restaurant.Name : String.Empty,
                Active = worker.Active,
                AppUserId = worker.AppUser.UserId
            };
        }
    }
}