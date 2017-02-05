(function(app) {
    app.factory("authService", ["$http", "$q", "localStorageService", function($http, $q, localStorageService) {

        var authServiceFactory = {};

        var authentication = {
            isAuth: false,
            userName: ""
        };

        function saveRegistration(registration) {
            logOut();

            return $http.post("/api/account/register", registration)
                .then(function(response) {
                    return response;
                });
        };

        function login(loginData) {
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
            var deferred = $q.defer();

            $http.post("/token", data, { headers: { "Content-Type": "application/x-www-form-urlencoded" } })
                .then(function (response) {
                    localStorageService.set("authorizationData", { token: response.data.access_token, userName: loginData.userName });
                    authentication.isAuth = true;
                    authentication.userName = loginData.userName;

                    deferred.resolve(response);
                })
                .catch(function(err, status) {
                    logOut();
                    deferred.reject(err);
                });

            return deferred.promise;
        };

        function logOut() {
            localStorageService.remove("authorizationData");

            authentication.isAuth = false;
            authentication.userName = "";
        };

        fillAuthData();
        function fillAuthData() {
            var authData = localStorageService.get("authorizationData");
            if (authData) {
                authentication.isAuth = true;
                authentication.userName = authData.userName;
            }
        }

        authServiceFactory.saveRegistration = saveRegistration;
        authServiceFactory.login = login;
        authServiceFactory.logOut = logOut;
        authServiceFactory.fillAuthData = fillAuthData;
        authServiceFactory.authentication = authentication;

        return authServiceFactory;
    }]);
})(angular.module("MicroBlogApp"))