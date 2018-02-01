namespace OrderManagementSystem.Domain.Product
{
    using System;
    using Common;

    /// <summary>
    /// Detale produktu
    /// </summary>
    public class ProductDetails : Entity<Guid>
    {
        /// <summary>
        /// Produkt
        /// </summary>
        public virtual Product Product {get; set; }

        /// <summary>
        /// Liczba kalorii w kcal
        /// </summary>
        public virtual int? Calories { get; set; }

        /// <summary>
        /// Ilość białka w gramach
        /// </summary>
        public virtual int? Protein { get; set; }

        /// <summary>
        /// Ilość węglowodanów w gramach
        /// </summary>
        public virtual int? Carbohydrates { get; set; }

        /// <summary>
        /// Ilość tłuszczu w gramach
        /// </summary>
        public virtual int? Fat { get; set; }
    }
}