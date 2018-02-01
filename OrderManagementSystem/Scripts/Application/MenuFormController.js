function MenuFormController() {
    var self = this;

    self.AddOrderItem = function() {
        var $productRow = $(this).parents("tr:first");
        var quantity = "#" + $productRow.data("product-id") + "-quantity";

        var orderItem = {
            ProductId: $productRow.data("product-id"),
            Quantity: $(quantity).val(),
            OrderId: $("#OrderId").val()
        };

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: Application.Routes.Customer.AddOrderItem,
            data: JSON.stringify(orderItem),
            dataType: "html"
        })
        .always(function (data) {
                $("#myOrder").html(data);
                var orderId = $(data).find("#actualOrderId").prevObject[0].value;
                $("#OrderId").val(orderId);
                $(".quantity").val("");
            });
    };

    self.init = function() {
        $(".addProductToOrder").click(self.AddOrderItem);
    };
}