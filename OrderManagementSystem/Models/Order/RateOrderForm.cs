namespace OrderManagementSystem.Models.Order
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Order evaluation form
    /// </summary>
    public class RateOrderForm
    {
        /// <summary>
        /// Order Id
        /// </summary>
        public Guid OrderId { get; set; }

        [Display(Name= "stars")]
        public int RateStars { get; set; }

        [Display(Name = "Description")]
        public string RateDetails { get; set; }
    }
}