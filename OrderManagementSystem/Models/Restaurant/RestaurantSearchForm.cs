namespace OrderManagementSystem.Models.Restaurant
{
    /// <summary>
    /// Formularz wyszukiwania restauracji
    /// </summary>
    public class RestaurantSearchForm
    {
        /// <summary>
        /// Name lub unikalny numer restauracji
        /// </summary>
        public string RestaurantNameOrCode { get; set; }
    }
}