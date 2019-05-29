namespace OrderManagementSystem.Models.Customer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Customer form
    /// </summary>
    public class CustomerForm
    {
        /// <summary>
        /// Customer Id
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