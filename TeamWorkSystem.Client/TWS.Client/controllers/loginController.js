define(["jquery", "modules"], function ($, modules) {
    var userInfo = {},
        url = modules.config.tokenURL;

    function run() {
        modules.view.load("login")
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
        var data = "grant_type=password&username=" + userInfo['username'] + "&password=" + userInfo['password'];

        modules.request.post(url, data, "application/x-www-form-urlencoded") //"application/x-www-form-urlencoded"
        .then(function (requestData) {
            modules.storage.set("token", requestData.access_token);
            modules.storage.set("user", requestData.userName);
            modules.redirect("#/");
        });
    }

    return {
        run: run
    }
});