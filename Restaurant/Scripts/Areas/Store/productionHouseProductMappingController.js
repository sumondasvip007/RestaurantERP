angular.module('myApp').controller('productionHouseProductMappingController',

    function ($scope, $window, $http, $uibModal, $location, $routeParams, $filter, $route) {
        $scope.itemsPerPage = [10, 15, 20, 25, 30];
        $scope.initialze = function () {
            $scope.getProductionHouse();
            $scope.MappedButtonShow = false;
        }



 
        $scope.getProductionHouse = function () {
            $http.get('/ProductionHouseProductMapping/GetProductionHouse')
                .success(function (data) {
                    if (data.success) {
                        $scope.productionHouse = data.result;
                        //console.log($scope.supplier);
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
        }

        $scope.getProducts = function (productionHouseId) {
            if (productionHouseId != null) {
                $http.get('/ProductionHouseProductMapping/GetProductList/' + $scope.suppliersProductForm.productionHouseId)
                .success(function (data) {
                    if (data.success) {
                        $scope.product = data.result;
                        console.log($scope.product);
                        $scope.MappedButtonShow = true;
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
            }

            else {
                $scope.MappedButtonShow = false;
                $scope.product = null;
                toastr.error("Select Production House");
            }
        }

        $scope.selectProduct = function (p) {

            var productIndex = $scope.product.indexOf(p);
            if (productIndex == -1) {
                $scope.product.push(p); //Add the selected host into array
            } else {
                $scope.product.splice(productIndex, 1); //Remove the selected host
                $scope.product.push(p);
            }
        }


        $scope.Submit = function() {
                $http({
                    method: 'POST',
                    url: '/ProductionHouseProductMapping/MapPtoductToProductionHouse/',
                    data: $scope.product
                }).success(function(data) {
                    if (data.success) {
                        $route.reload();
                        toastr.success(data.successMessage);
                        $scope.successMessage = data.successMessage;
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
          
        }


    });

