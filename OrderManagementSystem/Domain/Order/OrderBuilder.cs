namespace OrderManagementSystem.Domain.Order
{
    using System.Collections.Generic;
    using Product;
    using OrderItem;
    using System;
    using User;
    using Models.Order;
    using NHibernate;
    using Infrastructure.Service;

    /// <summary>
    /// Builder do zamówień i elementów zamowien
    /// </summary>
    public class OrderBuilder : BusinessService
    {
        /// <summary>
        /// Construct new instance, expects NHibernate session to be injcected
        /// </summary>
        public OrderBuilder(ISession session) : base(session)
        {
        }

        public Order ConstructOrderEntity(OrderForm orderForm)
        {
            var order = new Order
            {
                Customer = new Customer
                {
                    Id = orderForm.CustomerId
                },
                Comments = orderForm.OrderComments,
                CreationDate = DateTime.Now,
                OrderStatus = OrderStatus.Open,
                TableNumber = orderForm.TableNumber,
                OrderItems = new List<OrderItem.OrderItem>()
            };

            foreach(var orderItem in orderForm.OrderItems)
                order.OrderItems.Add(new OrderItem.OrderItem
                {
                    Order = order,
                    Product = new Product
                    {
                        Id = orderItem.ProductId.Value
                    },
                    Quantity = orderItem.Quantity,
                    OrderItemStatus = OrderItemStatus.New
                });

            return order;
        }

        public void UpdateOrderEntity(Order order, OrderForm orderForm)
        {
            order.Comments = orderForm.OrderComments;
            order.TableNumber = order.TableNumber;
        }

        /// <summary>
        /// Tworzenie nowego elementu zamówienia
        /// </summary>
        /// <param name="orderItemForm">Element zamówienia</param>
        /// <returns></returns>
        public OrderItem.OrderItem ConstructOrderItemEntity(OrderItemForm orderItemForm)
        {
            var orderItem = new OrderItem.OrderItem
            {
                OrderItemStatus = OrderItemStatus.New,
                Product = new Product
                {
                    Id = orderItemForm.ProductId.Value
                },
                Quantity = orderItemForm.Quantity,
                Order = new Order
                {
                    Id = orderItemForm.OrderId.Value
                }
            };

            return orderItem;
        }
    }
}