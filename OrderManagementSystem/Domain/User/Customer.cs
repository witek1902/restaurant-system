namespace OrderManagementSystem.Domain.User
{
    using Infrastructure.Security;
    using System.Collections.Generic;
    using System;
    using Common;

    /// <summary>
    /// Customer
    /// </summary>
    public class Customer : Entity<Guid>
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Firstname { get; set; }

        /// <summary>
        /// List of orders that have been placed by the customer
        /// </summary>
        public virtual ICollection<Order.Order> Orders { get; set; }

        /// <summary>
        /// Application user
        /// </summary>
        public virtual AppUser AppUser { get; set; }
    }
}