define(["jquery", "modules"], function ($, modules) {
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
            var id = requestData.Id;
            modules.redirect("#/teamwork/" + id);
        });
    }

    return {
        run: run
    }
});