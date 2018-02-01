namespace OrderManagementSystem.Models.Order
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using Domain.Order;
    using Restaurant;

    /// <summary>
    /// Formularz składania zamówienia
    /// </summary>
    public class OrderForm
    {
        /// <summary>
        /// Id zamówienia
        /// </summary>
        public Guid? OrderId { get; set; }

        /// <summary>
        /// Id restauracji, w której zostało żłożone zamówienie
        /// </summary>
        public Guid? RestaurantId { get; set; }

        [Display(Name="Restauracja")]
        public string RestaurantName { get; set; }

        [Display(Name="Data utworzenia")]
        public DateTime OrderCreationDate { get; set; }

        [Display(Name = "Data zakończenia")]
        public DateTime? OrderFinishedDate { get; set; }

        [Display(Name = "Ocena")]
        public int? OrderRate { get; set; }

        [Display(Name = "Ocena klienta")]
        public string OrderRateDetails { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Uwagi do zamówienia")]
        public string OrderComments { get; set; }

        [Display(Name = "Aktualny status zamówienia")]
        public OrderStatus OrderStatus { get; set; }

        public Guid CustomerId { get; set; }

        [Display(Name = "Klient")]
        public string CustomerFullName { get; set; }

        public Guid? WaiterId { get; set; }

        [Display(Name = "Kelner")]
        public string WaiterFirstName { get; set; }

        public Guid? CookId { get; set; }

        [Display(Name = "Kucharz")]
        public string CookFirstName { get; set; }

        [Display(Name="Numer stolika")]
        public int? TableNumber { get; set; }

        [Display(Name = "Suma")]
        public decimal TotalPrice
        {
            get { return OrderItems.Any() ? OrderItems.Select(x => x.ProductPrice * x.Quantity).Sum() : 0; }
        }

        /// <summary>
        /// Aktualnie wybrane menu
        /// </summary>
        [Display(Name="Menu")]
        public MenuForm ActualSelectedMenuId { get; set; }

        /// <summary>
        /// Menu, z których mogę wybierać produkty
        /// </summary>
        public List<MenuForm> Menus { get; set; }

        /// <summary>
        /// Pozycje zamówienia
        /// </summary>
        public List<OrderItemForm> OrderItems { get; set; }

        public OrderForm()
        {
            Menus = new List<MenuForm>();
            OrderItems = new List<OrderItemForm>();
        }

        public OrderForm(List<MenuForm> menus)
        {
            Menus = new List<MenuForm> {new MenuForm()};
            Menus.AddRange(menus);
            OrderItems = new List<OrderItemForm>();
        }

        public OrderForm(OrderForm receivedForm, List<MenuForm> menus)
        {
            Menus = menus;
            this.TableNumber = receivedForm.TableNumber;
        }
    }
}