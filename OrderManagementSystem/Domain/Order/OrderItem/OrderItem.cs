namespace OrderManagementSystem.Domain.Order.OrderItem
{
    using System.ComponentModel.DataAnnotations;
    using Product;
    using System;
    using Common;

    /// <summary>
    /// Pozycja zamówienia
    /// </summary>
    public class OrderItem : Entity<Guid>
    {
        /// <summary>
        /// Produkt
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Zamówienie, do którego należy pozycja
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// Ilość produktu
        /// </summary>
        public virtual int Quantity { get; set; }

        /// <summary>
        /// Aktualny status pozycji zamówienia
        /// </summary>
        public virtual OrderItemStatus OrderItemStatus { get; set; }
    }

    /// <summary>
    /// Statusy pozycji zamówienia
    /// </summary>
    public enum OrderItemStatus
    {
        /// <summary>
        /// Nowa pozycja na zamówieniu
        /// </summary>
        [Display(Name="Nowa")]
        New = 1,
        /// <summary>
        /// Pozycja potwierdzona
        /// </summary>
        [Display(Name="Potwierdzona")]
        Approved = 2,
        /// <summary>
        /// Pozycja w przygotowaniu 'W kuchni'
        /// </summary>
        [Display(Name = "W przygotowaniu w kuchni")]
        InProgressInKitchen = 3,
        /// <summary>
        /// Pozycja gotowa do podania
        /// </summary>
        [Display(Name = "Gotowa do podania")]
        Ready = 4,
        /// <summary>
        /// Pozycja dostarczona do stolika
        /// </summary>
        [Display(Name="Dostarczona do stolika")]
        Delivered = 5
    }
}