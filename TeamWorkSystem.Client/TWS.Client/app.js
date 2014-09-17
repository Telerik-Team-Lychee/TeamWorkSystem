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

    require(["sammy", "appConfig", "templater", "modules"], function (sammy, appConfig, modules) {
        var app = sammy("#mainContent", function () {

            this.get("#/", function () {
                //if (modules.storager.get("user") !== null) {
                    
                //}
                
                require([appConfig.controllersPath + "listTeamWorksController"], function (file) {
                    file.run();
                });
            });

            this.get("#/register", function () {
                require([appConfig.controllersPath + "registerController"], function (file) {
                    file.run();
                });
            });

            this.get("#/login", function () {
                require([appConfig.controllersPath + "loginController"], function (file) {
                    file.run();
                });
            });

            this.get("#/teamworks", function () {
                require([appConfig.controllersPath + "listTeamWorksController"], function (file) {
                    file.run();
                });
            });
        });

        app.run("#/");
    });
}());