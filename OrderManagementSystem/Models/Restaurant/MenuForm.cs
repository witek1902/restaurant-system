namespace OrderManagementSystem.Models.Restaurant
{
    using System.Collections.Generic;
    using Product;
    using System.ComponentModel.DataAnnotations;
    using System;

    /// <summary>
    /// Form for creating a new Menu
    /// </summary>
    public class MenuForm
    {
        /// <summary>
        /// Id menu
        /// </summary>
        public Guid? MenuId { get; set; }

        /// <summary>
        /// Id of the restaurant
        /// </summary>
        public Guid? RestaurantId { get; set; }

        [Required]
        [Display(Name="Name menu")]
        public string MenuName { get; set; }

        [Required]
        [Display(Name = "Brief Code menu")]
        public string MenuCode { get; set; }

        [Display(Name="Active")]
        public bool Active { get; set; }

        public List<ProductForm> Products { get; set; }

        public List<ProductCategoryForm> ProductCategories { get; set; } 
    }
}