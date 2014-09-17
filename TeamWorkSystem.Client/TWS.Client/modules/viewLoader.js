define(["jquery", "appConfig", "Q"], function ($, appConfig, Q) {

    function load(file) {
        var deferred = Q.defer();

        $(appConfig.mainContainer).load(appConfig.templatesPath + file + ".html", function () {
            deferred.resolve();
        });

        return deferred.promise;
    }

    return {
        load: load
    }
});