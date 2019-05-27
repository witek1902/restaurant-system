using OrderManagementSystem.Domain.Order.OrderItem;

namespace OrderManagementSystem.Models.Order
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Pojedyncza pozycja na zamówieniu
    /// </summary>
    public class OrderItemForm
    {
        public Guid? OrderItemId { get; set; }

        public Guid? OrderId { get; set; }

        public OrderItemStatus OrderItemStatus { get; set; }

        public Guid? ProductId { get; set; }

        public Guid? ProductCategoryId { get; set; }

        [Display(Name = "Name menu")]
        public string MenuName { get; set; }

        [Display(Name = "Name of the category")]
        public string ProductCategoryName { get; set; }

        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [Display(Name="Picture")]
        public string ProductPhotoUrl { get; set; }

        [Display(Name="Description")]
        public string ProductDescription { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}