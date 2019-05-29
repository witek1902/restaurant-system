namespace OrderManagementSystem.Domain.Restaurant
{
    using Models.Restaurant;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Restaurant upgrade
    /// </summary>
    public class UpdateRestaurantCommand : Command<Restaurant>, INeedSession, INeedAutocommitTransaction
    {
        private readonly RestaurantForm restaurantForm;
        private RestaurantBuilder restaurantBuilder;

        public UpdateRestaurantCommand(RestaurantForm restaurantForm)
        {
            this.restaurantForm = restaurantForm;
        }

        /// <summary>
        /// Invokes the command and returns the specified type
        /// </summary>
        /// <returns>Result</returns>
        public override Restaurant Execute()
        {
            var restaurant = Session.Load<Restaurant>(restaurantForm.RestaurantId);

            restaurantBuilder.UpdateRestaurantEntity(restaurant, restaurantForm);

            Session.Update(restaurant);

            return restaurant;
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