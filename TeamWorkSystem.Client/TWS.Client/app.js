(function () {
    require.config({
        paths: {
            // Libs
            "jquery": "scripts/libs/jquery",
            "mustache": "scripts/libs/mustache",
            "Q": "scripts/libs/q",
            "underscore": "scripts/libs/underscore",
            "sammy": "scripts/libs/sammy",

            // Modules
            "router": "modules/router",
            "requester": "modules/requester",
            "storager": "modules/storager",
            "viewLoader": "modules/viewLoader",
            "templater": "modules/templater",

            // App
            "modules": "scripts/moduleLoader",
            "appConfig": "scripts/appConfig",
        }
    });

    require(["sammy", "appConfig", "templater"], function (sammy, appConfig) {
        var app = sammy("#mainContent", function () {

            this.get("#/", function () {
                require([appConfig.controllersPath + "listMessagesController"], function (file) {
                    file.run();
                });
            });

            this.get("#/send", function () {
                require([appConfig.controllersPath + "sendMessageController"], function (file) {
                    file.run();
                });
            });

        });

        app.run("#/");
    });
}());