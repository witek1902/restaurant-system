namespace OrderManagementSystem.Models.Order
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Formularz oceny zamówienia
    /// </summary>
    public class RateOrderForm
    {
        /// <summary>
        /// Id zamówienia
        /// </summary>
        public Guid OrderId { get; set; }

        [Display(Name="Gwiazdki")]
        public int RateStars { get; set; }

        [Display(Name = "Description")]
        public string RateDetails { get; set; }
    }
}