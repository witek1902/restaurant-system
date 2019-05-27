namespace OrderManagementSystem.Domain.Restaurant
{
    using System.Collections.Generic;
    using Product;
    using System;
    using Common;

    /// <summary>
    /// Menu w restauracji
    /// </summary>
    public class Menu : Entity<Guid>
    {
        /// <summary>
        /// Code Menu
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Name menu
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Restauracja
        /// </summary>
        public virtual Restaurant Restaurant { get; set; }

        /// <summary>
        /// Lista produktów, które są w Menu
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        /// Czy Menu jest aktywne?
        /// </summary>
        public virtual bool Active { get; set; }
    }
}