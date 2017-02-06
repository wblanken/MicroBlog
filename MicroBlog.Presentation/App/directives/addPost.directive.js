(function (app) {
    app.directive("addPost", addPost);

    var controller = function ($rootScope, $scope, $http, authService, postService) {
        var addPt = this;

        $scope.authentication = authService.authentication;
        $scope.message = "";

        setPostData();
        function setPostData() {
            $scope.postData = {
                message: "",
                userName: $scope.authentication.userName
            }
        }

        addPt.submit = function () {
            if ($scope.postData.message !== "") {
                postService.createPost($scope.postData)
		            .then(function (response) {
		                $rootScope.$broadcast("postAdded");
		                setPostData();
		                $scope.addPostForm.$setPristine();
		            },
		                function (err) {
		                    $scope.message = err.error_description;
		                });
            }
        }
    }

    function addPost() {
        return {
            restrict: "E",
            templateUrl: "/App/views/addPost.html",
            controller: controller,
            controllerAs: "addPt"
        }
    }
})(angular.module("MicroBlogApp"));