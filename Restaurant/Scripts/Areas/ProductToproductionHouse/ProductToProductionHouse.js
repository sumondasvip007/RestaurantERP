angular.module('myApp').controller('productToProductionHouseController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route, ProductToProductionHouseServices) {
    $scope.itemsPerPage = [10, 15, 20, 25, 30];
//########### GET ALL Production House ####################
    $scope.GetAllOwnStore = function() {
        $http.get('/ProductionHouse/GetAllOwnStore')
            .success(function(data) {
                if (data.success) {
                 
                    $scope.ownStore = data.result;

                } else {
                    toastr.error(data.errorMessage);
                }
            })
            .error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };
    $scope.selectProduct = function (p) {
        var productIndex = $scope.ProductToStore.indexOf(p);
        if (productIndex === -1) {
            $scope.ProductToStore.push(p); //Add the selected host into array
        } else {
            $scope.ProductToStore.splice(productIndex, 1); //Remove the selected host
            //$scope.ProductToStore.push(p);
        }
    };

    $scope.ProductLoad = function() {
        if ($scope.ProductionHouse.ownStore) {
        $scope.GetPrudctionHouseInformationById =
            $http({
                method: 'POST',
                url: '/ProductToProductionHouse/GetAllProductByOwnStore',
                data: { id: $scope.ProductionHouse.ownStore }
            }).success(function(data) {
                if (data.success) {

                    $scope.Product = data.result;

                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {

            });
        }
        else {
            $scope.Product = null;
            toastr.error("Select a Production House Name");
            $route.reload();

        }
}
    //-- get suppier id -----
    $scope.GetSuppierId = function() {
        $http({
            method: 'POST',
            url: '/ProductToProductionHouse/GetSuppierInfoByProductId',
            data: { productId: $scope.ProductionHouse.ProductId }
        }).success(function(data) {
            if (data.success) {
                //get product id and suppier id 

                $scope.SuppierProduct = data.result;
            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function(XMLHttpRequest, textStatus, errorThrown) {

        });
    }

    var i = 0;
    //checking --- product quantity exist in main store 
    $scope.checkValidation = function (product, store, index, rowIndex,productIndex) {
        var requestedQuantity = product.Quantity[index];
        var productSupplier = {
            storeId: store.ownStore,
            productId: product.ProductInformation.ProductId
        }  
        //get data from main store 
        $http({
            method: 'POST',
            url: '/ProductToProductionHouse/GetProductAvailableQuantity',
            data: productSupplier
        }).success(function (data) {
            if (data.success) {
                //get product id and suppier id
                var availableproductQuantity = data.result;
                var isProductAvailable = 0;
                angular.forEach($scope.product,function(scopeProduct,key) {
                    if (product.ProductInformation.ProductId === scopeProduct.ProductInformation.ProductId) {
                        isProductAvailable = (availableproductQuantity - requestedQuantity);
                        if (isProductAvailable < 0) {
                            var a = requestedQuantity.slice(0, -1);
                            $scope.product[scopeProduct.ProductIndex].Quantity[index] = parseInt(a);
                            toastr.warning(availableproductQuantity + " " + "Products Available");
                            return;
                        }
                    }    
                    });
                isProductAvailable = (availableproductQuantity - requestedQuantity);

                if (isProductAvailable < 0) {
                    var a = requestedQuantity.slice(0, -1);
                    $scope.product[$scope.productIndex].Quantity[index] = parseInt(a);
                    toastr.warning(availableproductQuantity + " " + "Products Available");
                } 
            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {

        });
    }





    //$scope.checkValidation = function (product, store, index, rowIndex, productIndex) {
    //    var requestedQuantity = product.Quantity;
    //    var productSupplier = {
    //        storeId: store.ownStore,
    //        productId: product.ProductInformation.ProductId
    //    }
    //    //get data from main store 
    //    $http({
    //        method: 'POST',
    //        url: '/ProductToProductionHouse/GetProductAvailableQuantity',
    //        data: productSupplier
    //    }).success(function (data) {
    //        if (data.success) {
    //            //get product id and suppier id
    //            var availableproductQuantity = data.result;
    //            var isProductAvailable = 0;
    //            angular.forEach($scope.product, function (scopeProduct, key) {
    //                if (product.ProductInformation.ProductId === scopeProduct.ProductInformation.ProductId) {
    //                    isProductAvailable = (availableproductQuantity - requestedQuantity);
    //                    if (isProductAvailable < 0) {
    //                        var a = requestedQuantity.slice(0, -1);
    //                        $scope.product[scopeProduct.ProductIndex].Quantity = parseInt(a);
    //                        toastr.warning(availableproductQuantity + " " + "Products Available");
    //                        return;
    //                    }
    //                }
    //            });
    //            isProductAvailable = (availableproductQuantity - requestedQuantity);

    //            if (isProductAvailable < 0) {
    //                var a = requestedQuantity.slice(0, -1);
    //                $scope.product[$scope.productIndex].Quantity = parseInt(a);
    //                toastr.warning(availableproductQuantity + " " + "Products Available");
    //            }
    //        } else {
    //            toastr.error(data.errorMessage);
    //        }
    //    }).error(function (XMLHttpRequest, textStatus, errorThrown) {

    //    });
    //}



    var ProductList = [];
    //--------- save  --------------
    $scope.save = function () {

        var isvalidate = ProductToProductionHouseServices.CheckModelValidation($scope);
        if (isvalidate === false) {
            return;
        }

        var productList = $scope.product;
        angular.forEach($scope.product, function (product, key) {
            var Product = {};
            //
           
       
                var productToProductionHouse = {
                    StoreId: $scope.ProductionHouse.ownStore,
                    ProductId: product.ProductInformation.ProductId,
                    Quantity: product.Quantity,
                    Unit: product.ProductInformation.Unit
                };


                Product.ProductId = productToProductionHouse.ProductId;
                Product.StoreId = productToProductionHouse.StoreId;
                Product.Unit = productToProductionHouse.Unit;
                Product.Quantity = product.Quantity[product.QuatityIndex];                
                ProductList.push(Product);  
            
            i++;
           
        });
        if (isvalidate === true && ProductList.length != 0) {
            $http({
                method: 'POST',
                url: '/ProductToProductionHouse/AddProductToProductionHouse',
                data: ProductList
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $route.reload();

                } else {
                    toastr.error(errorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
     
            });
        }
     //Addding product to Production House 
    }



   // ------- Get unit ------------
  
    //------- SELETE ----------------
    $scope.product = [];
    var productsIndex = 0;
    $scope.selectProduct = function (p, rowIndex) {
        var productIndex = $scope.product.indexOf(p);


        if (productIndex === -1) {
            console.log(rowIndex);
            $scope.productIndex = productsIndex;
            productsIndex = productsIndex + 1;
            p.ProductIndex = $scope.productIndex;
            $scope.product.push(p); //Add the selected host into array
        } else {
            console.log(productIndex);
            $scope.product.splice(productIndex, 1); //Remove the selected host
            productsIndex = productsIndex - 1;
            $scope.productIndex = productsIndex;
            console.log($scope.product);
        }
    };


    //$scope.selectProduct = function (p, rowIndex) {
    //    var productIndex = $scope.product.indexOf(p);


    //    if (productIndex === -1) {
    //        console.log(rowIndex);
    //        $scope.productIndex = productsIndex;
    //        productsIndex = productsIndex + 1;
    //        p.ProductIndex = $scope.productIndex;
    //        $scope.product.push(p); //Add the selected host into array
    //    } else {
    //        console.log(productIndex);
    //        $scope.product.splice(productIndex, 1); //Remove the selected host
    //        productsIndex = productsIndex - 1;
    //        $scope.productIndex = productsIndex;
    //        console.log($scope.product);
    //    }
    //};
});

