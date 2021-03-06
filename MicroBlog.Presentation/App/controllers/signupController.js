﻿(function(app) {
    app.controller("signupController", signupController);
    signupController.$inject = ["$scope", "$location", "$timeout", "authService"];

    function signupController($scope, $location, $timeout, authService) {
        $scope.savedSuccessfully = false;
        $scope.message = "";

        $scope.registration = {
            userName: "",
            password: "",
            confirmPassword: ""
        };

        $scope.signUp = function() {
            authService.saveRegistration($scope.registration)
                .then(function (response) {
                        $scope.savedSuccessfully = true;
                        $scope
                            .message =
                            "New user registered successfully, you will be redirected to the login page in 2 seconds";

                        startTimer();
                    },
                    function (response) {
                        var errors = [];
                        for (var key in response.data.modelState) {
                            for (var i = 0; i < response.data.modelState[key].length; i++) {
                                errors.push(response.data.modelState[key][i]);
                            }
                        }
                        $scope.message = "Failed to register user! " + errors.join(" ");
                    });
        };

        function startTimer() {
            var timer = $timeout(function() {
                    $timeout.cancel(timer);
                    $location.path('/login');
                }, 2000);
        }
    };
})(angular.module("MicroBlogApp"));