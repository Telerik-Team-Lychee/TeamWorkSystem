define(["jquery", "modules"], function ($, modules) {
    var userInfo = {},
        url = modules.config.apiURL + "Account/Register";

    function run() {
        modules.view.load("register")
        .then(function (data) {
            addEvents();
        });
    }

    function addEvents() {
        $("form").on("submit", function (event) {
            event.preventDefault();
            var self = $(this);

            $.each(self.serializeArray(), function (i, input) {
                userInfo[input.name] = input.value;
            });

            addMessage();
        });
    }

    function addMessage() {
        var data = "Email=" + userInfo['Email'] + "&Password=" + userInfo['Password'] + "&ConfirmPassword=" + userInfo['ConfirmPassword'];

        modules.request.post(url, data, "application/x-www-form-urlencoded")
        .then(function () {
            modules.redirect("#/login");
        });
    }

    return {
        run: run
    }
});