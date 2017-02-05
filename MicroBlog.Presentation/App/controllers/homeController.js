(function(app) {
    app.controller("homeController", homeController);
    homeController.$inject = ["$scope"];

    function homeController($scope) {};
})(angular.module("MicroBlogApp"));