namespace OrderManagementSystem.Models.Restaurant
{
    using System.Collections.Generic;
    using Product;
    using System.ComponentModel.DataAnnotations;
    using System;

    /// <summary>
    /// Formularz do tworzenia nowego Menu
    /// </summary>
    public class MenuForm
    {
        /// <summary>
        /// Id menu
        /// </summary>
        public Guid? MenuId { get; set; }

        /// <summary>
        /// Id restauracji
        /// </summary>
        public Guid? RestaurantId { get; set; }

        [Required]
        [Display(Name="Nazwa menu")]
        public string MenuName { get; set; }

        [Required]
        [Display(Name = "Skrócony kod menu")]
        public string MenuCode { get; set; }

        [Display(Name="Aktywne")]
        public bool Active { get; set; }

        public List<ProductForm> Products { get; set; }

        public List<ProductCategoryForm> ProductCategories { get; set; } 
    }
}