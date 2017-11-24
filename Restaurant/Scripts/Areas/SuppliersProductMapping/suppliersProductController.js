angular.module('myApp').controller('spMappingController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

   

    // Insert Update Store Information
    $scope.insert = function () {
        //if module form valid then run it 
        if ($scope.suppliersProductForm.$valid) {
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

    //Load Suppliers Information
    $scope.loadSuppliers = function () {
        $http({
            method: 'GET',
            url: '/SPMapping/GetSupplierList',
        }).then(function successCallback(response) {
            $scope.a = response.data.result;
            console.log($scope.a);
        }, function errorCallback(response) {
        });
    }

    //Load Purchse Product Information
    $scope.loadPurchaseProduct = function () {
        $http({
            method: 'GET',
            url: '/SPMapping/GetPurchaseProductList',
        }).then(function successCallback(response) {
            //show all to the table 

            $scope.a = response.data.result;
            console.log($scope.a);

        }, function errorCallback(response) {
        });
    }

    app.directive('csSelect', function () {
        return {
            require: '^stTable',
            template: '<input type="checkbox"/>',
            scope: {
                row: '=csSelect'
            },
            link: function (scope, element, attr, ctrl) {

                element.bind('change', function (evt) {
                    scope.$apply(function () {
                        ctrl.select(scope.row, 'multiple');
                    });
                });

                scope.$watch('row.isSelected', function (newValue, oldValue) {
                    if (newValue === true) {
                        element.parent().addClass('st-selected');
                    } else {
                        element.parent().removeClass('st-selected');
                    }
                });
            }
        };
    });

});

