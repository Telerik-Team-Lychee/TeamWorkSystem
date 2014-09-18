define(["jquery", "modules"], function ($, modules) {
    var url = modules.config.apiURL + "TeamWork/ById/";

    function run(id) {
        modules.view.load("teamwork")
       .then(function () {
           modules.request.get(url + id)
           .then(function (requestData) {
               $("#single-teamwork").loadTemplate([requestData]);
           });

           modules.request.get(modules.config.apiURL + "Assignment/ByTeamwork/" + id)
           .then(function (requestData) {
               $("#assignments").loadTemplate(requestData);
           });

           modules.request.get(modules.config.apiURL + "Message/All/" + id)
           .then(function (requestData) {
              // console.log(requestData);
              $("#messages").loadTemplate(requestData);
           });

       });
    }

    return {
        run: run
    }
});