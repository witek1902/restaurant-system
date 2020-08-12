namespace OrderManagementSystem.Models.Product
{
    using Restaurant;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System;

    /// <summary>
    /// Form for adding / editing a product
    /// </summary>
    public class ProductForm
    {
        /// <summary>
        /// Product ID
        /// </summary>
        public Guid? ProductId { get; set; }

        /// <summary>
        /// The menu to which the product belongs
        /// </summary>
        public Guid? MenuId { get; set; }

        [Display(Name= "Active")]
        public bool Active { get; set; }

        /// <summary>
        /// The category ID to which the product belongs
        /// </summary>
        public Guid? ProductCategoryId { get; set; }

        /// <summary>
        /// Product details id
        /// </summary>
        public Guid? ProductDetailsId { get; set; }

        [Display(Name="Name")]
        public string ProductName { get; set; }

        [Display(Name = "Link to the photo")]
        public string ProductPhotoUrl { get; set; }

        [Display(Name = "Category")]
        public string ProductCategoryName { get; set; }

        [Display(Name="Name menu")]
        public string MenuName { get; set; }

        [Display(Name = "Description")]
        public string ProductDescription { get; set; }

        [RegularExpression(@"[0-9]*\,?[0-9]+", ErrorMessage = "{0} must be numeric.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Promotion (in%)")]
        public int? PercentDiscount { get; set; }

        [Display(Name = "Quantity of calories w kcal")]
        public int? ProductDetailsCalories { get; set; }

        [Display(Name = "Quantity of protein in grams")]
        public int? ProductDetailsProtein { get; set; }

        [Display(Name = "Quantity of carbohydrates in grams")]
        public int? ProductDetailsCarbohydrates { get; set; }

        [Display(Name = "Quantity of fat in grams")]
        public int? ProductDetailsFat { get; set; }

        /// <summary>
        /// List of available categories in the restaurant
        /// </summary>
        [Display(Name="Category")]
        public List<ProductCategoryForm> ProductCategories { get; set; }

        /// <summary>
        /// List of available menus in the restaurant
        /// </summary>
        [Display(Name="Menu")]
        public List<MenuForm> Menus { get; set; } 

        public ProductForm()
        {
            ProductCategories = new List<ProductCategoryForm>();
            Menus = new List<MenuForm>(); 
        }

        public ProductForm(List<ProductCategoryForm> productCategories, List<MenuForm> menus)
        {
            ProductCategories = productCategories;
            Menus = menus;
        }
    }

    /// <summary>
    /// Form for adding / editing product categories
    /// </summary>
    public class ProductCategoryForm
    {
        /// <summary>
        /// Category Id
        /// </summary>
        public Guid? ProductCategoryId { get; set; }

        /// <summary>
        /// Id of the restaurant
        /// </summary>
        public Guid? RestaurantId { get; set; }

        [Display(Name = "Name of the category")]
        public string ProductCategoryName { get; set; }

        [Display(Name = "Category Code")]
        public string ProductCategoryCode { get; set; }

        /// <summary>
        /// List of categories available in the restaurant
        /// </summary>
        public List<ProductCategoryForm> ProductCategories { get; set; }
    }
}