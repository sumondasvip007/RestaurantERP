angular.module('myApp').controller('storeController', function ($scope, $window, $http, $uibModal, $location, $routeParams, $filter, $route) {

    $scope.buttonName = "Save";
   

    $scope.itemsPerPage = [10, 15, 20, 25, 30];

    

    // Insert Update Store Information
    $scope.submit = function () {
        var isvalidate = true;
        if (!$scope.store.ischecked) {
            toastr.error("Please check a checkbox");
            isvalidate = false;
            return;
        } 
        //if module form valid then run it 
        if ($scope.storeForm.$valid && isvalidate === true) {

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
         

            $scope.store = response.data.result;
       
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

    $scope.makeSellsPointStore = function () {
        $scope.store.ischecked = true;
        $scope.store.IsSellsPointStore = true;
        $scope.store.isProductionHouseStore = false;
        $scope.store.is_mainStore = false;

    }



    $scope.makeProductionHouseStore = function () {
        $scope.store.ischecked = true;
        $scope.store.IsSellsPointStore = false;
        $scope.store.isProductionHouseStore = true;
        $scope.store.is_mainStore = false;
    }
    $scope.makeMainStore = function () {
        $scope.store.ischecked = true;
        $scope.store.IsSellsPointStore = false;
        $scope.store.isProductionHouseStore = false;
        $scope.store.is_mainStore = true;
    }
    $scope.makeYesNo = function (x) {
        if (x.is_mainStore) {
            return 'Yes';
        } else {
            return 'No';
        }
    }
});

