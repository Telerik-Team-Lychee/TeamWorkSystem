define(["jquery", "modules"], function ($, modules) {
    var url = modules.config.apiURL + "api/TeamWork/ById/";

    function run(id) {
        url = url + id;
        modules.view.load("teamwork")
       .then(function () {
           modules.request.get(url)
           .then(function (requestData) {
               //console.log(requestData);
               $("#single-teamwork").loadTemplate([requestData]);
           }, function () {
               modules.redirect("#/teamwork/" + id);
           });
       });
    }

    return {
        run: run
    }
});