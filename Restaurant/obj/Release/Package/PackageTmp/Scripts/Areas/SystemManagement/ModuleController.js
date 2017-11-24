angular.module('myApp').controller('ModuleController', function ($scope, $window, $http, $location, $routeParams, $filter, dateconfigservice, isundefinedornullservice, globalvalueservice) {
    $scope.loadNewModule= function() {
        $scope.module = {};
    }
    $scope.GetAllModule = function() {
        $scope.rowCollection = [];
        $http.get('/SystemManagement/Module/GetAllModule/')
            .success(function(data) {
                if (data.success) {
                    $scope.rowCollection = data.result;
                    $scope.displayCollection = [].concat($scope.rowCollection);
                } else {
                    toastr.error(data.errorMessage);
                }

            })
            .error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };
    $scope.AddNewModule = function() {
        if ($scope.AddModuleform.$valid) {
            $http({
                method: 'POST',
                url: '/SystemManagement/Module/AddNewModule',
                data: $scope.module
            }).success(function(data) {
                if (data.success) {
                    toastr.success(data.message);
                    $scope.module = {};

                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
    };
    $scope.GetModuleById = function() {
        $http.get('/SystemManagement/Module/GetModuleById/' + $routeParams.id)
            .success(function(data) {
                if (data.success) {
                    $scope.editModule = data.result;
                } else {
                    toastr.error(data.errorMessage);
                }
            })
            .error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };

    $scope.UpdateModule = function () {
        if ($scope.EditModuleform.$valid) {
            $http({
                method: 'POST',
                url: '/SystemManagement/Module/UpdateModule',
                data: $scope.editModule
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.message);
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
    };
});