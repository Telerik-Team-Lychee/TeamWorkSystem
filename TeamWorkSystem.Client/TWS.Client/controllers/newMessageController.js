define(["jquery", "modules", "pubnub"], function ($, modules) {
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
            var channel = subscribe(messageInfo);
            return channel;

        }).then(function (channel) {
            publish(channel);
            modules.redirect("#/teamwork/" + messageInfo['TeamWorkId']);
        });
    }


    function subscribe(messageInfo) {
        var publishKey = 'pub-c-c4395ede-b7ee-4ca2-b9d3-324f1c2007ff';
        var subscribeKey = 'sub-c-248e8f78-3ff2-11e4-b33e-02ee2ddab7fe';

        var pubnub = PUBNUB.init({
            publish_key: publishKey,
            subscribe_key: subscribeKey,
        });

        pubnub.subscribe({
            channel: messageInfo['TeamWorkId'],
            message: function (message) {
                $('#notifications').html('<p>' + message + '</p>');
            }
        });

        return messageInfo['TeamWorkId'];
    }

    function publish(channel) {
        var publishKey = 'pub-c-c4395ede-b7ee-4ca2-b9d3-324f1c2007ff';
        var subscribeKey = 'sub-c-248e8f78-3ff2-11e4-b33e-02ee2ddab7fe';

        var pubnub = PUBNUB.init({
            publish_key: publishKey,
            subscribe_key: subscribeKey,
        });

        pubnub.publish({
            channel: channel,
            message: "New message added."
        });
    }

    return {
        run: run
    }
});