namespace OrderManagementSystem.Domain.Order
{
    using System.Linq;
    using NHibernate;
    using Common;
    using OrderItem;
    using Infrastructure.Exception;
    using Infrastructure.Service;

    /// <summary>
    /// Interfejs do zarządzania statusem zamówienia
    /// </summary>
    public interface IOrderStatusService
    {
        /// <summary>
        /// Klient zamyka zamówienie (chce zapłacić, nie będzie nic więcej zamawiać)
        /// </summary>
        /// <param name="order"></param>
        void CloseOrder(Order order);

        /// <summary>
        /// Kelner przypisuje do siebie zamówienie
        /// </summary>
        /// <param name="order"></param>
        void AssignToWaiter(Order order);

        /// <summary>
        /// Kelner oznacza zamówienie jako opłacone.
        /// </summary>
        /// <param name="order"></param>
        void PaidOrder(Order order);

        /// <summary>
        /// Klient odrzuca zamówienie
        /// </summary>
        /// <param name="order">Odrzucane zamówienie</param>
        void RejectOrder(Order order);
    }

    /// <summary>
    /// Implementacja interfejsu do zarządzania statusem zamówienia
    /// </summary>
    public class OrderStatusService : BusinessService, IOrderStatusService
    {
        public OrderStatusService(ISession session) : base(session)
        {
        }

        /// <summary>
        /// Kelner przypisuje do siebie zamówienie
        /// </summary>
        /// <param name="order"></param>
        public void AssignToWaiter(Order order)
        {
            if (order.OrderStatus == OrderStatus.Open)
                order.OrderStatus = OrderStatus.AssignedToWaiter;
            else
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Tylko zamówienie w statusie 'Otwarte' może zostać przypisane do kelnera.");
        }

        /// <summary>
        /// Klient zamyka zamówienie (chce zapłacić, nie będzie nic więcej zamawiać)
        /// </summary>
        /// <param name="order"></param>
        public void CloseOrder(Order order)
        {
            if (order.OrderStatus == OrderStatus.AssignedToWaiter && !order.OrderItems.Any(x => x.OrderItemStatus == OrderItemStatus.Approved))
                order.OrderStatus = OrderStatus.Closed;
            else
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Tylko zamówienie w statusie 'Przypisane do kelnera' może zostać zamknięte.");
        }

        /// <summary>
        /// Kelner oznacza zamówienie jako opłacone.
        /// </summary>
        /// <param name="order"></param>
        public void PaidOrder(Order order)
        {
            if (order.OrderStatus == OrderStatus.Closed)
                order.OrderStatus = OrderStatus.Paid;
            else
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Tylko zamówienie w statusie 'Zamknięte' może zostać opłacone.");

        }

        /// <summary>
        /// Manager odrzuca zamówienie
        /// </summary>
        /// <param name="order">Odrzucane zamówienie</param>
        public void RejectOrder(Order order)
        {
            if (!order.OrderItems.Any(x => x.OrderItemStatus == OrderItemStatus.InProgressInKitchen))
                order.OrderStatus = OrderStatus.Rejected;
            else
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Tylko zamówienie, na którym nie ma elementów w statusie 'W przygotowaniu w kuchni' może zotać odrzucone.");
        }
    }
}