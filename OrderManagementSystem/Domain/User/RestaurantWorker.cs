namespace OrderManagementSystem.Domain.User
{
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Security;
    using System;
    using Common;

    /// <summary>
    /// Kelner w restauracji
    /// </summary>
    public class RestaurantWorker : Entity<Guid>
    {
        /// <summary>
        /// "Pseudonim"
        /// </summary>
        public virtual string Nick { get; set; }

        /// <summary>
        /// Imię
        /// </summary>
        public virtual string Firstname { get; set; }

        /// <summary>
        /// Nazwisko
        /// </summary>
        public virtual string Lastname { get; set; }

        /// <summary>
        /// Stanowisko
        /// </summary>
        public virtual Position Position { get; set; }

        /// <summary>
        /// Restauracja, w której zatrudniony jest pracownik
        /// </summary>
        public virtual Restaurant.Restaurant Restaurant { get; set; }

        /// <summary>
        /// Użytkownik aplikacji
        /// </summary>
        public virtual AppUser AppUser { get; set; }

        /// <summary>
        /// Czy pracownik jest nadal pracownikiem restauracji
        /// </summary>
        public virtual bool Active { get; set; }
    }

    /// <summary>
    /// Stanowisko
    /// </summary>
    public enum Position
    {
        [Display(Name="Kelner")]
        Waiter = 1,
        [Display(Name = "Kucharz")]
        Cook = 2,
        [Display(Name = "Manager")]
        Manager = 3
    }
}