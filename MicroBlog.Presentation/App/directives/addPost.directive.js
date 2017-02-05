(function(app) {
	app.directive("addPost", addPost);

	var controller = function ($rootScope, $scope, $http, authService, postService) {
		var addPt = this;

		$scope.postData = {
		    message : ""
		}

		$scope.message = "";

		addPt.submit = function () {
		    if ($scope.postData.message !== "") {
		        postService.CreatePost($scope.postData)
		            .then(function(response) {
		                $rootScope.$broadcast("postAdded");
		                },
		                function(err) {
		                    $scope.message = err.error_description;
		                });
		    }
		}

		$scope.authentication = authService.authentication;
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