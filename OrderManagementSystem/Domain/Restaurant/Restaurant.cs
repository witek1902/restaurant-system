namespace OrderManagementSystem.Domain.Restaurant
{
    using Product;
    using System.Collections.Generic;
    using User;
    using System;
    using Common;

    /// <summary>
    /// Restaurant
    /// </summary>
    public class Restaurant : Entity<Guid>
    {
        /// <summary>
        /// Name of the restaurant
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// A unique restaurant code
        /// </summary>
        public virtual string UniqueCode { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public virtual RestaurantAddress Address { get; set; }

        /// <summary>
        /// Restaurent Manager
        /// </summary>
        public virtual RestaurantWorker Manager { get; set; }

        /// <summary>
        /// Link to the photo restaurant
        /// </summary>
        public virtual string PhotoUrl { get; set; }

        /// <summary>
        /// Restaurant employees
        /// </summary>
        public virtual ICollection<RestaurantWorker> RestaurantWorkers { get; set; }

        /// <summary>
        /// Menu available in the restaurant
        /// </summary>
        public virtual ICollection<Menu> Menus { get; set; }

        /// <summary>
        /// List of categories available in the restaurant
        /// </summary>
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } 
    }

    /// <summary>
    /// Restaurant address
    /// </summary>
    public class RestaurantAddress
    {
        /// <summary>
        /// Street
        /// </summary>
        public virtual string Street { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// ZIP code
        /// </summary>
        public virtual string PostalCode { get; set; }

        /// <summary>
        /// Number of the building
        /// </summary>
        public virtual int StreetNumber { get; set; }

        /// <summary>
        /// House number
        /// </summary>
        public virtual int? FlatNumber { get; set; }
    }
}