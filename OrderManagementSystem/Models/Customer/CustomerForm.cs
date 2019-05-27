namespace OrderManagementSystem.Models.Customer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Formularz klienta
    /// </summary>
    public class CustomerForm
    {
        /// <summary>
        /// Id klienta
        /// </summary>
        public Guid? CustomerId { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public int AppUserId { get; set; }

        [Required]
        [Display(Name="Customer's name")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name="Login")]
        public string Login { get; set; }

        [Display(Name="Password ")]
        public string Password { get; set; }
    }
}