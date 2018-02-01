namespace OrderManagementSystem.Domain.Order.OrderItem
{
    using Common;
    using Infrastructure.Exception;
    using NHibernate;
    using Infrastructure.Service;

    /// <summary>
    /// Interfejs do zarządzania statusem pozycji zamówienia
    /// </summary>
    public interface IOrderItemStatusService
    {
        /// <summary>
        /// Zatwierdź pozycje zamówienia
        /// </summary>
        /// <param name="item">Pozycja w zamówieniu</param>
        void ApproveOrderItem(OrderItem item);

        /// <summary>
        /// Oznacz pozycję jako 'Przygotowywana przez kuchnie'
        /// </summary>
        /// <param name="item">Pozycja w zamówieniu</param>
        void InProgressOrderItem(OrderItem item);

        /// <summary>
        /// Pozycja gotowa do podania
        /// </summary>
        /// <param name="item">Pozycja w zamówieniu</param>
        void ReadyOrderItem(OrderItem item);

        /// <summary>
        /// Pozycja podana
        /// </summary>
        /// <param name="item">Pozycja w zamówieniu</param>
        void DeliveredOrderItem(OrderItem item);
    }

    /// <summary>
    /// Implementacja interfejsu do zarządzania statusem pozycji zamówienia
    /// </summary>
    public class OrderItemStatusService : BusinessService, IOrderItemStatusService
    {
        public OrderItemStatusService(ISession session) : base(session)
        {
        }

        public void ApproveOrderItem(OrderItem item)
        {
            if(item.OrderItemStatus == OrderItemStatus.New)
                item.OrderItemStatus = OrderItemStatus.Approved;
            else
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Tylko element zamówienia w statusie 'Nowe' może zostać zaakceptowany.");
        }

        public void InProgressOrderItem(OrderItem item)
        {
            if (item.OrderItemStatus == OrderItemStatus.Approved)
                item.OrderItemStatus = OrderItemStatus.InProgressInKitchen;
            else
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Tylko element zamówienia w statusie 'Zaakceptowany' może zostać oznaczony jako 'W trakcie przygotowywania'.");
        }

        public void ReadyOrderItem(OrderItem item)
        {
            if (item.OrderItemStatus == OrderItemStatus.InProgressInKitchen)
                item.OrderItemStatus = OrderItemStatus.Ready;
            else
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Tylko element zamówienia w statusie 'W trakcie realizacji' może zostać oznaczony jako 'Gotowy'.");
        }

        public void DeliveredOrderItem(OrderItem item)
        {
            if (item.OrderItemStatus == OrderItemStatus.Ready)
                item.OrderItemStatus = OrderItemStatus.Delivered;
            else
                throw new BusinessException(BusinessErrorCodes.BusinessRulesViolation, "Tylko element zamówienia w statusie 'Gotowy' może zostać oznaczony jako 'Doręczony'.");
        }
    }
}