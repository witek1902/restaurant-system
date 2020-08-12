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
        /// Quantity produktu
        /// </summary>
        public virtual int Quantity { get; set; }

        /// <summary>
        /// Aktualny status pozycji zamówienia
        /// </summary>
        public virtual OrderItemStatus OrderItemStatus { get; set; }
    }

    /// <summary>
    /// Status of the order item
    /// </summary>
    public enum OrderItemStatus
    {
        /// <summary>
        /// New item on the order
        /// </summary>
        [Display(Name= "New")]
        New = 1,
        /// <summary>
        /// Position confirmed
        /// </summary>
        [Display(Name= "Confirmed")]
        Approved = 2,
        /// <summary>
        /// Position in preparation 'In the kitchen'
        /// </summary>
        [Display(Name = "In preparation in the kitchen")]
        InProgressInKitchen = 3,
        /// <summary>
        /// Position ready to be served
        /// </summary>
        [Display(Name = "Ready to serve")]
        Ready = 4,
        /// <summary>
        /// Item delivered to the table
        /// </summary>
        [Display(Name= "Delivered to the table")]
        Delivered = 5
    }
}