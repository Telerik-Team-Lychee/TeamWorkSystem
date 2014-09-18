define(["jquery", "modules"], function ($, modules) {
    var url = modules.config.apiURL + "Message/All?teamWorkId={teamWorkId}";

    function run(id) {
        modules.view.load("message")
       .then(function () {
           modules.request.get(url + id)
           .then(function (requestData) {
               $("#single-message").loadTemplate([requestData]);
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