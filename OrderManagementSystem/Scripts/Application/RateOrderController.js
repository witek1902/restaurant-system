function RateOrderController() {
    var self = this;

    self.init = function() {
        $('#input-id').on('rating.change', function (event, value, caption) {
            console.log(value);
            console.log(caption);
            $("#RateStars").val(value);
        });
    }
}