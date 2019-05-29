namespace OrderManagementSystem.Models.Order
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using Domain.Order;
    using Restaurant;

    /// <summary>
    /// Order form
    /// </summary>
    public class OrderForm
    {
        /// <summary>
        /// Order Id
        /// </summary>
        public Guid? OrderId { get; set; }

        /// <summary>
        /// Id of the restaurant where the order was placed
        /// </summary>
        public Guid? RestaurantId { get; set; }

        [Display(Name= "Restaurant")]
        public string RestaurantName { get; set; }

        [Display(Name= "Date created")]
        public DateTime OrderCreationDate { get; set; }

        [Display(Name = "end date")]
        public DateTime? OrderFinishedDate { get; set; }

        [Display(Name = "Rating")]
        public int? OrderRate { get; set; }

        [Display(Name = "Customer rating")]
        public string OrderRateDetails { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comments to order")]
        public string OrderComments { get; set; }

        [Display(Name = "Current Status of the order")]
        public OrderStatus OrderStatus { get; set; }

        public Guid CustomerId { get; set; }

        [Display(Name = "Customer")]
        public string CustomerFullName { get; set; }

        public Guid? WaiterId { get; set; }

        [Display(Name = "Waiter")]
        public string WaiterFirstName { get; set; }

        public Guid? CookId { get; set; }

        [Display(Name = "Cook")]
        public string CookFirstName { get; set; }

        [Display(Name="Table number")]
        public int? TableNumber { get; set; }

        [Display(Name = "Sum")]
        public decimal TotalPrice
        {
            get { return OrderItems.Any() ? OrderItems.Select(x => x.ProductPrice * x.Quantity).Sum() : 0; }
        }

        /// <summary>
        /// Currently selected menu
        /// </summary>
        [Display(Name="Menu")]
        public MenuForm ActualSelectedMenuId { get; set; }

        /// <summary>
        /// Menu from which I can choose products
        /// </summary>
        public List<MenuForm> Menus { get; set; }

        /// <summary>
        /// Items of the order
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