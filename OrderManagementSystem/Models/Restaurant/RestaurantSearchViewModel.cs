namespace OrderManagementSystem.Models.Restaurant
{
    using System.Collections.Generic;

    /// <summary>
    /// View model do wyszukiwania restauracji
    /// </summary>
    public class RestaurantSearchViewModel
    {
        /// <summary>
        /// Kryteria wyszukiwania restauracji
        /// </summary>
        public RestaurantSearchForm Criteria { get; set; }

        /// <summary>
        /// Rezultat wyszukiwania
        /// </summary>
        public List<RestaurantSearchResultItem> Results { get; set; }
    }
}