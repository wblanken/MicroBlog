(function(app) {
    app.factory("postService", ["$http", function($http) {
        var postService = {};

        var getRecentPosts = function() {
            return $http.get("/api/posts/recent");
        }

        var createPost = function(postData) {
            return $http.post("/api/posts/create", postData);
        }

        var updatePost = function(updateData) {
            return $http.post("/api/posts/update", updateData);
        }

        var deletePost = function(id) {
            return $http.delete("/api/posts/deletePost/" + id);
        }

        postService.getRecentPosts = getRecentPosts;
        postService.createPost = createPost;
        postService.updatePost = updatePost;
        postService.deletePost = deletePost;

        return postService;
    }]);
})(angular.module("MicroBlogApp"));