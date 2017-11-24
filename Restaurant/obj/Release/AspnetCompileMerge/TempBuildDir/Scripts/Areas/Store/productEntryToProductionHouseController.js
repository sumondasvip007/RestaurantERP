angular.module('myApp').controller('productEntryToProductionHouseController',
    function ($scope, $window, $http, $uibModal, $location, $routeParams, $filter, $route) {
        $scope.itemsPerPage = [10, 15, 20, 25, 30];
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

        $scope.GetProductionHouseProductList = function (StoreId) {
            if (StoreId != null) {
                $scope.ProductionHouseProductList = [];
                $http.get('/ProductEntryToProductionHouse/GetProductionHouseProductList/?id=' + StoreId).
                    success(function(data) {
                        if (data.success) {
                            $scope.ProductionHouseProductList = data.result;
                        } else {
                            toastr.error(data.errorMessage);
                        }
                    }).
                    error(function(XMLHttpRequest, textStatus, errorThrown) {
                        toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                    });
            } else {
                $scope.ProductionHouseProductList = null;
                toastr.error("Select Production House");
                $route.reload();

            }
        };
        $scope.ProductToStore = [];

        $scope.selectProduct = function (p) {
            var productIndex = $scope.ProductToStore.indexOf(p);
            if (productIndex == -1) {
                $scope.ProductToStore.push(p); //Add the selected host into array

            } else {
                $scope.ProductToStore.splice(productIndex, 1); //Remove the selected host
                //$scope.ProductToStore.push(p);
            }
        };
        $scope.SaveProductToProductionHouse = function() {

            if ($scope.ProductEntryToProductionHouseForm.$invalid) {
                toastr.error("Please fill all fields");

            } 


                var j = 0;
                if ($scope.ProductToStore.length > 0) {
                    for (var i = 0; i < $scope.ProductToStore.length; i++) {

                        if (!$scope.ProductToStore[i].Quantity) {
                            toastr.error('Quantity required for ' + $scope.ProductToStore[i].ProductName);
                            j = 1;
                            break;
                        }
                        if ($scope.ProductToStore[i].Quantity === "") {
                            j = 1;
                            toastr.error('Quantity required for ' + $scope.ProductToStore[i].ProductName);
                            break;
                        }

                    }
                    if (j === 0) {
                        console.log("ok");
                        $http({
                            method: 'POST',
                            url: '/ProductEntryToProductionHouse/SaveProductToProductionHouse/',
                            data: { StoreId: $scope.ProductionHose.StoreId, productList: $scope.ProductToStore }
                        }).success(function(data) {
                            if (data.success) {
                                toastr.success(data.successMessage);
                                $scope.ProductToStore = {};
                                $route.reload();
                            } else {
                                toastr.error(data.errorMessage);
                            }
                        }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                        });
                    };
                } else {
                    toastr.error("please select product");
                }

        };

        //$scope.getProducts = function () {
        //    $http.get('/ProductEntryToProductionHouse/GetProductList/' + $scope.productionHouseId)
        //        .success(function (data) {
        //            if (data.success) {
        //                $scope.product = data.result;
        //                //console.log($scope.product);
        //            } else {
        //                toastr.error(data.errorMessage);
        //            }
        //        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
        //            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        //        });
        //}

        //$scope.getUnits = function () {
        //    $http.get('/ProductEntryToProductionHouse/GetUnits')
        //        .success(function (data) {
        //            if (data.success) {
        //                $scope.uints = data.result;
        //                // console.log($scope.product);
        //            } else {
        //                toastr.error(data.errorMessage);
        //            }
        //        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
        //            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        //        });
        //}

        //$scope.Submit = function () {
        //    $scope.product = {
        //        ProductionHouseId: $scope.productionHouseId,
        //        ProductId: $scope.productId,
        //        Unit: $scope.unitId,
        //        Quantity: $scope.quantity
        //    };
        //    if (confirm("Are sure want to save it?") == true) {
        //        $http({
        //            method: 'POST',
        //            url: '/ProductEntryToProductionHouse/Save',
        //            data: $scope.product
        //        }).success(function (data) {
        //            if (data.success) {
        //                toastr.success(data.successMessage);
        //                clearField();
        //            } else {
        //                toastr.error(data.errorMessage);
        //            }
        //        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
        //            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        //        });
        //    }
        //}

        //function clearField() {
        //    $scope.productionHouseId = '';
        //    $scope.productId = '';
        //    $scope.unitId = '';
        //    $scope.quantity = '';
        //}
    });