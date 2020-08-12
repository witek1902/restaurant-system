namespace OrderManagementSystem.Domain.Product
{
    using System;
    using Common;

    /// <summary>
    /// Details of the product
    /// </summary>
    public class ProductDetails : Entity<Guid>
    {
        /// <summary>
        /// Product
        /// </summary>
        public virtual Product Product {get; set; }

        /// <summary>
        /// The number of calories in kcal
        /// </summary>
        public virtual int? Calories { get; set; }

        /// <summary>
        /// Quantity of protein in grams
        /// </summary>
        public virtual int? Protein { get; set; }

        /// <summary>
        /// Quantity of carbohydrates in grams
        /// </summary>
        public virtual int? Carbohydrates { get; set; }

        /// <summary>
        /// Quantity of fat in grams
        /// </summary>
        public virtual int? Fat { get; set; }
    }
}