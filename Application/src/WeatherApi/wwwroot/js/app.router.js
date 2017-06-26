(function () {
    "use strict";

    angular
        .module('weatherApp')
        .config(config)
        
    function config($routeProvider, $locationProvider) {

        $routeProvider
            .when("/", {
                templateUrl: "./client/home/home.view.html",
                controller: "homeController",
                controllerAs: 'vm'
            })

        $locationProvider.html5Mode(true);
    
    }

})();