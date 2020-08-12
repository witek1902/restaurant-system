namespace OrderManagementSystem.Models.Restaurant
{
    using System.Collections.Generic;

    /// <summary>
    /// View model for restaurant search
    /// </summary>
    public class RestaurantSearchViewModel
    {
        /// <summary>
        /// Criteria for restaurant search
        /// </summary>
        public RestaurantSearchForm Criteria { get; set; }

        /// <summary>
        /// Search result
        /// </summary>
        public List<RestaurantSearchResultItem> Results { get; set; }
    }
}