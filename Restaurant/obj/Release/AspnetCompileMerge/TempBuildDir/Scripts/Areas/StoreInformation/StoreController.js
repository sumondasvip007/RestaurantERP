angular.module('myApp').controller('storeController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {
    $scope.buttonName = "Save";

    // Insert Update Store Information
    $scope.submit = function () {
        //if module form valid then run it 
        if ($scope.storeForm.$valid) {
            //edit save change
            if ($scope.store.store_id) {
                $http({
                    method: 'POST',
                    url: '/Store/EditStoreById',
                    data: $scope.store
                }).success(function (data) {
                    if (data.success) {
                        $route.reload();
                        toastr.success(data.successMessage);
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
            } //if end 
                //else start
            else {
                //pass data to controller to save  
                $http({
                    method: 'POST',
                    url: '/Store/AddStore/',
                    data: $scope.store
                }).success(function (data) {
                    if (data.success) {
                        $route.reload();
                        toastr.success(data.successMessage);
                        $scope.successMessage = data.successMessage;
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
            }
        } else {
            toastr.error("Validation Error");
        }
    }

    //Load Store Information
    $scope.dataload = function () {
        $http({
            method: 'GET',
            url: '/Store/GetAllStore',
        }).then(function successCallback(response) {
            //show all to the table 

            $scope.a = response.data.result;
            console.log($scope.a);

        }, function errorCallback(response) {
        });
    }

    //Edit Store Information
    $scope.edit = function (id) {
        $http({
            method: 'POST',
            url: '/Store/GetStoreById',
            data: { id: id }
        }).then(function successCallback(response) {
            console.log(response.data.result);

            $scope.store = response.data.result;
            console.log(response);
            $scope.buttonName = "Update";
        }, function errorCallback(response) {
        });
    }

    //Delete Store Information
    $scope.delete = function (id) {

        $http({
            method: 'POST',
            url: '/Store/DeleteStorebyId',
            data: { id: id }
        }).success(function (data) {
            if (data.success) {
                $route.reload();
                toastr.success(data.successMessage);
            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    }
});

