define(["jquery", "modules"], function ($, modules) {
    var teamworkInfo = {};
    var url = modules.config.apiURL + "Message/Create";

    function run() {
        modules.view.load("newMessage")

        modules.request.get(modules.config.apiURL + "TeamWork/All")
        .then(function (requestData) {
            if (requestData.length < 2) {
                $("#Teamworks").loadTemplate([requestData])
            }
            else {
                $("#Teamworks").loadTemplate(requestData)
            }
        })
            .then(addEvents())
    }

    function addEvents() {
        $("#mainContent").on("click", "#create-message", function (event) {
            event.preventDefault();
            var self = $(this);

            $.each(self.serializeArray(), function (i, input) {
                messageInfo[input.name] = input.value;
            });

            addmessage();
        });
    }

    function addmessage() {
        var data = "Name=" + messageInfo['Name'] + "&Description=" + messageInfo['Description'] + "&GitHubLink=" + messageInfo['GitHubLink']
        + "&Category" + messageInfo['Category'] + "&EndDate=" + messageInfo['EndDate'];

        modules.request.post(url, data, "application/x-www-form-urlencoded")
        .then(function () {
            modules.redirect("#/messages/");
        });
    }

    return {
        run: run
    }
});