define(["jquery", "modules", "pubnub"], function ($, modules) {
    var teamworkInfo = {};
    var url = modules.config.apiURL + "Teamwork/Create";

    function run() {
        modules.view.load("newTeamwork")
        modules.request.get(modules.config.apiURL + "TeamWork/GetCategories")
        .then(function (requestData) {
        if (requestData.length < 2) {
                $("#CategoriesSelect").loadTemplate([requestData])
        }
        else {
                $("#CategoriesSelect").loadTemplate(requestData)
        }

            addEvents();
        }).then(subscribe('teamwork1', "Teamwork teamwork1 created."));
    }

    function addEvents() {
        $("form").on("submit", function (event) {
            event.preventDefault();
            var self = $(this);

            $.each(self.serializeArray(), function (i, input) {
                teamworkInfo[input.name] = input.value;
            });

            addmessage();
        });
    }

    function addmessage() {
        var data =
            "Name=" + teamworkInfo['Name'] +
            "&Description=" + teamworkInfo['Description'] +
            "&GitHubLink=" + teamworkInfo['GitHubLink'] +
            "&Category=" + teamworkInfo['Category'] +
            "&EndDate=" + teamworkInfo['EndDate'];
            
        modules.request.post(url, data, "application/x-www-form-urlencoded")
        .then(function (requestData) {
            var id = requestData.Id;
            modules.redirect("#/teamwork/" + id);
        });
    }

    function subscribe(channel, message) {
        $('#mainContent').on('click', '#create-teamwork', function () {
            var publishKey = 'pub-c-914d69be-0ad7-4b88-8a4b-fc9543e9fa2d';
            var subscribeKey = 'sub-c-718ede88-3f0f-11e4-8c81-02ee2ddab7fe';

            var pubnub = PUBNUB.init({
                publish_key: publishKey,
                subscribe_key: subscribeKey,
            });

            pubnub.subscribe({
                channel: channel,
                message: function (message) {
                    $('#notifications').text(message);
                }
            });
            console.log('subscribed');
            console.log(teamworkInfo['Name']);
            pubnub.publish({
                channel: channel,
                message: message
            });

            //pubnub.bind('click', pubnub.$('create-teamwork'), function (e) {
            //    pubnub.publish({
            //        channel: channel,
            //        message: message //"Teamwork " + channel + " created."
            //    });
            //});
        })
   }

    return {
        run: run
    }
});