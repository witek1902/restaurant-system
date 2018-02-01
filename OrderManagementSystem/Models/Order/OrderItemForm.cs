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

        [Display(Name = "Nazwa menu")]
        public string MenuName { get; set; }

        [Display(Name = "Nazwa kategorii")]
        public string ProductCategoryName { get; set; }

        [Display(Name = "Nazwa")]
        public string ProductName { get; set; }

        [Display(Name="Zdjęcie")]
        public string ProductPhotoUrl { get; set; }

        [Display(Name="Opis")]
        public string ProductDescription { get; set; }

        [Display(Name = "Cena")]
        [DataType(DataType.Currency)]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Ilość")]
        public int Quantity { get; set; }
    }
}