function RestaurantWorkerController() {
    var self = this;

    self.MarkAsReadyOrderItem = function(e) {
        var $button = $(e.target);
        var $orderItemRow = $button.parents("tr:first");
        var orderItemId = $orderItemRow.attr("data-order-item-id");

        e.preventDefault();

        $.get('/RestaurantWorker/MarkAsReadyOrderItem?orderItemId=' + orderItemId, function (data) {
            $('#ajax-' + orderItemId).html(data);
        });
    };

    self.MarkAsDeliveredOrderItem = function (e) {
        var $button = $(e.target);
        var $orderItemRow = $button.parents("tr:first");
        var orderItemId = $orderItemRow.attr("data-order-item-id");

        e.preventDefault();

        $.get('/RestaurantWorker/MarkAsDeliveredOrderItem?orderItemId=' + orderItemId, function (data) {
            $('#ajax-' + orderItemId).html(data);
        });
    };

    self.init = function() {
        $(".readyOrderItem").click(self.MarkAsReadyOrderItem);
        $(".deliveredOrderItem").click(self.MarkAsDeliveredOrderItem);
    };
}