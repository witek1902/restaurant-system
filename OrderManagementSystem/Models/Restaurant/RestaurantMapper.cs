namespace OrderManagementSystem.Models.Restaurant
{
    using Product;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Mapping class for the 'Restaurant' entity
    /// </summary>
    public static class RestaurantMapper
    {
        /// <summary>
        /// Entity mapping to search results
        /// </summary>
        /// <param name="restaurant">Entity restaurant</param>
        /// <returns>Search result</returns>
        public static RestaurantSearchResultItem MapToSearchResultsItem(Domain.Restaurant.Restaurant restaurant)
        {
            return new RestaurantSearchResultItem
            {
                RestaurantId = restaurant.Id,
                RestaurantName = restaurant.Name,
                RestaurantPhotoUrl = restaurant.PhotoUrl,
                RestaurantCode = restaurant.UniqueCode,
                RestaurantAddress = $"{restaurant.Address.PostalCode} {restaurant.Address.City}, ul. {restaurant.Address.Street} {restaurant.Address.StreetNumber}"
            };
        }

        /// <summary>
        /// Entity mapping on the form
        /// </summary>
        /// <param name="restaurant">entity</param>
        /// <returns>Form</returns>
        public static RestaurantForm MapToForm(Domain.Restaurant.Restaurant restaurant)
        {
            var form = new RestaurantForm
            {
                RestaurantId = restaurant.Id,
                ManagerId = restaurant.Manager?.Id,
                ManagerFirstname = restaurant.Manager != null ? restaurant.Manager.Firstname : String.Empty,
                ManagerLastname = restaurant.Manager != null ? restaurant.Manager.Lastname : String.Empty,
                RestaurantName = restaurant.Name,
                RestaurantPhotoUrl = restaurant.PhotoUrl,
                RestaurantCode = restaurant.UniqueCode,
                RestaurantCity = restaurant.Address.City,
                RestaurantStreet = restaurant.Address.Street,
                RestaurantStreetNumber = restaurant.Address.StreetNumber,
                RestaurantFlatNumber = restaurant.Address.FlatNumber,
                RestaurantPostalCode = restaurant.Address.PostalCode,
                RestaurantWorkers = new List<RestaurantWorkerForm>(),
                Menus = new List<MenuForm>()
            };

            if(restaurant.RestaurantWorkers.Any())
                form.RestaurantWorkers
                    .AddRange(restaurant.RestaurantWorkers.Select(RestaurantWorkerMapper.MapToForm).ToList());

            if (restaurant.Menus.Any())
                form.Menus
                    .AddRange(restaurant.Menus
                        .Select(x => new MenuForm
                        {
                            MenuId = x.Id,
                            MenuName = x.Name,
                            MenuCode = x.Code,
                            Products = x.Products.Select(ProductMapper.MapProductToForm).ToList()
                        })
                        .ToList());

            return form;
        }
    }
}