function OrderFormController() {
    var self = this;

    self.renderProducts = function() {
        var selectedId = $(this).val();

        if(selectedId != "")
            $.get('/Customer/RenderProductsFromMenu?menuId=' + selectedId, function(data) {
                $('#productsInMenu').html(data);
                $('#productsInMenu').fadeIn('fast');
            });
    };

    self.DeleteOrder = function() {
        $.get('/Customer/DeleteOrder?orderId=' + $("#OrderId").val(), function() {
            window.location ='/Customer/Index';
        });
    };

    self.init = function() {
        $("#menuDropDown").change(self.renderProducts);
        $("#deleteOrder").click(self.DeleteOrder);

        var orderId = $("#OrderId").val();

        if (orderId != null) {
            $.get('/Customer/GetActualOrder?orderId=' + orderId, function (data) {
                $('#myOrder').html(data);
            });
        }
    };
}