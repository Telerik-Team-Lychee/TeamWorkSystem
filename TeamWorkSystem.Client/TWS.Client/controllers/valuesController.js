define(["jquery", "modules"], function ($, modules) {
    var url = modules.config.apiURL + "values";

    function run() {
       
        modules.request.get(url)
        .then(function (requestData) {
            console.log(requestData);
        });
    }

    return {
        run: run
    }
});