define(["jquery", "modules"], function ($, modules) {
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
            modules.redirect("#/teamwork/" + assignmentInfo['TeamWorkId']);
        });
    }

    return {
        run: run
    }
});