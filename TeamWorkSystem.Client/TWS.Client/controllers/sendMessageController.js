define(["jquery", "modules"], function ($, modules) {
    var messageInfo = {},
        url = modules.config.apiURL;

    function run() {
        modules.view.load("sendMessage")
        .then(function (data) {
            addEvents();
        });
    }

    function addEvents() {
        $("form").on("submit", function (event) {
            event.preventDefault();
            var self = $(this);

            $.each(self.serializeArray(), function (i, input) {
                messageInfo[input.name] = input.value;
            });

            addMessage();
        });
    }

    function addMessage() {
        modules.request.post(url, JSON.stringify(messageInfo))
        .then(function () {
            modules.redirect("#/");
        });
    }

    return {
        run: run
    }
});