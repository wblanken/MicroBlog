﻿(function(app) {
    app.factory("authServiceIntercept", ["$q", "$location", "localStorageService", function($q, $location, localStorageService) {
            var authServiceInterceptFactory = {};

            function request(config) {

                config.headers = config.headers || {};

                var authData = localStorageService.get("authorizationData");
                if (authData) {
                    config.headers.Authorization = "Bearer " + authData.token;
                }

                return config;
            }

            function responseError(rejection) {
                if (rejection.status === 401) {
                    $location.path("/login");
                }
                return $q.reject(rejection);
            }

            authServiceInterceptFactory.request = request;
            authServiceInterceptFactory.responseError = responseError;

            return authServiceInterceptFactory;
        }
    ]);
})(angular.module("MicroBlogApp"));