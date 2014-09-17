define(["jquery", "modules", "appConfig"], function ($, modules, appConfig) {
    var url = modules.config.apiURL + "api/teamwork/" ;

    function run() {
        url = url + "all";
        modules.view.load("listTeamworks")
       .then(function () {
           modules.request.get(url)
           .then(function (requestData) {
               $("#teamworks").loadTemplate([requestData.reverse()])
           })
               .then(addEvents());
           });
    }

    function addEvents() {
        $("#mainContent").on("click", ".teamwork-box li", function () {
            event.preventDefault();
            var self = $(this);

            var id = self.attr('id');
            //console.log(id);
            //require([appConfig.controllersPath + "teamworkController"], function (file) {
            //    file.run(id);
            //})
            modules.redirect("#/teamwork/" + id);
        });
    }


    return {
        run: run
    }
});