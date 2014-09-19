define(["jquery", "modules", "pubnub"], function ($, modules) {
    var assignmentInfo = {};
    var url = modules.config.apiURL + "Assignment/Create/";

    function run() {
        modules.view.load("newAssignment")
        modules.request.get(modules.config.apiURL + "TeamWork/All")
        .then(function (requestData) {
            if (requestData.length < 2) {
                $("#TeamWorkId").loadTemplate([requestData])
            }
            else {
                $("#TeamWorkId").loadTemplate(requestData)
            }

            addEvents();
        });
    }

    function addEvents() {
        $("form").on("submit", function (event) {
            event.preventDefault();
            var self = $(this);

            $.each(self.serializeArray(), function (i, input) {
                assignmentInfo[input.name] = input.value;
            });

            addassignment();
        });
    }

    function addassignment() {
        var data =
            "Name=" + assignmentInfo['Name'] +
            "&Description=" + assignmentInfo['assignmentDescription'] +
            "&Priority= 10";// + assignmentInfo['Priority'];
            //"&TeamWorkId=" + assignmentInfo['TeamWorkId'] +
            //"&Status=" + assignmentInfo['AssignmentStatus'];

        modules.request.post(url + assignmentInfo['TeamWorkId'], data, "application/x-www-form-urlencoded")
        .then(function () {            
            var channel = subscribe(assignmentInfo);
            return channel;
            
        }).then(function (channel) {
            publish(channel);
            modules.redirect("#/teamwork/" + assignmentInfo['TeamWorkId']);
        });
    }

    function subscribe(assignmentInfo) {
        var publishKey = 'pub-c-c4395ede-b7ee-4ca2-b9d3-324f1c2007ff';
        var subscribeKey = 'sub-c-248e8f78-3ff2-11e4-b33e-02ee2ddab7fe';

        var pubnub = PUBNUB.init({
            publish_key: publishKey,
            subscribe_key: subscribeKey,
        });

        pubnub.subscribe({
            channel: assignmentInfo['TeamWorkId'],
            message: function (message) {
                $('#notifications').html('<p>' + message + '</p>');
            }
        });

        return assignmentInfo['TeamWorkId'];
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
            message: "New task created."
        });
    }

    return {
        run: run
    }
});