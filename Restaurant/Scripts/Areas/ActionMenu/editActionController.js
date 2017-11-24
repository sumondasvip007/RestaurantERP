


angular.module('myApp').controller('editActionController', function ($scope, $http, $location, $routeParams) {

    $scope.initialize = function () {
        //var ids = $routeParams.id;
        $scope.GetActionById();
        $scope.getModules();
    }
    $scope.GetActionById = function () {
        var id = $routeParams.id;

        $http.get('/MenuAction/GetActionById/' + id)
        .success(function (data) {
            if (data.success) {
                $scope.actionData = data.result;
                console.log($scope.actionData);
            }
            else {
                toastr.error(data.errorMessage);
            }
        }).
        error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    }

    $scope.updateAction = function () {
        console.log($scope.actionData);
        $http({
            method: 'POST',
            url: '/MenuAction/UpdateAction',
            data: $scope.actionData
        }).success(function(data) {
            if (data.success) {
                toastr.success(data.successMessage);
                $location.path('/viewAction');
            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function(XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    }

    $scope.getModules = function () {

        $http.get('/MenuAction/GetModules')
        .success(function (data) {
            if (data.success) {
                $scope.modules = data.result;
            }
            else {
                toastr.error(data.errorMessage);
            }
        }).
        error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    }

});
