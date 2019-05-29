namespace OrderManagementSystem.Controllers
{
    using Domain.User;
    using System.Collections.Generic;
    using Domain.Order.OrderItem;
    using Models.Customer;
    using Models.Product;
    using Domain.Order;
    using Infrastructure.Security;
    using Models.Order;
    using System;
    using Models.Restaurant;
    using System.Web.Mvc;

    /// <summary>
    /// Controller for the restaurant client
    /// </summary>
    [Authorize(Roles="customers")]
    public class CustomerController : Infrastructure.Web.ControllerBase
    {
        /// <summary>
        /// The customer's initial panel
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Editing an existing client
        /// </summary>
        /// <returns>View with edition</returns>
        [HttpGet]
        public ActionResult EditCustomer()
        {
            var customerForm = Query(new GetCustomerByUserIdQuery(Security.CurrentUserId));

            return View(customerForm);
        }

        /// <summary>
        /// Saves changes made on the client
        /// </summary>
        /// <param name="customerForm">Customer form</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveCustomer(CustomerForm customerForm)
        {
            ExecuteCommand(new UpdateCustomerCommand(customerForm));
            return RedirectToAction("EditCustomer", new { customer = customerForm.CustomerId });
        }

        /// <summary>
        /// Search for restaurants
        /// </summary>
        /// <param name="vm">The search model</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(RestaurantSearchViewModel vm)
        {
            vm.Results = Query(new SearchRestaurantQuery(vm.Criteria));

            return View(vm);
        }

        /// <summary>
        /// Screen to order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PlaceOrder(Guid restaurantId)
        {
            var openOrder = Query(new GetOpenOrdersInActualRestaurantByUserIdQuery(Security.CurrentUserId, restaurantId));
            var menus = Query(new GetMenusByRestaurantIdQuery(restaurantId));

            if (openOrder != null)
            {
                openOrder.Menus = menus;
                return View(openOrder);
            }
            else
            {
                var orderForm = new OrderForm(menus);
                return View(orderForm);
            }
        }

        /// <summary>
        /// Downloading products from the Menu, used by AJAX shots
        /// </summary>
        /// <param name="menuId">Id menu</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RenderProductsFromMenu(Guid menuId)
        {
            var products = Query(new GetProductsByMenuQuery(menuId));

            return PartialView("_ProductInMenu", new MenuForm {Products = products });
        }

        /// <summary>
        /// Downloading partialView of the current order view
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetActualOrder(Guid orderId)
        {
            var order = Query(new GetOrderQuery(orderId));

            return PartialView("_ActualOrder", order);
        }

        /// <summary>
        /// Adding an item to the order(or creating a new one if it's the first item)
        /// </summary>
        /// <param name="orderItemForm">Line item</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrderItem(OrderItemForm orderItemForm)
        {
            if (orderItemForm.OrderId.HasValue)
            {
                var cmdResult = ExecuteCommand(new AddOrderItemCommand(orderItemForm));

                var order = Query(new GetOrderByOrderItemIdQuery(cmdResult.Result));
                
                return PartialView("_ActualOrder", order);
            }
            else
            {
                var customer = Query(new GetCustomerByUserIdQuery(Security.CurrentUserId));
                var order = new OrderForm
                {
                    CustomerId = customer.CustomerId.Value,
                    OrderItems = new List<OrderItemForm>()
                };
                order.OrderItems.Add(orderItemForm);

                var cmdResult = ExecuteCommand(new CreateOrderCommand(order));
                var insertedOrder = Query(new GetOrderQuery(cmdResult.Result));

                return PartialView("_ActualOrder", insertedOrder);
            }
        }

        /// <summary>
        /// Delete the line item
        /// </summary>
        /// <param name="orderItemId">The line item id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteOrderItem(Guid orderItemId)
        {
            var cmdResult = ExecuteCommand(new DeleteOrderItemCommand(orderItemId));

            var order = Query(new GetOrderQuery(cmdResult.Result));

            return PartialView("_ActualOrder", order);
        }

        /// <summary>
        /// Change in the quantity of product on the line item
        /// </summary>
        /// <param name="orderItem">Line item</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeQuantity(OrderItemForm orderItem)
        {
            var cmdResult = ExecuteCommand(new ChangeQuantityOrderItemCommand(orderItem));

            var order = Query(new GetOrderQuery(cmdResult.Result));

            return PartialView("_ActualOrder", order);
        }

        /// <summary>
        /// Removal of the order
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteOrder(Guid? orderId)
        {
            if (orderId.HasValue)
                ExecuteCommand(new DeleteOrderCommand(orderId.Value));
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Confirmation of the order        
        /// </summary>
        /// <param name="orderForm">Current order</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PlaceOrder(OrderForm orderForm)
        {
            var order = Query(new GetOrderQuery(orderForm.OrderId.Value));
            order.TableNumber = orderForm.TableNumber;
            order.OrderComments = orderForm.OrderComments;

            var cmdResult = ExecuteCommand(new PlaceOrderCommand(order));

            return RedirectToAction("ActualOrderDetails" , new { orderId = cmdResult.Result });
        }

        /// <summary>
        /// Details of the current order
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ActualOrderDetails(Guid orderId)
        {
            var orderForm = Query(new GetOrderQuery(orderId));
            Response.AddHeader("Refresh", "60");

            return View(orderForm);
        }

        /// <summary>
        /// The History of Customer's Orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderHistory()
        {
            var orderForms = Query(new GetCustomerHistoryOrdersQuery(Security.CurrentUserId));

            return View(orderForms);
        }

        /// <summary>
        /// Preview of historical order details
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderDetails(Guid orderId)
        {
            var orderForm = Query(new GetOrderQuery(orderId));

            return View(orderForm);
        }

        /// <summary>
        /// Customer expresses the willingness to pay for the order
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WantToPay(Guid orderId)
        {
            var cmdResult = ExecuteCommand(new CloseOrderCommand(orderId));

            if(cmdResult.Success)
                return RedirectToAction("RateOrder", new {orderId = orderId});
            else
                return new HttpStatusCodeResult(400, "Not all Line items have been delivered to the table, wait a minute! :)");
        }

        /// <summary>
        /// Order evaluation view
        /// </summary>
        /// <param name="orderId">Id of the order being assessed</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RateOrder(Guid orderId)
        {
            return View(new RateOrderForm { OrderId = orderId });
        }

        /// <summary>
        /// Sending the order evaluation
        /// </summary>
        /// <param name="rateForm">Order evaluation form</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RateOrder(RateOrderForm rateForm)
        {
            var cmdResult = ExecuteCommand(new RateOrderCommand(rateForm));

            if (cmdResult.Success)
                return RedirectToAction("ActualOrderDetails", new {orderId = rateForm.OrderId});
            else
                return new HttpStatusCodeResult(400, cmdResult.MessageForHumans);
        }
    }
}