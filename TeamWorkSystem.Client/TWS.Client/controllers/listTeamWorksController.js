define(["jquery", "modules", "appConfig"], function ($, modules, appConfig) {
    var url = modules.config.apiURL + "api/teamwork/all" ;

    function run() {
        modules.view.load("listTeamworks")
       .then(function () {
           modules.request.get(url)
          .then(function (requestData) {
              if (requestData.length < 2) {
                  $("#teamworks").loadTemplate([requestData])
              }
              else {
                  $("#teamworks").loadTemplate(requestData)
              }
          });

           modules.request.get(modules.config.apiURL + "api/categories/all")
          .then(function (requestData) {
              if (requestData.length < 2) {
                  $("#categories").loadTemplate([requestData])
              }
              else {
                  $("#categories").loadTemplate(requestData)
              }
          });
       })
       .then(addEvents());
    }

    function addEvents() {
        $("#mainContent").on("click", ".teamwork-box li", function () {
            event.preventDefault();
            var self = $(this);

            var id = self.attr('id');
            modules.redirect("#/teamwork/" + id);
        });
    }


    return {
        run: run
    }
});