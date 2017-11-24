angular.module('myApp').controller('phtoSProductTransferController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route,ProductionHouseToSellsPointServices) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    // ------- Get ProductionHouse Point ------------//
    $scope.GetProductionHouseList = function () {
        $scope.productionHouseList = [];
        $http.get('/PHtoSPProductTransfer/GetProductionHouseList/').
          success(function (data) {
              if (data.success) {
                  $scope.productionHouseList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    // ------- Get Sells Point ------------//
    $scope.GetSellsPointist = function () {
        $scope.sellsPointList = [];
        $http.get('/PHtoSPProductTransfer/GetSellsPointList/').
          success(function (data) {
              if (data.success) {
                  $scope.sellsPointList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    // ------- Get unit ------------//
    $scope.getUnits = function () {
        $http.get('/PHtoSPProductTransfer/GetUnits')
            .success(function (data) {
                if (data.success) {
                    $scope.unitLoad = data.result;

                } else {
                    toastr.error(data.errorMessage);
                }
            })
            .error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    }

    $scope.getGroups = function () {
        $http.get('/PHtoSPProductTransfer/GetAllGroup')
            .success(function (data) {
                if (data.success) {
                    $scope.groupList = data.result;

                } else {
                    toastr.error(data.errorMessage);
                }
            })
            .error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    }
    $scope.getShifts = function () {
        $http.get('/PHtoSPProductTransfer/GetAllShift')
            .success(function (data) {
                if (data.success) {
                    $scope.shiftList = data.result;

                } else {
                    toastr.error(data.errorMessage);
                }
            })
            .error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    }

    // ------- Get Product Information ------------//
    $scope.GetProductionHouseProductList = function (ProductionHouseId) {
        if (ProductionHouseId) {
        $scope.productionHouseProductList = [];
        $http.get('/PHtoSPProductTransfer/GetProductionHouseProductList/?ProductionHouseStoreId=' + ProductionHouseId).
          success(function (data) {
              if (data.success) {
                  $scope.productionHouseProductList = data.result;
                  //console.log(productionHouseProductList);
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
        } else {
            $scope.ProductionHouseProductList = null;
            toastr.error("Select a Production House");
            $route.reload();

        }
    };


    ////checking --- product quantity exist in main store 
    //$scope.checkValidation = function (ddlProductionHouse, ProductToStore, index, rowIndex, productIndex) {
    //    $scope.productionHouseProductList[index].Quantity = ProductToStore.Qty[index];
    //    var requestedQuantity = ProductToStore.Qty[index];
    //    var productData = {
    //        storeId: ddlProductionHouse.ProductionHouseId,
    //        productId: ProductToStore.ProductId
    //    }
    //    //get data from main store 

    //    $http({
    //        method: 'POST',
    //        url: '/PHtoSPProductTransfer/GetAvailableProductQuantity',
    //        data: productData
    //    }).success(function (data) {
    //        if (data.success) {
    //            //get product id and suppier id 

    //            var availableproductQuantity = data.result;
    //            var isProductAvailable = (availableproductQuantity - requestedQuantity);

    //            if (isProductAvailable < 0) {
    //                var a = requestedQuantity.slice(0, -1);
    //                $scope.rowCollection[index].Qty[index] = parseInt(a);
    //                $scope.rowCollection[index].Quantity = parseInt(a);
    //                toastr.error(" Requested Quanity ( " + requestedQuantity + " ) Exceeds Limit! ...  Quantity Available  : " + availableproductQuantity);
    //            } else {
    //                {
    //                    $scope.rowCollection[index].Quantity = requestedQuantity;
    //                }
    //            }
    //        } else {
    //            toastr.error(data.errorMessage);
    //        }
    //    }).error(function (XMLHttpRequest, textStatus, errorThrown) {

    //    });
    //}



    $scope.checkValidation = function (ddlProductionHouse, ProductToStore, index, rowIndex, productIndex) {
        //$scope.productionHouseProductList[index].Quantity = ProductToStore.Qty[index];
        var requestedQuantity = ProductToStore.Quantity[index];
        var productData = {
            storeId: ddlProductionHouse.ProductionHouseId,
            productId: ProductToStore.ProductId
        }
        //get data from main store 

        $http({
            method: 'POST',
            url: '/PHtoSPProductTransfer/GetAvailableProductQuantity',
            data: productData
        }).success(function (data) {
            if (data.success) {
                //get product id and suppier id 

                var availableproductQuantity = data.result;
                var isProductAvailable = 0;

                angular.forEach($scope.ProductToStore, function (scopeProduct, key) {
                    if (ProductToStore.ProductId === scopeProduct.ProductId) {

                        isProductAvailable = (availableproductQuantity - requestedQuantity);

                        if (isProductAvailable < 0) {
                            var a = requestedQuantity.slice(0, -1);
                            $scope.ProductToStore[scopeProduct.ProductIndex].Quantity[index] = parseInt(a);

                            toastr.error(" Requested Quanity ( " + requestedQuantity + " ) Exceeds Limit! ...  Quantity Available  : " + availableproductQuantity);
                        }
                    }
                });
                isProductAvailable = (availableproductQuantity - requestedQuantity);

                            if (isProductAvailable < 0) {
                                var a = requestedQuantity.slice(0, -1);
                                $scope.ProductToStore[$scope.productIndex].Quantity[index] = parseInt(a);

                                toastr.error(" Requested Quanity ( " + requestedQuantity + " ) Exceeds Limit! ...  Quantity Available  : " + availableproductQuantity);
                            }
               
            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {

        });
    }



    //$scope.checkValidation = function (product, store, index, rowIndex, productIndex) {
    //    var requestedQuantity = product.Quantity[index];
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
    //                        $scope.product[scopeProduct.ProductIndex].Quantity[index] = parseInt(a);
    //                        toastr.warning(availableproductQuantity + " " + "Products Available");
    //                        return;
    //                    }
    //                }
    //            });
    //            isProductAvailable = (availableproductQuantity - requestedQuantity);

    //            if (isProductAvailable < 0) {
    //                var a = requestedQuantity.slice(0, -1);
    //                $scope.product[$scope.productIndex].Quantity[index] = parseInt(a);
    //                toastr.warning(availableproductQuantity + " " + "Products Available");
    //            }
    //        } else {
    //            toastr.error(data.errorMessage);
    //        }
    //    }).error(function (XMLHttpRequest, textStatus, errorThrown) {

    //    });
    //}










    //------- SELECTED ----------------//
    //$scope.ProductList = [];
    //$scope.createProductList = function (p) {
    //    var productIndex = $scope.ProductList.indexOf(p);
    //    if (productIndex === -1) {
    //        $scope.ProductList.push(p); //Add the selected host into array

    //    } else {
    //        $scope.ProductList.splice(productIndex, 1); //Remove the selected host

    //    }
    //};



    $scope.ProductToStore = [];
    var productsIndex = 0;
    $scope.createProductList = function (p,rowIndex) {
        var productIndex = $scope.ProductToStore.indexOf(p);
        if (productIndex === -1) {
            console.log(rowIndex);
            $scope.productIndex = productsIndex;
            productsIndex = productsIndex + 1;
            p.ProductIndex = $scope.productIndex;
            $scope.ProductToStore.push(p); //Add the selected host into array

        } else {
            $scope.ProductToStore.splice(productIndex, 1); //Remove the selected host
            productsIndex = productsIndex - 1;
            $scope.productIndex = productsIndex;
        }
    };






    //------- Transfer Product ----------------//

    //$scope.SaveProductToProductionHouse = function () {

    //    if ($scope.PHtoSPProductTransfer.$invalid) {
    //        toastr.error("Please enter All Field ");
    //        return;
    //    }

    //    if ($scope.ProductList.length > 0) {
    //        for (var i = 0; i < $scope.ProductList.length; i++) {
    //            console.log($scope.ProductList[i]);
    //            if ($scope.ProductList[i].Quantity === null || $scope.ProductList[i].Quantity === "" || $scope.ProductList[i].Quantity === 0) {
    //                toastr.error("Enter a valid product quantity");
    //                console.log($scope.ProductList[i].Quantity);
    //                return;
    //            }
    //            if ($scope.ProductList[i].Unit === null || $scope.ProductList[i].Unit === "") {
    //                toastr.error("Select a unit ");
    //                return;
    //            }
    //            if (!$scope.ProductList[i].Quantity) {
    //                toastr.error("Enter a valid product quantity");
    //                return;
    //            }
    //        }

    //        $http({
    //            method: 'POST',
    //            url: '/PHtoSPProductTransfer/SaveProductToSellPoint/',
    //            data: {
    //                SellsPointId: $scope.ddlSellsPoint.SellsPointStoreId,
    //                ProductionHouseStoreId: $scope.ddlProductionHouse.ProductionHouseId,
    //                productList: $scope.ProductList
    //            }
    //        }).success(function(data) {
    //            if (data.success) {
    //                $route.reload();
    //                toastr.success(data.successMessage);

    //            } else {
    //                toastr.error(data.ErrorMessage);
    //            }
    //        }).error(function(XMLHttpRequest, textStatus, errorThrown) {
    //            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
    //        });
    //    }
    //    else {
    //        toastr.error("please select product");
    //    }
    //};


    var ProductList = [];

    $scope.SaveProductToProductionHouse = function () {

        //if ($scope.PHtoSPProductTransfer.$invalid) {
        //    toastr.error("Please enter All Field ");
        //    return;
        //}

        var isvalidate = ProductionHouseToSellsPointServices.CheckModelValidation($scope);
        if (isvalidate === false) {
            return;
        }



        var productList = $scope.ProductToStore;
        angular.forEach($scope.ProductToStore, function (productToStore, key) {
            var ProductToStore = {};
            //
           
       
            var productToProductionHouse = {
                //StoreId: $scope.ProductionHouse.ownStore,
                ProductId: productToStore.ProductId,
                Quantity: productToStore.Quantity,
                Unit: productToStore.Unit
            };


            ProductToStore.ProductId = productToProductionHouse.ProductId;
            //Product.StoreId = productToProductionHouse.StoreId;
            ProductToStore.Unit = productToProductionHouse.Unit;
            ProductToStore.Quantity = productToStore.Quantity[productToStore.Qty];
            ProductList.push(ProductToStore);
            
            i++;
           
        });


        if (isvalidate === true && ProductList.length != 0) {
            $http({
                method: 'POST',
                url: '/PHtoSPProductTransfer/SaveProductToSellPoint/',
                data: {
                    SellsPointId: $scope.ddlSellsPoint.SellsPointStoreId,
                    ProductionHouseStoreId: $scope.ddlProductionHouse.ProductionHouseId,
                    //groupId:$scope.GroupId,
                    shiftId: $scope.ShiftId,
                    //fromDate: $scope.FromDate,
                    productList: ProductList
                }
            }).success(function(data) {
                if (data.success) {
                    $route.reload();
                    toastr.success(data.successMessage);

                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("please select product");
        }
    }
     

});