angular.module('myApp').controller('DbBackUpController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.BackupDatabase = function () {
        $http.get('/DbBackUp/BackupDatabase').
                success(function(data) {
                if (data.success) {
                    if (data.result === "Database Backup Successfully") {
                        toastr.success(data.result);
                    } else {
                        toastr.error(data.result);
                    }
                 } else {
                        toastr.error(data.errorMessage);
                    }
                }).
                error(function (XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
    };

    $scope.LoadBackupPath = function() {
        $http.get('/DbBackUp/GetDbBackUpPath').
               success(function (data) {
                   if (data.success) {
                       $scope.Path = data.result;
                   } else {
                       toastr.error(data.errorMessage);
                   }
               }).
               error(function (XMLHttpRequest, textStatus, errorThrown) {
                   toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
               });
    }


});