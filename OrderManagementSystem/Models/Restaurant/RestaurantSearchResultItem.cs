namespace OrderManagementSystem.Models.Restaurant
{
    using System;

    /// <summary>
    /// Pojedyncza pozycja rezultatów wyszukiwania restauracji
    /// </summary>
    public class RestaurantSearchResultItem
    {
        /// <summary>
        /// Id restauracji
        /// </summary>
        public Guid RestaurantId { get; set; }

        /// <summary>
        /// Link to the photo
        /// </summary>
        public string RestaurantPhotoUrl { get; set; }

        /// <summary>
        /// Name restauracji
        /// </summary>
        public string RestaurantName { get; set; }

        /// <summary>
        /// Code restauracji
        /// </summary>
        public string RestaurantCode { get; set; }

        /// <summary>
        /// Adres restauracji
        /// </summary>
        public string RestaurantAddress { get; set; }
    }
}