﻿namespace OrderManagementSystem.Models.Order
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Mapper for orders
    /// </summary>
    public static class OrderMapper
    {
        /// <summary>
        /// Entity mapping on the form
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>The order form</returns>
        public static OrderForm MapOrderToForm(Domain.Order.Order order)
        {
            var form = new OrderForm
            {
                CustomerFullName = order.Customer?.Firstname,
                CustomerId = order.Customer.Id,
                CookFirstName = order.Cook?.Firstname,
                CookId = order.Cook?.Id,
                RestaurantId = order.Waiter?.Restaurant.Id,
                RestaurantName = order.Waiter?.Restaurant.Name,
                WaiterFirstName = order.Waiter?.Firstname,
                WaiterId = order.Waiter?.Id,
                OrderId = order.Id,
                OrderComments = order.Comments,
                OrderCreationDate = order.CreationDate,
                OrderFinishedDate = order.FinishedDate,
                OrderRate = order.Rate,
                OrderRateDetails = order.RateDetails,
                OrderStatus = order.OrderStatus,
                TableNumber = order.TableNumber,
                OrderItems = new List<OrderItemForm>()
            };

            if (order.OrderItems != null && order.OrderItems.Any())
            {
                foreach (var orderItem in order.OrderItems)
                {
                    form.OrderItems.Add(MapOrderItemToForm(orderItem));
                }

                var restaurantInfo = order.OrderItems.First().Product.Menu.Restaurant;
                form.RestaurantId = restaurantInfo.Id;
                form.RestaurantName = restaurantInfo.Name;
            }
                

            return form;
        }

        /// <summary>
        /// Mapping a single item to the form
        /// </summary>
        /// <param name="orderItem">Position in the order</param>
        /// <returns>Formularz</returns>
        public static OrderItemForm MapOrderItemToForm(Domain.Order.OrderItem.OrderItem orderItem)
        {
            var form = new OrderItemForm
            {
                ProductCategoryId = orderItem.Product.ProductCategory.Id,
                MenuName = orderItem.Product.Menu.Name,
                ProductCategoryName = orderItem.Product.ProductCategory.Name,
                OrderItemStatus = orderItem.OrderItemStatus,
                ProductName = orderItem.Product.Name,
                ProductPhotoUrl = orderItem.Product.PhotoUrl,
                ProductId = orderItem.Product.Id,
                ProductDescription = orderItem.Product.Description,
                ProductPrice = orderItem.Product.Price,
                Quantity = orderItem.Quantity,
                OrderItemId = orderItem.Id
            };
            return form;
        }
    }
}