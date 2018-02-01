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
        /// Link do zdjęcia
        /// </summary>
        public string RestaurantPhotoUrl { get; set; }

        /// <summary>
        /// Nazwa restauracji
        /// </summary>
        public string RestaurantName { get; set; }

        /// <summary>
        /// Kod restauracji
        /// </summary>
        public string RestaurantCode { get; set; }

        /// <summary>
        /// Adres restauracji
        /// </summary>
        public string RestaurantAddress { get; set; }
    }
}