angular.module("myApp").factory('ProductionHouseService', function ($http) {
    return {
        GetAllProductionHouseStore: function () {
            var data = {};
            var data = $http.get("/ProductionHouse/GetAllOwnStore").then(function (response) {
                return response.data.result;
            }, function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
            return data;
        },
        GetAvailableProductInProductionHouse: function (product, store) {
            var requestAvaliableProductQuatity = {
                storeId: store,
                productId: product.ProductId

            }
            var availableProduct = $http.post("/ProductUsesInProductionHouse/GetAvailableProductInProductionHouseJsonResult", requestAvaliableProductQuatity)
                .then(function (response) {
                    if (response.data.success === true) {
                        return response.data.result;
                    } else {
                        toastr.error(response.data.error);
                    }
                });
            return availableProduct;
        },
        CheckValidationForProductExist: function (product, store, index) {
            var isvalidate = true;
            var a = this.GetAvailableProductInProductionHouse(product, store, index).then(function (availableQuatityQuantity) {
                var requestedQuatity = product.Quantity[index];
                if ((availableQuatityQuantity - requestedQuatity) < 0) {
                    toastr.warning(availableQuatityQuantity + " " + "Products Available");
                    isvalidate = false;
                    return isvalidate;

                } else {
                    isvalidate = true;
                    return isvalidate;
                }
            });
            return a;
        },

        SaveProductUsesInProductionHouse: function (productList, $scope,$route) {

            var productGroups = [];
            angular.forEach(productList, function (product) {
                var theProduct = {}
                theProduct = {
                    ProductName: product.ProductName,
                    ProductId: product.ProductId,
                    Quantity: product.Quantity[product.QuatityIndex],
                    StoreId: $scope.store.ownStore,
                    Unit: product.Unit,
                    UnitPrice: product.UnitPrice
                }
                productGroups.push(theProduct);
            });
            $http({
                method: 'POST',
                url: '/ProductUsesInProductionHouse/ProductUseInProductionHouse/',
                data: {
                    productList: productGroups
                }
            }).success(function (data) {
                if (data.success) {
                    $route.reload();
                    toastr.success(data.successMesseage);
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        },
        CheckFormValidation: function ($scope, $route) {
            var isvalidate = true;
            console.log($scope.ProductList);
            if ($scope.productusesInProductionHouse.productionHouse.$invalid) {
                toastr.error("Select a product house ");
                return false;
            }
            if ($scope.ProductList.length === 0) {
                toastr.error("Select a product ");
                return false;
            }
            angular.forEach($scope.ProductList, function (product, key) {
                if (product.Quantity === 0 || product.Quantity === null) {
                    toastr.error("Give a product quantity  !");
                    return isvalidate = false;
                }
            });


            angular.forEach($scope.ProductList, function (product, key) {
           
                if (product.Quantity) {
                    if (product.Quantity[product.QuatityIndex] == null || product.Quantity[product.QuatityIndex] === 0 || product.Quantity[product.QuatityIndex] === "") {
                        toastr.error("please give a product quantity");
                        isvalidate = false;
                        return;
                    }
                }

            });

      
            return isvalidate;
        }


    }

});
