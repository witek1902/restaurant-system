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
        /// Nazwa restauracji
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Unikalny kod restauracji
        /// </summary>
        public virtual string UniqueCode { get; set; }

        /// <summary>
        /// Adres
        /// </summary>
        public virtual RestaurantAddress Address { get; set; }

        /// <summary>
        /// Manager restauracji
        /// </summary>
        public virtual RestaurantWorker Manager { get; set; }

        /// <summary>
        /// Link do zdjęcia restauracji
        /// </summary>
        public virtual string PhotoUrl { get; set; }

        /// <summary>
        /// Pracownicy restauracji
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
        /// Ulica
        /// </summary>
        public virtual string Street { get; set; }

        /// <summary>
        /// Miasto
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// Kod pocztowy
        /// </summary>
        public virtual string PostalCode { get; set; }

        /// <summary>
        /// Numer budynku
        /// </summary>
        public virtual int StreetNumber { get; set; }

        /// <summary>
        /// Numer lokalu
        /// </summary>
        public virtual int? FlatNumber { get; set; }
    }
}