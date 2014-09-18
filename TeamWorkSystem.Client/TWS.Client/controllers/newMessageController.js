define(["jquery", "modules"], function ($, modules) {
    var messageInfo = {};
    var url = modules.config.apiURL + "Message/Create";

    function run() {
        modules.view.load("newMessage")
        modules.request.get(modules.config.apiURL + "TeamWork/All")
        .then(function (requestData) {
            if (requestData.length < 2) {
                $("#TeamWorkId").loadTemplate([requestData])
            }
            else {
                $("#TeamWorkId").loadTemplate(requestData)
            }

            addEvents();
        })
    }

    function addEvents() {
        $("form").on("submit", function (event) {
            event.preventDefault();
            var self = $(this);

            $.each(self.serializeArray(), function (i, input) {
                messageInfo[input.name] = input.value;
            });

            addmessage();
        });
    }

    function addmessage() {
        var data =
            "Text=" + messageInfo['messageText'] +
            "&TeamWorkId=" + messageInfo['TeamWorkId'] +
            "&SentBy = " + modules.storage.get("user");

        modules.request.post(url, data, "application/x-www-form-urlencoded")
        .then(function () {
            modules.redirect("#/teamwork/" + messageInfo['TeamWorkId']);
        });
    }

    return {
        run: run
    }
});