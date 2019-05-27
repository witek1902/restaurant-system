namespace OrderManagementSystem.Domain.Order
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using Common;
    using User;

    /// <summary>
    /// Zamówienie klienta
    /// </summary>
    public class Order : Entity<Guid>
    {
        /// <summary>
        /// Status of the order
        /// </summary>
        public virtual OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Data utworzenia zamówienia
        /// </summary>
        public virtual DateTime CreationDate { get; set; }

        /// <summary>
        /// Data zakończenia procesowania zamówienia 
        /// </summary>
        public virtual DateTime? FinishedDate { get; set; }

        /// <summary>
        /// Waiter przypisany do zamówienia
        /// </summary>
        public virtual RestaurantWorker Waiter { get; set; }

        /// <summary>
        /// Kuchar przygotowujący zamówienie
        /// </summary>
        public virtual RestaurantWorker Cook { get; set; }

        /// <summary>
        /// Customer składający zamówienie
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Table number
        /// </summary>
        public virtual int? TableNumber { get; set; }

        /// <summary>
        /// Order rating przez klienta w gwiazdkach (1-5)
        /// </summary>
        public virtual int? Rate { get; set; }

        /// <summary>
        /// Dodatkowe informacje dotyczące oceny zamówienia
        /// </summary>
        public virtual string RateDetails { get; set; }

        /// <summary>
        /// Pozycje, z których składa się zamówienie
        /// </summary>
        public virtual ICollection<OrderItem.OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Comments to order
        /// </summary>
        public virtual string Comments { get; set; }
    }

    /// <summary>
    /// Statusy zamówienia
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Zamówienie otwarte (Customer cały czas może dodawać nowe produkty do zamówienia)
        /// </summary>
        [Display(Name="Otwarte")]
        Open = 1,
        /// <summary>
        /// Przypisane do kelnera
        /// </summary>
        [Display(Name = "Przypisane do kelnera")]
        AssignedToWaiter = 2,
        /// <summary>
        /// Zamknięte zamówienie (Customer chce zapłacić i opuścić restauracje)
        /// </summary>
        [Display(Name="Zamknięte")]
        Closed = 3,
        /// <summary>
        /// Zamówienie opłacone
        /// </summary>
        [Display(Name="Opłacone")]
        Paid = 4,
        /// <summary>
        /// Zamówienie odrzucone (Customer się rozmyślił)
        /// </summary>
        [Display(Name = "Odrzucone")]
        Rejected = 5
    }
}