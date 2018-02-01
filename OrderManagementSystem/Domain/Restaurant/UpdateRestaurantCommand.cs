namespace OrderManagementSystem.Domain.Restaurant
{
    using Models.Restaurant;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Aktualizacja restauracji
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
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Restaurant Execute()
        {
            var restaurant = Session.Load<Restaurant>(restaurantForm.RestaurantId);

            restaurantBuilder.UpdateRestaurantEntity(restaurant, restaurantForm);

            Session.Update(restaurant);

            return restaurant;
        }

        /// <summary>
        /// Dodawanie własnych zależności do komendy.
        /// </summary>
        /// <param name="container">Kontener IoC</param>
        public override void SetupDependencies(IWindsorContainer container)
        {
            restaurantBuilder = container.Resolve<RestaurantBuilder>();
        }

        /// <summary>
        /// Sesja NHibernate.
        /// </summary>
        public ISession Session { get; set; }
    }
}