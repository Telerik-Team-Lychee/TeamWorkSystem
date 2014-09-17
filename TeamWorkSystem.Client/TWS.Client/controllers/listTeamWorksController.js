define(["jquery", "modules"], function ($, modules) {
    var url = modules.config.apiURL + "/teamworks/all" ;

    function run() {
        modules.view.load("listTeamworks")
       .then(function () {
            modules.request.get(url)
            .then(function (requestData) {
                $("#teamworks").loadTemplate(requestData.reverse());
            });
       });
    }

    return {
        run: run
    }
});