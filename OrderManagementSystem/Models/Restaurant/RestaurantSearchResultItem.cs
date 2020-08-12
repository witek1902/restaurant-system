namespace OrderManagementSystem.Models.Restaurant
{
    using System;

    /// <summary>
    /// A single item of restaurant search results
    /// </summary>
    public class RestaurantSearchResultItem
    {
        /// <summary>
        /// Id of the restaurant
        /// </summary>
        public Guid RestaurantId { get; set; }

        /// <summary>
        /// Link to the photo
        /// </summary>
        public string RestaurantPhotoUrl { get; set; }

        /// <summary>
        /// Name of the restaurant
        /// </summary>
        public string RestaurantName { get; set; }

        /// <summary>
        /// Restaurant code
        /// </summary>
        public string RestaurantCode { get; set; }

        /// <summary>
        /// Restaurant address
        /// </summary>
        public string RestaurantAddress { get; set; }
    }
}