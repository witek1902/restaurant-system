namespace OrderManagementSystem.Domain.Restaurant
{
    using Models.Restaurant;
    using System;
    using Castle.Windsor;
    using NHibernate;
    using Infrastructure.Command;

    /// <summary>
    /// Tworzenie restauracji
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
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public override Guid Execute()
        {
            var restaurant = restaurantBuilder.ConstructRestaurantEntity(restaurantForm);

            Session.Save(restaurant);
            restaurant.Manager.Restaurant = restaurant;
            Session.Update(restaurant.Manager);

            return restaurant.Id;
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