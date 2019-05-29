namespace OrderManagementSystem.Domain.Product
{
    using System;
    using Common;

    /// <summary>
    /// Product category
    /// </summary>
    public class ProductCategory : Entity<Guid>
    {
        /// <summary>
        /// Category Code
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Name of the category
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The restaurant to which the product category belongs
        /// </summary>
        public virtual Restaurant.Restaurant Restaurant { get; set; }
    }
}