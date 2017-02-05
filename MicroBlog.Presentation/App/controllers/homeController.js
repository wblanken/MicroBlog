(function(app) {
    app.controller("homeController", homeController);
    homeController.$inject = ["$rootScope", "$scope", "postService"];

    function homeController($rootScope, $scope, postService) {
        getRecentPosts();
        function getRecentPosts() {
            postService.getRecentPosts().then(function(posts) {
                $scope.posts = posts.data;
                console.log($scope.posts);
            });
        }

        $rootScope.$on("sampleAdded"), function() {
            getRecentPosts();
        }
    };
})(angular.module("MicroBlogApp"));