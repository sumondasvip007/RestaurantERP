angular.module('myApp').controller('EditUserController', function ($scope, $window, $http, $location, $routeParams, $filter, dateconfigservice, isundefinedornullservice, globalvalueservice) {
    $scope.LoadInitial = function () {
        $scope.EditUser = {};
        $scope.LoadUserInfo();
    };

    $scope.LoadUserInfo = function()
    {
        $http.get('/Account/LoadUserInfo/' + $routeParams.id)
        .success(function (data) {
            if (data.success) {
                $scope.EditUser = data.result;
            }
            else {
                toastr.error(data.errorMessage);
            }
        }).
        error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    }
    $scope.UpdateUser = function()
    {
        $scope.EditUser.Id = $scope.EditUser.UserId;
        if ($scope.EditUserForm.$valid) {
            $http({
                method: 'POST',
                url: '/Account/UpdateUser',
                data: $scope.EditUser
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $location.path('/UserList');
                }
                else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            })
        }
    }
})