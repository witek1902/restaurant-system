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
        /// Lista zamówień, które zostały złożone przez klienta
        /// </summary>
        public virtual ICollection<Order.Order> Orders { get; set; }

        /// <summary>
        /// Użytkownik aplikacji
        /// </summary>
        public virtual AppUser AppUser { get; set; }
    }
}