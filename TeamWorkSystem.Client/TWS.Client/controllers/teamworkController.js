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
           }).then(function () {
               addEvents();
           });

           modules.request.get(modules.config.apiURL + "Message/All/" + id)
           .then(function (requestData) {
               // console.log(requestData);
               $("#messages").loadTemplate(requestData);
           });
       });
    }

    function addEvents() {
        $("button.deleteass").on("click", function (event) {
            event.preventDefault();
            var self = $(this);

            var assId = self.attr("id");

            self.parent().parent().hide();
            deleteAss(assId);

        });
    }

    function deleteAss(assId) {

        modules.request.get(modules.config.apiURL + "Assignment/Delete/" + assId);
    }

    return {
        run: run
    }
});