(function(app) {
    app.factory("postService", ["$http", function($http) {
        var postService = {};
        postService.getRecentPosts = function() {
            return $http.get("/api/posts/recent");
        }

        postService.CreatePost = function(postData) {
            return $http.post("/api/posts/", postData);
        }
        return postService;
    }]);
})(angular.module("MicroBlogApp"));