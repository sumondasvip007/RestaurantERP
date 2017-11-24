angular.module('myApp').controller('ControllerController', function ($scope, $window, $http, $location, $routeParams, $filter, dateconfigservice, isundefinedornullservice, globalvalueservice) {
    $scope.GetControllerList = function() {
        $scope.rowCollection = [];
        $http.get('/SystemManagement/Controller/GetControllerList/').
            success(function(data) {
                $scope.rowCollection = data.result;
                $scope.displayCollection = [].concat($scope.rowCollection);

            }).
            error(function(data) {
                toastr.error(data.errorMessage);
            });
    };

    $scope.LoadNewController = function() {
        $scope.controller = {};
    };
    $scope.GetDdlModule = function () {
        $scope.LoadNewController();
        $http.get('/SystemManagement/Controller/GetDdlModule/').
            success(function(data) {
                $scope.ModuleList = data.result;
            })
            .error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };
    $scope.SaveNewController = function() {
        if ($scope.AddNewControllerForm.$valid) {
            $http({
                method: 'POST',
                url: '/SystemManagement/Controller/SaveNewController/',
                data: $scope.controller
            }).success(function(data) {
                if (data.success) {
                    toastr.success(data.message);
                    $scope.controller = {};

                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
    };
    $scope.GetControllerById = function() {
        $http.get('/SystemManagement/Controller/GetControllerById/' + $routeParams.id)
            .success(function(data) {
                if (data.success) {
                    $scope.editController = data.result;
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };
    $scope.UpdateController = function () {
        if ($scope.EditControllerForm.$valid) {
            $http({
                method: 'POST',
                url: '/SystemManagement/Controller/UpdateController/',
                data: $scope.editController
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

})