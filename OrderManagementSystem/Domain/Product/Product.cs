namespace OrderManagementSystem.Domain.Product
{
    using Restaurant;
    using System;
    using Common;

    /// <summary>
    /// A product that can be added to the order
    /// </summary>
    public class Product : Entity<Guid>
    {
        /// <summary>
        /// Menu to which the product is assigned
        /// </summary>
        public virtual Menu Menu { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Category
        /// </summary>
        public virtual ProductCategory ProductCategory { get; set; }

        /// <summary>
        /// Details
        /// </summary>
        public virtual ProductDetails ProductDetails { get; set; }

        /// <summary>
        /// Price
        /// </summary>
        public virtual decimal Price { get; set; }

        /// <summary>
        /// Description of the product
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Discount from the price in percent
        /// </summary>
        public virtual int? PercentDiscount { get; set; }

        /// <summary>
        /// Url for product photo
        /// </summary>
        public virtual string PhotoUrl { get; set; }

        /// <summary>
        /// Is active
        /// </summary>
        public virtual bool Active { get; set; }
    }

}