namespace OrderManagementSystem.Domain.Restaurant
{
    using System.Collections.Generic;
    using Product;
    using System;
    using Common;

    /// <summary>
    /// Menu in the restaurant
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
        /// Restaurant
        /// </summary>
        public virtual Restaurant Restaurant { get; set; }

        /// <summary>
        /// List of products that are on the Menu
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        /// Is Menu Active?
        /// </summary>
        public virtual bool Active { get; set; }
    }
}