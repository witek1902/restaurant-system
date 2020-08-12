namespace OrderManagementSystem.Domain.Order
{
    using System.Linq;
    using NHibernate;
    using Common;
    using OrderItem;
    using Infrastructure.Exception;
    using Infrastructure.Service;

    /// <summary>
    /// Interface to manage order status
    /// </summary>
    public interface IOrderStatusService
    {
        /// <summary>
        /// Customer closes the order (he wants to pay, he will not order anything more)
        /// </summary>
        /// <param name="order"></param>
        void CloseOrder(Order order);

        /// <summary>
        /// Waiter assigns the order to himself
        /// </summary>
        /// <param name="order"></param>
        void AssignToWaiter(Order order);

        /// <summary>
        /// Waiter means the order is paid for.
        /// </summary>
        /// <param name="order"></param>
        void PaidOrder(Order order);

        /// <summary>
        /// Customer rejects the order
        /// </summary>
        /// <param name="order">Rejected order</param>
        void RejectOrder(Order order);
    }

    /// <summary>
    /// Implementation of the interface to manage the order status
    /// </summary>
    public class OrderStatusService : BusinessService, IOrderStatusService
    {
        public OrderStatusService(ISession session) : base(session)
        {
        }

        /// <summary>
        /// Waiter assigns the order to himself
        /// </summary>
        /// <param name="order"></param>
        public void AssignToWaiter(Order order)
        {
            if (order.OrderStatus == OrderStatus.Open)
                order.OrderStatus = OrderStatus.AssignedToWaiter;
            else
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Only the order in the status 'Open' can be assigned to the waiter.");
        }

        /// <summary>
        /// Customer closes the order (he wants to pay, he will not order anything more)
        /// </summary>
        /// <param name="order"></param>
        public void CloseOrder(Order order)
        {
            if (order.OrderStatus == OrderStatus.AssignedToWaiter && !order.OrderItems.Any(x => x.OrderItemStatus == OrderItemStatus.Approved))
                order.OrderStatus = OrderStatus.Closed;
            else
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Only the order in the 'Assigned to waiter' status can be closed.");
        }

        /// <summary>
        /// Waiter means the order is paid for.
        /// </summary>
        /// <param name="order"></param>
        public void PaidOrder(Order order)
        {
            if (order.OrderStatus == OrderStatus.Closed)
                order.OrderStatus = OrderStatus.Paid;
            else
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Only the order in the 'Closed' status can be paid for.");

        }

        /// <summary>
        /// Manager rejects the order
        /// </summary>
        /// <param name="order">Rejected order</param>
        public void RejectOrder(Order order)
        {
            if (!order.OrderItems.Any(x => x.OrderItemStatus == OrderItemStatus.InProgressInKitchen))
                order.OrderStatus = OrderStatus.Rejected;
            else
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Only the order on which there are no items in the status 'In preparation in the kitchen' can be rejected.");
        }
    }
}