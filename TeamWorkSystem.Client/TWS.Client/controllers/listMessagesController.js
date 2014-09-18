define(["jquery", "modules", "appConfig"], function ($, modules, appConfig) {
    var url = modules.config.apiURL + "TeamWork/All";

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

       })
       .then(addEvents());
    }

    function addEvents() {
        $("#mainContent").on("click", ".teamwork-box li", function () {
            event.preventDefault();
            var self = $(this);

            var id = self.attr('id');
            modules.redirect("#/messages/" + id);
        });
    }


    return {
        run: run
    }
});