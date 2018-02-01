namespace OrderManagementSystem.Models.Restaurant
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System;

    /// <summary>
    /// Formularz do edytowania/tworzenia restauracji
    /// </summary>
    public class RestaurantForm
    {
        /// <summary>
        /// Id restauracji
        /// </summary>
        public Guid? RestaurantId { get; set; }

        [Required]
        [Display(Name="Nazwa")]
        public string RestaurantName { get; set; }

        [Display(Name="Link do zdjęcia")]
        public string RestaurantPhotoUrl { get; set; }

        [Required]
        [Display(Name="Unikalny kod restauracji")]
        public string RestaurantCode { get; set; }

        [Required]
        [Display(Name ="Miasto")]
        public string RestaurantCity { get; set; }

        [Required]
        [Display(Name="Ulica")]
        public string RestaurantStreet { get; set; }

        [Required]
        [Display(Name="Kod pocztowy")]
        public string RestaurantPostalCode { get; set; }

        [Required]
        [Display(Name="Numer budynku")]
        public int RestaurantStreetNumber { get; set; }

        [Display(Name="Numer lokalu")]
        public int? RestaurantFlatNumber { get; set; }

        /// <summary>
        /// Id managera
        /// </summary>
        public Guid? ManagerId { get; set; }

        public int? ManagerAppUserId { get; set; }

        [Display(Name = "Login")]
        public string ManagerLogin { get; set; }

        [Display(Name = "Hasło")]
        public string ManagerPassword { get; set; }

        [Display(Name="Imię managera")]
        public string ManagerFirstname { get; set; }

        [Display(Name="Pseudonim")]
        public string ManagerNick { get; set; }

        [Display(Name="Nazwisko managera")]
        public string ManagerLastname { get; set; }

        /// <summary>
        /// Lista pracowników restauracji
        /// </summary>
        public List<RestaurantWorkerForm> RestaurantWorkers { get; set; }

        /// <summary>
        /// Lista menu dostępnych w restauracji
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