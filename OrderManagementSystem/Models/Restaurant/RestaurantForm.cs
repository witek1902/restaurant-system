namespace OrderManagementSystem.Models.Restaurant
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System;

    /// <summary>
    /// Form for editing / creating restaurants
    /// </summary>
    public class RestaurantForm
    {
        /// <summary>
        /// Id of the restaurant
        /// </summary>
        public Guid? RestaurantId { get; set; }

        [Required]
        [Display(Name="Name")]
        public string RestaurantName { get; set; }

        [Display(Name="Link to the photo")]
        public string RestaurantPhotoUrl { get; set; }

        [Required]
        [Display(Name="A unique restaurant code")]
        public string RestaurantCode { get; set; }

        [Required]
        [Display(Name ="City")]
        public string RestaurantCity { get; set; }

        [Required]
        [Display(Name="Street")]
        public string RestaurantStreet { get; set; }

        [Required]
        [Display(Name="ZIP code")]
        public string RestaurantPostalCode { get; set; }

        [Required]
        [Display(Name="Number of the building")]
        public int RestaurantStreetNumber { get; set; }

        [Display(Name="House number")]
        public int? RestaurantFlatNumber { get; set; }

        /// <summary>
        /// Id manager
        /// </summary>
        public Guid? ManagerId { get; set; }

        public int? ManagerAppUserId { get; set; }

        [Display(Name = "Login")]
        public string ManagerLogin { get; set; }

        [Display(Name = "Password ")]
        public string ManagerPassword { get; set; }

        [Display(Name="Manager's Name")]
        public string ManagerFirstname { get; set; }

        [Display(Name="Pseudonym")]
        public string ManagerNick { get; set; }

        [Display(Name="Manager's Name")]
        public string ManagerLastname { get; set; }

        /// <summary>
        /// List of restaurant staff
        /// </summary>
        public List<RestaurantWorkerForm> RestaurantWorkers { get; set; }

        /// <summary>
        /// List of menus available in the restaurant
        /// </summary>
        public List<MenuForm> Menus { get; set; } 

        public RestaurantForm() { }

        public RestaurantForm(RestaurantForm receivedRestaurantForm)
        {
            RestaurantId = receivedRestaurantForm.RestaurantId;
            RestaurantCode = receivedRestaurantForm.RestaurantCode;
            RestaurantName = receivedRestaurantForm.RestaurantName;
            RestaurantCity = receivedRestaurantForm.RestaurantCity;
            RestaurantPostalCode = receivedRestaurantForm.RestaurantPostalCode;
            RestaurantStreet = receivedRestaurantForm.RestaurantStreet;
            RestaurantStreetNumber = receivedRestaurantForm.RestaurantStreetNumber;
            RestaurantFlatNumber = receivedRestaurantForm.RestaurantFlatNumber;
            ManagerFirstname = receivedRestaurantForm.ManagerFirstname;
            ManagerLastname = receivedRestaurantForm.ManagerLastname;
            ManagerId = receivedRestaurantForm.ManagerId;
            ManagerLogin = receivedRestaurantForm.ManagerLogin;
            //TODO Menu + Workers
        }
    }
}