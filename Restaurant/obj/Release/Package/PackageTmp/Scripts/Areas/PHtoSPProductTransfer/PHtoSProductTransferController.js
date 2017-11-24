angular.module('myApp').controller('phtoSProductTransferController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

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
    $scope.checkValidation = function (ddlProductionHouse, ProductToStore, index) {
        $scope.productionHouseProductList[index].Quantity = ProductToStore.Qty[index];
        var requestedQuantity = ProductToStore.Qty[index];
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
                var isProductAvailable = (availableproductQuantity - requestedQuantity);

                if (isProductAvailable < 0) {
                    var a = requestedQuantity.slice(0, -1);
                    $scope.rowCollection[index].Qty[index] = parseInt(a);
                    $scope.rowCollection[index].Quantity = parseInt(a);
                    toastr.error(" Requested Quanity ( " + requestedQuantity + " ) Exceeds Limit! ...  Quantity Available  : " + availableproductQuantity);
                } else {
                    {
                        $scope.rowCollection[index].Quantity = requestedQuantity;
                    }
                }
            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {

        });
    }
    //------- SELECTED ----------------//
    $scope.ProductList = [];
    $scope.createProductList = function (p) {
        var productIndex = $scope.ProductList.indexOf(p);
        if (productIndex === -1) {
            $scope.ProductList.push(p); //Add the selected host into array

        } else {
            $scope.ProductList.splice(productIndex, 1); //Remove the selected host

        }
    };

    //------- Transfer Product ----------------//

    $scope.SaveProductToProductionHouse = function () {

        if ($scope.PHtoSPProductTransfer.$invalid) {
            toastr.error("Please enter All Field ");
            return;
        }

        if ($scope.ProductList.length > 0) {
            for (var i = 0; i < $scope.ProductList.length; i++) {
                console.log($scope.ProductList[i]);
                if ($scope.ProductList[i].Quantity === null || $scope.ProductList[i].Quantity === "" || $scope.ProductList[i].Quantity === 0) {
                    toastr.error("Enter a valid product quantity");
                    console.log($scope.ProductList[i].Quantity);
                    return;
                }
                if ($scope.ProductList[i].Unit === null || $scope.ProductList[i].Unit === "") {
                    toastr.error("Select a unit ");
                    return;
                }
                if (!$scope.ProductList[i].Quantity) {
                    toastr.error("Enter a valid product quantity");
                    return;
                }
            }

            $http({
                method: 'POST',
                url: '/PHtoSPProductTransfer/SaveProductToSellPoint/',
                data: {
                    SellsPointId: $scope.ddlSellsPoint.SellsPointStoreId,
                    ProductionHouseStoreId: $scope.ddlProductionHouse.ProductionHouseId,
                    productList: $scope.ProductList
                }
            }).success(function(data) {
                if (data.success) {
                    $route.reload();
                    toastr.success(data.successMessage);

                } else {
                    toastr.error(data.ErrorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("please select product");
        }
    };
});