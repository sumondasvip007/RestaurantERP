angular.module('myApp').controller('AccGroupController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {
    $scope.AddButton = true;
    $scope.GetAccNatureInformation = function () {
        $scope.AccNatureList = [];
        $http.get('/AccGroup/GetAccNatureInformation/').
          success(function (data) {
              if (data.success) {
                  $scope.AccNatureList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };
    $scope.GetAccGroupInformation = function () {
        $scope.AccGroupList = [];
        $http.get('/AccGroup/GetAccGroupInformation/').
          success(function (data) {
              if (data.success) {
                  $scope.AccGroupList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };
    $scope.SaveAccGroup = function () {
        if ($scope.AccGroupForm.$valid) {
            $http({
                method: 'POST',
                url: '/AccGroup/SaveAccGroup/',
                data: $scope.AccGroup
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.AccGroup = {};
                    $route.reload();
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("Please fill up all fields");
        }
    };

    $scope.EditAccGroup = function (x) {
        $scope.AccGroup = x;
        $scope.AddButton = false;
        $scope.UpdateButton = true;
        $scope.CancelButton = true;
    };
    
    $scope.CancelEdit = function (x) {
        $scope.AccGroup = {};
        $scope.AddButton = true;
        $scope.UpdateButton = false;
        $scope.CancelButton = false;
        $route.reload();
    };
    

    $scope.UpdateAccGroup = function () {
        if ($scope.AccGroupForm.$valid) {
            $http({
                method: 'POST',
                url: '/AccGroup/UpdateAccGroup/',
                data: $scope.AccGroup
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.AccGroup = {};
                    $scope.AddButton = true;
                    $scope.UpdateButton = false;
                    $scope.CancelButton = false;
                    $route.reload();
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("Please fill up all fields");
        }
    };
});
