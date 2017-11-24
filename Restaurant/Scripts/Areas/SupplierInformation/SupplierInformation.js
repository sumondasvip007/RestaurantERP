angular.module('myApp').controller('AddSupplierController', function ($scope, $window, $http,$route, $location, $routeParams, $filter) {
    $scope.button = "Save";



    $scope.AddnewSupplierInformation = function () {
        console.log($scope.AddSupplierform.$valid);
        if ($scope.AddSupplierform.$valid) {
            if ($scope.SupplierInformation.SupplierId) {
                $http({
                    method: 'POST',
                    url: 'SupplierInformation/UpdateSupplierInformation',
                    data: $scope.SupplierInformation
                }).success(function(data) {
                    if (data.success) {
                        $route.reload();
                        toastr.success(data.successMessage);
                        $scope.successMessage = data.successMessage;
                        $window.location.href = "#/ShowAllSupplier";
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });

            } else {

                //########### ADD Supplier  ##########################
                console.log("supplier info");
                $http({
                    method: 'POST',
                    url: 'SupplierInformation/AddSupplier',
                    data: $scope.SupplierInformation
                }).success(function(data) {
                    if (data.success) {
                        $route.reload();
                        toastr.success(data.successMessage);
                        $scope.successMessage = data.successMessage;
                        $scope.SupplierInformation = {};

                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
            }
        } else {
            toastr.error("Please Fill Up All The Fields");
        }
    }
    //----------- cancel update  -----------
    $scope.CancelUpdate = function() {
        $window.location.href = "#/ShowProductionHouse";
    }


    //############ GET BY ID ###############
    if($routeParams.id)
    {
        $http({
            method: 'POST',
            url: 'SupplierInformation/EditSuppier',
            data: $routeParams
        }).success(function (data) {
            if (data.success) {
                $scope.SupplierInformation = data.result;
                $scope.button = "Update";

            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    };
});
