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
    /// Kontroler dla klienta restauracji
    /// </summary>
    [Authorize(Roles="customers")]
    public class CustomerController : Infrastructure.Web.ControllerBase
    {
        /// <summary>
        /// Panel początkowy klienta
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Edycja istniejącego klienta
        /// </summary>
        /// <returns>Widok z edycją</returns>
        [HttpGet]
        public ActionResult EditCustomer()
        {
            var customerForm = Query(new GetCustomerByUserIdQuery(Security.CurrentUserId));

            return View(customerForm);
        }

        /// <summary>
        /// Zapisuje zmiany wprowadzone na kliencie
        /// </summary>
        /// <param name="customerForm">Formularz klienta</param>
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
        /// <param name="vm">Model do wyszukiwania</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(RestaurantSearchViewModel vm)
        {
            vm.Results = Query(new SearchRestaurantQuery(vm.Criteria));

            return View(vm);
        }

        /// <summary>
        /// Ekran do składania zamówienia
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
        /// Pobranie produktów z Menu, wykorzystywana przez strzały AJAXowe
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
        /// Pobranie partialView widoku aktualnego zamówienia
        /// </summary>
        /// <param name="orderId">Id zamówienia</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetActualOrder(Guid orderId)
        {
            var order = Query(new GetOrderQuery(orderId));

            return PartialView("_ActualOrder", order);
        }

        /// <summary>
        /// Dodanie elementu do zamówieniu (lub stworzenie nowego, jeśli to pierwszy element)
        /// </summary>
        /// <param name="orderItemForm">Element zamówienia</param>
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
        /// Usunięcie elementu zamówienia
        /// </summary>
        /// <param name="orderItemId">Id elementu zamówienia</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteOrderItem(Guid orderItemId)
        {
            var cmdResult = ExecuteCommand(new DeleteOrderItemCommand(orderItemId));

            var order = Query(new GetOrderQuery(cmdResult.Result));

            return PartialView("_ActualOrder", order);
        }

        /// <summary>
        /// Zmiana ilości produktu na elemencie zamówienia
        /// </summary>
        /// <param name="orderItem">Element zamówienia</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeQuantity(OrderItemForm orderItem)
        {
            var cmdResult = ExecuteCommand(new ChangeQuantityOrderItemCommand(orderItem));

            var order = Query(new GetOrderQuery(cmdResult.Result));

            return PartialView("_ActualOrder", order);
        }

        /// <summary>
        /// Usunięcie zamówienia
        /// </summary>
        /// <param name="orderId">Id zamówienia</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteOrder(Guid? orderId)
        {
            if (orderId.HasValue)
                ExecuteCommand(new DeleteOrderCommand(orderId.Value));
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Zatwierdzenie zamówienia
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
        /// Details aktualnego zamówienia
        /// </summary>
        /// <param name="orderId">Id zamówienia</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ActualOrderDetails(Guid orderId)
        {
            var orderForm = Query(new GetOrderQuery(orderId));
            Response.AddHeader("Refresh", "60");

            return View(orderForm);
        }

        /// <summary>
        /// The History of Orders klienta
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderHistory()
        {
            var orderForms = Query(new GetCustomerHistoryOrdersQuery(Security.CurrentUserId));

            return View(orderForms);
        }

        /// <summary>
        /// Podgląd szczegółów historycznego zamówienia
        /// </summary>
        /// <param name="orderId">Id zamówienia</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderDetails(Guid orderId)
        {
            var orderForm = Query(new GetOrderQuery(orderId));

            return View(orderForm);
        }

        /// <summary>
        /// Customer wyraża chęć zapłaty za zamówienie
        /// </summary>
        /// <param name="orderId">Id zamówienia</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WantToPay(Guid orderId)
        {
            var cmdResult = ExecuteCommand(new CloseOrderCommand(orderId));

            if(cmdResult.Success)
                return RedirectToAction("RateOrder", new {orderId = orderId});
            else
                return new HttpStatusCodeResult(400, "Nie wszystkie Line items zostały dostarczone do stolika, poczekaj chwilkę! :)");
        }

        /// <summary>
        /// Widok oceny zamówienia
        /// </summary>
        /// <param name="orderId">Id ocenianego zamówienia</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RateOrder(Guid orderId)
        {
            return View(new RateOrderForm { OrderId = orderId });
        }

        /// <summary>
        /// Przesłanie oceny zamówienia
        /// </summary>
        /// <param name="rateForm">Formularz oceny zamówienia</param>
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