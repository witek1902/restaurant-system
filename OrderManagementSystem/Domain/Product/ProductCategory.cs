namespace OrderManagementSystem.Domain.Product
{
    using System;
    using Common;

    /// <summary>
    /// Kategoria produktu
    /// </summary>
    public class ProductCategory : Entity<Guid>
    {
        /// <summary>
        /// Kod kategorii
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Nazwa kategorii
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Restauracja, do której należy dana kategoria produktu
        /// </summary>
        public virtual Restaurant.Restaurant Restaurant { get; set; }
    }
}