namespace OrderManagementSystem.Models.Restaurant
{
    using System.ComponentModel.DataAnnotations;
    using Domain.User;
    using System;

    /// <summary>
    /// Form of a restaurant worker
    /// </summary>
    public class RestaurantWorkerForm
    {
        /// <summary>
        /// Id of a restaurant employee
        /// </summary>
        public Guid? RestaurantWorkerId { get; set; }

        [Display(Name= "Active")]
        public bool Active { get; set; }

        [Display(Name = "Position")]
        public Position Position { get; set; }

        [Display(Name="Pseudonym")]
        public string Nick { get; set; }

        [Required]
        [Display(Name = "Name of the employee")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Name of the employee")]
        public string Lastname { get; set; }

        public int AppUserId { get; set; }

        [Display(Name="Login")]
        public string Login { get; set; }

        [Display(Name="Password ")]
        public string Password { get; set; }

        /// <summary>
        /// Id of the restaurant
        /// </summary>
        public Guid? RestaurantId { get; set; }

        [Display(Name = "Name of the restaurant")]
        public string RestaurantName { get; set; }

        public RestaurantWorkerForm() { }

        /// <summary>
        /// Reconstruction of the form from data sent by the user
        /// </summary>
        /// <param name="receivedForm">Entered data</param>
        public RestaurantWorkerForm(RestaurantWorkerForm receivedForm)
        {
            this.RestaurantWorkerId = receivedForm.RestaurantWorkerId;
            this.Firstname = receivedForm.Firstname;
            this.Lastname = receivedForm.Lastname;
            this.Nick = receivedForm.Nick;
            this.Position = receivedForm.Position;
            this.RestaurantName = receivedForm.RestaurantName;
            this.RestaurantId = receivedForm.RestaurantId;
            this.Login = receivedForm.Login;
            this.Password = receivedForm.Password;
        }
    }
}