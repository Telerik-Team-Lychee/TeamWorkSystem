(function () {
    require.config({
        paths: {
            // Libs
            "jquery": "scripts/libs/jquery",
            "mustache": "scripts/libs/mustache",
            "Q": "scripts/libs/q",
            "underscore": "scripts/libs/underscore",
            "sammy": "scripts/libs/sammy",
            "pubnub": "scripts/libs/pubnub.min",

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

                if(JSON.parse(localStorage.getItem("token")) !== null) {
                    $("a.logIn").hide();
                    $("a.register").hide();
                    $("a.logout").show();
                    $("a.home").show();
                } else {
                    $("a.logIn").show();
                    $("a.register").show();
                    $("a.logout").hide();
                    $("a.home").hide();
                    window.location.hash = "#/login";
                }
                

                require([appConfig.controllersPath + "listTeamWorksController"], function (file) {
                    file.run();
                });
            });

            this.get("#/logout", function () {
                localStorage.removeItem("token");
                $("a.logIn").show();
                $("a.register").show();
                $("a.logout").hide();
                $("a.home").show();
                window.location.hash = "#/";
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

            this.get("#/teamwork/:id", function () {
                var teamworkId = this.params['id'];
                require([appConfig.controllersPath + "teamworkController"], function (file) {
                    file.run(teamworkId);
                });
            });

            this.get("#/newteamwork", function () {
                require([appConfig.controllersPath + "newTeamworkController"], function (file) {
                    file.run();
                });
            });

            this.get("#/messages", function () {
                require([appConfig.controllersPath + "listMessagesController"], function (file) {
                    file.run();
                });
            });

            this.get("#/messages/:id", function () {
                var messageId = this.params['id'];
                require([appConfig.controllersPath + "messageController"], function (file) {
                    file.run(messageId);
                });
            });

            this.get("#/deleteAssingment/:id:id2", function () {
                var assId = this.params['id'];
                require([appConfig.controllersPath + "deleteAssController"], function (file) {
                    file.run(assId);
                });
            });

            this.get("#/newmessage", function () {
                require([appConfig.controllersPath + "newMessageController"], function (file) {
                    file.run();
                });
            });

            this.get("#/newassignment", function () {
                require([appConfig.controllersPath + "newAssignmentController"], function (file) {
                    file.run();
                });
            });
        });

        app.run("#/");
    });
}());