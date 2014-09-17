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
        modules.request.post(url, JSON.stringify(userInfo))
        .then(function () {
            modules.redirect("#/");
        });
    }

    return {
        run: run
    }
});