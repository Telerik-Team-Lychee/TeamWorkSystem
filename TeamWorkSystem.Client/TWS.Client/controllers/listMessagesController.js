define(["jquery", "modules", "appConfig"], function ($, modules, appConfig) {
    var url = modules.config.apiURL + "Message/All";
    console.log(modules.config.apiURL);
    function run() {
        modules.view.load("listMessages")
       .then(function () {
           modules.request.get(url)
          .then(function (requestData) {
              if (requestData.length < 2) {
                  $("#messages").loadTemplate([requestData])
              }
              else {
                  $("#messages").loadTemplate(requestData)
              }
          });

           modules.request.get(modules.config.apiURL + "TeamWork/All")
          .then(function (requestData) {
              if (requestData.length < 2) {
                  $("#teamworks").loadTemplate([requestData])
              }
              else {
                  $("#teamworks").loadTemplate(requestData)
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