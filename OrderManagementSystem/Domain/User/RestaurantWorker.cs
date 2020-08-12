namespace OrderManagementSystem.Domain.User
{
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Security;
    using System;
    using Common;

    /// <summary>
    /// Waiter in the restaurant
    /// </summary>
    public class RestaurantWorker : Entity<Guid>
    {
        /// <summary>
        /// "Pseudonym"
        /// </summary>
        public virtual string Nick { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public virtual string Firstname { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public virtual string Lastname { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public virtual Position Position { get; set; }

        /// <summary>
        /// The restaurant where Employee is employed
        /// </summary>
        public virtual Restaurant.Restaurant Restaurant { get; set; }

        /// <summary>
        /// Application user
        /// </summary>
        public virtual AppUser AppUser { get; set; }

        /// <summary>
        /// Is Employee still a restaurant employee
        /// </summary>
        public virtual bool Active { get; set; }
    }

    /// <summary>
    /// Position
    /// </summary>
    public enum Position
    {
        [Display(Name="Waiter")]
        Waiter = 1,
        [Display(Name = "Cook")]
        Cook = 2,
        [Display(Name = "Manager")]
        Manager = 3
    }
}