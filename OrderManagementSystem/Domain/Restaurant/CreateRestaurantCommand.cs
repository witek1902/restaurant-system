namespace OrderManagementSystem.Domain.Restaurant
{
    using Models.Restaurant;
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Creating a restaurant
    /// </summary>
    public class CreateRestaurantCommand : Command<Guid>, INeedSession, INeedAutocommitTransaction
    {
        private readonly RestaurantForm restaurantForm;
        private RestaurantBuilder restaurantBuilder;

        public CreateRestaurantCommand(RestaurantForm restaurantForm)
        {
            this.restaurantForm = restaurantForm;
        }

        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Guid Execute()
        {
            var restaurant = restaurantBuilder.ConstructRestaurantEntity(restaurantForm);

            Session.Save(restaurant);
            restaurant.Manager.Restaurant = restaurant;
            Session.Update(restaurant.Manager);

            return restaurant.Id;
        }

        /// <summary>
        /// Adding custom dependencies to the command.
        /// </summary>
        /// <param name="container">IoC container</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            restaurantBuilder = container.Resolve<RestaurantBuilder>();
        }

        /// <summary>
        /// NHibernate session.
        /// </summary>
        public ISession Session { get; set; }
    }
}