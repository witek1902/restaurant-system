namespace OrderManagementSystem.Domain.Restaurant
{
    using Infrastructure.Security;
    using NHibernate.Util;
    using User;
    using NHibernate;
    using Infrastructure.Service;
    using Models.Restaurant;
    using System.Collections.Generic;

    /// <summary>
    /// The class responsible for constructing and updating the restaurant entities from the ViewModel
    /// </summary>
    public class RestaurantBuilder : BusinessService
    {
        /// <summary>
        /// Creates a new service instance, expects to inject an NHibernate session
        /// </summary>
        public RestaurantBuilder(ISession session) : base(session)
        {
        }

        /// <summary>
        /// Creating a new restaurant
        /// </summary>
        /// <param name="restaurantForm"></param>
        /// <returns></returns>
        public Restaurant ConstructRestaurantEntity(RestaurantForm restaurantForm)
        {
            var restaurant = new Restaurant
            {
                Name = restaurantForm.RestaurantName,
                UniqueCode = restaurantForm.RestaurantCode,
                PhotoUrl = restaurantForm.RestaurantPhotoUrl,
                Address = new RestaurantAddress
                {
                    City = restaurantForm.RestaurantCity,
                    StreetNumber = restaurantForm.RestaurantStreetNumber,
                    FlatNumber = restaurantForm.RestaurantFlatNumber,
                    Street = restaurantForm.RestaurantStreet,
                    PostalCode = restaurantForm.RestaurantPostalCode
                },
                Manager = new RestaurantWorker
                {
                    Firstname = restaurantForm.ManagerFirstname,
                    Lastname = restaurantForm.ManagerLastname,
                    Nick = restaurantForm.ManagerNick,
                    Position = Position.Manager,
                    AppUser = new AppUser
                    {
                        UserId = restaurantForm.ManagerAppUserId.Value
                    },
                    Active = true
                },
                RestaurantWorkers = new List<RestaurantWorker>()
            };

            if(restaurant.RestaurantWorkers.Any())
                restaurantForm.RestaurantWorkers
                    .ForEach(x =>
                    {
                        if (x.RestaurantWorkerId != null)
                            restaurant.RestaurantWorkers
                                .Add(new RestaurantWorker
                                    {
                                        Id = x.RestaurantWorkerId.Value,
                                        Firstname = x.Firstname,
                                        Lastname = x.Lastname,
                                        Nick = x.Nick,
                                        Position = x.Position,
                                        Restaurant = restaurant
                                    });
                    });

            return restaurant;
        }

        /// <summary>
        /// Update restaurant with model view
        /// </summary>
        /// <param name="restaurant">entity</param>
        /// <param name="restaurantForm">Form</param>
        public void UpdateRestaurantEntity(Restaurant restaurant, RestaurantForm restaurantForm)
        {
            restaurant.PhotoUrl = restaurantForm.RestaurantPhotoUrl;
            restaurant.Name = restaurantForm.RestaurantName;
            restaurant.UniqueCode = restaurantForm.RestaurantCode;
            restaurant.Address.City = restaurantForm.RestaurantCity;
            restaurant.Address.Street= restaurantForm.RestaurantStreet;
            restaurant.Address.StreetNumber= restaurantForm.RestaurantStreetNumber;
            restaurant.Address.FlatNumber = restaurantForm.RestaurantFlatNumber;
            restaurant.Address.PostalCode = restaurantForm.RestaurantPostalCode;
        }
    }
}