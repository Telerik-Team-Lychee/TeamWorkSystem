define(["requester", "storager", "viewLoader", "appConfig"], function (requester, storager, viewLoader, appConfig) {
    // Loads all modules that we are gonna need.
    return {
        request: requester,
        storage: storager,
        view: viewLoader,
        config: appConfig,
        redirect: function (newLocation) {
            window.location.hash = newLocation;
        },
    }
});