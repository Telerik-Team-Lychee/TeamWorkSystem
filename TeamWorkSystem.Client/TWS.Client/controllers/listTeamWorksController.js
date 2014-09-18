define(["jquery", "modules", "appConfig"], function ($, modules, appConfig) {
    var url = modules.config.apiURL + "teamwork/all";

    function run() {
        modules.view.load("listTeamworks")
       .then(function () {
           modules.request.get(url)
          .then(function (requestData) {
              //console.log(requestData);
              if (requestData.length < 2) {
                  $("#teamworks").loadTemplate([requestData])
              }
              else {
                  $("#teamworks").loadTemplate(requestData)
              }
          });

           modules.request.get(modules.config.apiURL + "teamwork/GetCategories")
          .then(function (requestData) {
              //console.log(requestData);
              if (requestData.length < 2) {
                  $("#categories").loadTemplate([requestData])
              }
              else {
                  $("#categories").loadTemplate(requestData)
              }
          }).then(filterByCategory());
       });
    }

    function addEvents() {
        $("#mainContent").on("click", "#teamworks a", function () {
            event.preventDefault();
            var self = $(this);

            var id = self.attr('id');
            modules.redirect("#/teamwork/" + id);
        });
    }

    function filterByCategory() {
        console.log('filtered');
        var selected = "";
        $("select#categories").on('change', function () {
            $("select option:selected").each(function () {
                var selected = $(this).attr('id');
                modules.request.get(modules.config.apiURL + "teamwork/All")
                .then(function (requestData) {
                    var filtered = [];
                    for (var i = 0; i < requestData.length; i++) {
                        if (requestData[i].Category == selected) {
                            //console.log(requestData[i].Category)
                            filtered.push(requestData[i]);
                        }
                    }

                    if (filtered.length > 0) {
                        $("#teamworks").empty();
                        for (var i = 0; i < filtered.length; i++) {
                            $("#teamworks").loadTemplate([filtered[i]])
                        }
                        filtered = [];
                    } else {
                        $("#teamworks").html('<p>There are no teamworks in this category.</p>');

                    }
                });
            });
        });
    }

    return {
        run: run
    }
});