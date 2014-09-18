define(["jquery", "modules", "appConfig"], function ($, modules, appConfig) {
    var url = modules.config.apiURL + "teamwork/all" ;

    function run() {
        modules.view.load("listTeamworks")
       .then(function () {
           modules.request.get(url)
          .then(function (requestData) {
              console.log(requestData);
              if (requestData.length < 2) {
                  $("#teamworks").loadTemplate([requestData])
              }
              else {
                  $("#teamworks").loadTemplate(requestData)
              }
          });

           modules.request.get(modules.config.apiURL + "teamwork/GetCategories")
          .then(function (requestData) {
              console.log(requestData);
              if (requestData.length < 2) {
                  $("#categories").loadTemplate([requestData])
              }
              else {
                  $("#categories").loadTemplate(requestData)
              }
          });
       })
       //.then(addEvents());
    }

    function addEvents() {
        $("#mainContent").on("click", "#teamworks a", function () {
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