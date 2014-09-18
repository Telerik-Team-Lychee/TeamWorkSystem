define(["jquery", "modules"], function ($, modules) {
    var url = modules.config.apiURL + "TeamWork/ById/";

    function run(id) {
        modules.view.load("teamwork")
       .then(function () {
           modules.request.get(url + id)
           .then(function (requestData) {
               $("#single-teamwork").loadTemplate([requestData]);
           }, function () {
               modules.redirect("#/teamwork/" + id);
           });

           modules.request.get(modules.config.apiURL + "assignment/ByTeamwork/" + id)
           .then(function (requestData) {
               console.log(requestData);
               $("#assignments").loadTemplate(requestData);
           }, function () {
               modules.redirect("#/teamwork/" + id);
           });

       });
    }

    return {
        run: run
    }
});