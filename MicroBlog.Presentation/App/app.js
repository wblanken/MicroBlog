(function() {
    var app = angular.module("MicroBlogApp", ["ngRoute", "LocalStorageModule"]);

    app.config(function($routeProvider, $locationProvider) {

        $locationProvider.hashPrefix("");

        $routeProvider.when("/home", {
            controller: "homeController",
            templateUrl: "/app/views/home.html"
        });

        $routeProvider.when("/login", {
            controller: "loginController",
            templateUrl: "/app/views/login.html"
        });

        $routeProvider.when("/signup", {
            controller: "signupController",
            templateUrl: "/app/views/signup.html"
        });

        $routeProvider.otherwise({ redirectTo: "/home" });
    });

    app.config(function($httpProvider) {
        $httpProvider.interceptors.push("authServiceIntercept");
    });

    app.run(["authService", function(authSerivce) {
            authSerivce.fillAuthData();
        }
    ]);
})();