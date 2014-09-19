define(["jquery", "modules", "pubnub"], function ($, modules) {
    var teamworkInfo = {};
    var url = modules.config.apiURL + "Teamwork/Create";
    var id = "";
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
        });
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
            id = requestData.Id;
            var name = subscribe(requestData);
            return name;

        }).then(function (name) {
            publish(name);
            modules.redirect("#/teamwork/" + id);
        });
    }

    function subscribe(requestData) {
        var publishKey = 'pub-c-c4395ede-b7ee-4ca2-b9d3-324f1c2007ff';
        var subscribeKey = 'sub-c-248e8f78-3ff2-11e4-b33e-02ee2ddab7fe';

        var pubnub = PUBNUB.init({
            publish_key: publishKey,
            subscribe_key: subscribeKey,
        });

        pubnub.subscribe({
            channel: requestData.Name,
            message: function (message) {
                $('#notifications').html('<p>' + message + '</p>');
            }
        });

        return requestData.Name;
    }

    function publish(name) {      
        var publishKey = 'pub-c-c4395ede-b7ee-4ca2-b9d3-324f1c2007ff';
        var subscribeKey = 'sub-c-248e8f78-3ff2-11e4-b33e-02ee2ddab7fe';

        var pubnub = PUBNUB.init({
            publish_key: publishKey,
            subscribe_key: subscribeKey,
        });

        pubnub.publish({
            channel: name,
            message: "Teamwork " + name + " created."
        });
    }

    return {
        run: run
    }
});