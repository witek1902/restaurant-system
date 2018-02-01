function ApplicationController() {
    var self = this;

    self.init = function () {
        $(document).delegate('*[data-toggle="lightbox"]', 'click', function (event) {
            event.preventDefault();
            $(this).ekkoLightbox();
        });

        $("#input-id").rating();

        $.validator.methods.number = function (n, t) {
            return true;
        }
    }
}

var Application;

$(function () {
    Application = new ApplicationController();
    Application.init();
});