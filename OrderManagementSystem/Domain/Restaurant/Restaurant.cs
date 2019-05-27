namespace OrderManagementSystem.Domain.Restaurant
{
    using Product;
    using System.Collections.Generic;
    using User;
    using System;
    using Common;

    /// <summary>
    /// Resturacja
    /// </summary>
    public class Restaurant : Entity<Guid>
    {
        /// <summary>
        /// Name restauracji
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// A unique restaurant code
        /// </summary>
        public virtual string UniqueCode { get; set; }

        /// <summary>
        /// Adres
        /// </summary>
        public virtual RestaurantAddress Address { get; set; }

        /// <summary>
        /// Restaurent Manager
        /// </summary>
        public virtual RestaurantWorker Manager { get; set; }

        /// <summary>
        /// Link to the photo restauracji
        /// </summary>
        public virtual string PhotoUrl { get; set; }

        /// <summary>
        /// Employees restauracji
        /// </summary>
        public virtual ICollection<RestaurantWorker> RestaurantWorkers { get; set; }

        /// <summary>
        /// Menu dostępne w restauracji
        /// </summary>
        public virtual ICollection<Menu> Menus { get; set; } 

        /// <summary>
        /// Lista kategorii dotępnych w restauracji
        /// </summary>
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } 
    }

    /// <summary>
    /// Adres restauracji
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