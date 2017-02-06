(function(app) {
    app.controller("homeController", homeController);
    homeController.$inject = ["$scope", "postService", "authService"];

    function homeController($scope, postService, authService) {

        getPosts();
        function getPosts() {
            postService.getRecentPosts().then(function(posts) {
                $scope.posts = posts.data;
                console.log($scope.posts);
            });
        }

        $scope.removePost = function(id) {
            postService.deletePost(id).then(getPosts).catch(function(err) {
                console.log("Delete failed for post: " + id + "\n" + err.message);
            });
        }

        $scope.$on("postAdded", getPosts);
        $scope.authentication = authService.authentication;
    };
})(angular.module("MicroBlogApp"));