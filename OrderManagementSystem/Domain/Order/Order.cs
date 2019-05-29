namespace OrderManagementSystem.Domain.Order
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using Common;
    using User;

    /// <summary>
    /// Customers order
    /// </summary>
    public class Order : Entity<Guid>
    {
        /// <summary>
        /// Status of the order
        /// </summary>
        public virtual OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Order creation date        
        /// </summary>
        public virtual DateTime CreationDate { get; set; }

        /// <summary>
        /// Date of processing the order
        /// </summary>
        public virtual DateTime? FinishedDate { get; set; }

        /// <summary>
        /// Waiter assigned to the order
        /// </summary>
        public virtual RestaurantWorker Waiter { get; set; }

        /// <summary>
        /// Kuchar preparing the order
        /// </summary>
        public virtual RestaurantWorker Cook { get; set; }

        /// <summary>
        /// Customer placing an order
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Table number
        /// </summary>
        public virtual int? TableNumber { get; set; }

        /// <summary>
        /// Order rating by the client in asterisks (1-5)
        /// </summary>
        public virtual int? Rate { get; set; }

        /// <summary>
        /// Additional information about the order evaluation
        /// </summary>
        public virtual string RateDetails { get; set; }

        /// <summary>
        /// Items that make up the order
        /// </summary>
        public virtual ICollection<OrderItem.OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Comments to order
        /// </summary>
        public virtual string Comments { get; set; }
    }

    /// <summary>
    /// Order statuses
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Open order (Customer may add new products to the order at any time)
        /// </summary>
        [Display(Name= "Open")]
        Open = 1,
        /// <summary>
        /// Assigned to the waiter
        /// </summary>
        [Display(Name = "Assigned to the waiter")]
        AssignedToWaiter = 2,
        /// <summary>
        /// Closed order (Customer wants to pay and leave restaurants)
        /// </summary>
        [Display(Name= "Closed")]
        Closed = 3,
        /// <summary>
        /// Order paid
        /// </summary>
        [Display(Name= "Paid")]
        Paid = 4,
        /// <summary>
        /// Order rejected (Customer changed his mind)
        /// </summary>
        [Display(Name = "Rejected")]
        Rejected = 5
    }
}