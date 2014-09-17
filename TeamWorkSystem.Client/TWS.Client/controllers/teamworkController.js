define(["jquery", "modules"], function ($, modules) {
    var url = modules.config.apiURL + "api/teamworks/";

    function run(id) {
        url = url + id;
        modules.view.load("teamwork")
       .then(function () {
           modules.request.get(url)
           .then(function (requestData) {
               $("#single-teamwork").loadTemplate(requestData);
           });
       });
    }

    return {
        run: run
    }
});