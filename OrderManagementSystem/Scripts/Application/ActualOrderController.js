function ActualOrderController() {
    var self = this;
    
    self.DeleteOrderItem = function () {
        var $orderItem = $(this).parents("tr:first");
        var orderItemId = $orderItem.data("order-item-id");

        $.get('/Customer/DeleteOrderItem?orderItemId=' + orderItemId, function (data) {
            $('#myOrder').html(data);
            var orderId = $(data).find("#actualOrderId").prevObject[0].value;
            $("#OrderId").val(orderId);
        });
    }

    self.ChangeQuantityOrderItem = function () {

        var $orderItem = $(this).parents("tr:first");
        var orderItemId = $orderItem.data("order-item-id");
        var quantity = $("#" + orderItemId + "-quantity").val();

        if (quantity != null && quantity != 0) {
            var orderItem = {
                OrderItemId: orderItemId,
                Quantity: quantity
            };

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: Application.Routes.Customer.ChangeQuantityOrderItem,
                data: JSON.stringify(orderItem),
                dataType: "html"
            })
            .always(function (data) {
                $("#myOrder").html(data);
                var orderId = $(data).find("#actualOrderId").prevObject[0].value;
                $("#OrderId").val(orderId);
            });
        }
    }

    self.init = function () {
        $(".deleteOrderItem").click(self.DeleteOrderItem);
        $(".changeQuantityOrderItem").change(self.ChangeQuantityOrderItem);
    };
}