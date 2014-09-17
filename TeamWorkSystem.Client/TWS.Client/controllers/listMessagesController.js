define(["jquery", "modules"], function ($, modules) {
    var url = modules.config.apiURL + "/groups" ;

    function run() {
        modules.view.load("listMessages")
       .then(function () {
            modules.request.get(url)
            .then(function (requestData) {
                $("#messages").loadTemplate(requestData.reverse());
            });
       });
    }

    return {
        run: run
    }
});