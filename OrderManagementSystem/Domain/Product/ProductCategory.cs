namespace OrderManagementSystem.Domain.Product
{
    using System;
    using Common;

    /// <summary>
    /// Category produktu
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
        /// Restauracja, do której należy dana Category produktu
        /// </summary>
        public virtual Restaurant.Restaurant Restaurant { get; set; }
    }
}