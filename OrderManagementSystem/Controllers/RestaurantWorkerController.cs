namespace OrderManagementSystem.Controllers
{
    using Domain.Restaurant;
    using Domain.Order;
    using Domain.User;
    using System;
    using System.Collections.Generic;
    using Domain.Order.OrderItem;
    using Infrastructure.Security;
    using Models.Order;
    using System.Web.Mvc;

    /// <summary>
    /// Controller for a restaurant employee
    /// </summary>
    [Authorize(Roles="waiters,cooks")]
    public class RestaurantWorkerController : Infrastructure.Web.ControllerBase
    {
        private RestaurantWorkerEnum position;
        private Guid? restaurantId;
        private Guid? restaurantWorkerId;

        /// <summary>
        /// Show orders to handle
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if(restaurantId == null)
                InitialInfo();

            var orders = new Dictionary<OrderTypeEnum, List<OrderForm>>();

            if (position == RestaurantWorkerEnum.Waiter)
            {
                orders.Add(OrderTypeEnum.New, Query(new GetWaiterNewOrdersQuery(restaurantId.Value)));
                orders.Add(OrderTypeEnum.InProgress, Query(new GetWaiterActualOrdersQuery(Security.CurrentUserId)));
            }
            else if (position == RestaurantWorkerEnum.Cook)
            {
                orders.Add(OrderTypeEnum.New, Query(new GetCookNewOrdersQuery(restaurantId.Value)));
                orders.Add(OrderTypeEnum.InProgress, Query(new GetCookActualOrdersQuery(Security.CurrentUserId)));
            }

            return View(orders);
        }

        /// <summary>
        /// Downloading the history of orders handled for a restaurant employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult History()
        {
            if (restaurantId == null)
                InitialInfo();

            var orders = new List<OrderForm>();

            if (position == RestaurantWorkerEnum.Waiter)
            {
                orders = Query(new GetWaiterHistoryOrdersQuery(Security.CurrentUserId));
            }
            else if(position == RestaurantWorkerEnum.Cook)
            {
                orders = Query(new GetCookHistoryOrdersQuery(Security.CurrentUserId));
            }

            return View(orders);
        }

        /// <summary>
        /// The order is assigned to the waiter
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <returns></returns>
        public ActionResult AssignToWaiterOrder(Guid orderId)
        {
            if (restaurantId == null)
                InitialInfo();

            var cmdResult = ExecuteCommand(new AssignOrderCommand(orderId, restaurantWorkerId.Value));

            if(cmdResult.Success)
                return RedirectToAction("Index");
            else
                return new HttpStatusCodeResult(400, cmdResult.MessageForHumans);
        }

        /// <summary>
        /// The line item is marked as 'Delivered to the table'
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns></returns>
        public ActionResult MarkAsDeliveredOrderItem(Guid orderItemId)
        {
            if (restaurantId == null)
                InitialInfo();

            var cmdResult = ExecuteCommand(new MarkAsDeliveredOrderItemCommand(orderItemId));

            if (cmdResult.Success)
                return PartialView("_OrderItemInTable", OrderMapper.MapOrderItemToForm(cmdResult.Result));
            else
                return new HttpStatusCodeResult(400, cmdResult.MessageForHumans);
        }

        /// <summary>
        /// The order is marked as 'Paid'
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ActionResult MarkAsPaidOrder(Guid orderId)
        {
            if (restaurantId == null)
                InitialInfo();

            var cmdResult = ExecuteCommand(new MarkAsPaidOrderCommand(orderId));

            if (cmdResult.Success)
                return RedirectToAction("Index");
            else
                return new HttpStatusCodeResult(400, cmdResult.MessageForHumans);
        }

        /// <summary>
        /// Cook means the line item as 'In preparation in the kitchen'
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns></returns>
        public ActionResult MarkAsInProgressOrderItem(Guid orderItemId)
        {
            if (restaurantId == null)
                InitialInfo();

            var cmdResult = ExecuteCommand(new MarkAsInProgressOrderItemCommand(orderItemId, restaurantWorkerId.Value));

            if (cmdResult.Success)
                return RedirectToAction("Index");
            else
                return new HttpStatusCodeResult(400, cmdResult.MessageForHumans);
        }

        /// <summary>
        /// Cook means the line item is 'Ready to release'
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns></returns>
        public ActionResult MarkAsReadyOrderItem(Guid orderItemId)
        {
            if (restaurantId == null)
                InitialInfo();

            var cmdResult = ExecuteCommand(new MarkAsReadyOrderItemCommand(orderItemId));

            if (cmdResult.Success)
                return PartialView("_OrderItemInTable", OrderMapper.MapOrderItemToForm(cmdResult.Result));
            else
                return new HttpStatusCodeResult(400, cmdResult.MessageForHumans);
        }

        private void InitialInfo()
        {
            if (Security.IsUserInRole("waiters"))
            {
                position = RestaurantWorkerEnum.Waiter;
            }
            else if (Security.IsUserInRole("cooks"))
            {
                position = RestaurantWorkerEnum.Cook;
            }

            restaurantId = Query(new GetRestaurantIdByUserIdQuery(Security.CurrentUserId));
            restaurantWorkerId = Query(new GetRestaurantWorkerIdByUserIdQuery(Security.CurrentUserId));
        }

    }

    /// <summary>
    /// Staff positions in the restaurant(without the Manager)
    /// </summary>
    public enum RestaurantWorkerEnum
    {
        /// <summary>
        /// Waiter
        /// </summary>
        Waiter,
        /// <summary>
        /// Cook
        /// </summary>
        Cook
    }

    /// <summary>
    /// Order type
    /// </summary>
    public enum OrderTypeEnum
    {
        /// <summary>
        /// new
        /// </summary>
        New,
        /// <summary>
        ///  In progress
        /// </summary>
        InProgress
    }
}