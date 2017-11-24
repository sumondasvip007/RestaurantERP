angular.module('myApp').controller('ProductToStoreController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];

    $scope.Transferable = false;
    $scope.GetAllStoreList = function () {
        $scope.StoreList = [];
        $http.get('/ProductToStore/GetAllStoreList/').
          success(function (data) {
              if (data.success) {
                  $scope.StoreList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };
    
    $scope.GetAllSupplierList = function () {
        $scope.SupplierList = [];
        $http.get('/ProductToStore/GetAllSupplierList/').
          success(function (data) {
              if (data.success) {
                  $scope.SupplierList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    $scope.GetSupplierProductList = function (SupplierId) {
        if (SupplierId != null) {
            $scope.SupplierProductList = [];
            $http.get('/ProductToStore/GetSupplierProductList/?id=' + SupplierId).
                success(function(data) {
                    if (data.success) {
                        $scope.SupplierProductList = data.result;
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).
                error(function(XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
        }
        else {
            $scope.SupplierProductList = null;
            toastr.error("Select Supplier");
            $route.reload();
        }
    };

    $scope.GetAllUnits = function () {
        $scope.unitList = [];
        $http.get('/ProductEntryToProductionHouse/GetUnits/').
          success(function (data) {
              if (data.success) {
                  $scope.unitList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };
    $scope.onStoreChange = function (storeId) {
        $http.get('/ProductToStore/GetStoreInfo?storeId=' + storeId).
        success(function (data) {
            if (data.success) {
                if (data.result === true) {
                    $scope.Transferable = true;
                } else {
                    $scope.Transferable = false;
                }
            }
            else {
                toastr.error("Select a Store");
            }
        }).
        error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
        
    }

  
    $scope.ProductToStore = [];
    $scope.enable = false;
    $scope.selectProduct = function (p, index, $event) {
        
           $scope.enable = false;
        var productIndex = $scope.ProductToStore.indexOf(p);
        if (p.ProductTypeId !== 3) {
            if (productIndex == -1) {  
                $scope.ProductToStore.push(p); //Add the selected host into array

        } else {
            $scope.ProductToStore.splice(productIndex, 1); //Remove the selected host
            //$scope.ProductToStore.push(p);
        }
        } else {
            if ($scope.Transferable === true) {
                if (productIndex === -1) {
                    $scope.ProductToStore.push(p); //Add the selected host into array

                } else {
                   
                    $scope.ProductToStore.splice(productIndex, 1); //Remove the selected host
                    //$scope.ProductToStore.push(p);
                }
            } else {
                
                $event.target.checked = false;
                $scope.enable = true;
               
                toastr.error("Please select trade item store for " + p.ProductName);
            }
            
        }
    };
    $scope.SaveProductToStore = function () {

        if ($scope.ProductToStoreForm.$invalid) {
            toastr.error("Please enter All Field ");
            return;
        }

        //var newProductToStore = [];
        //for (var i = 0; i < $scope.ProductToStore.length; i++) {
        //    var product = {};
        //    product.ProductId = $scope.ProductToStore[i].ProductId;
        //    product.Quantity = $scope.ProductToStore[i].Quantity;
        //    product.Unit = $scope.ProductToStore[i].Unit;
        //    product.UnitPrice = $scope.ProductToStore[i].UnitPrice;
        //    newProductToStore.push(product);
        //
        var j = 0;

       // console.log($scope.ProductToStore[i].Quantity);

        if ($scope.ProductToStore.length > 0) {
            for (var i = 0; i < $scope.ProductToStore.length; i++) {

                if(!$scope.ProductToStore[i].Quantity)
                {
                    toastr.error('Quantity required for ' + $scope.ProductToStore[i].ProductName);
                    j = 1;
                    break;
                }
                if (!$scope.ProductToStore[i].UnitPrice) {
                    toastr.error('Unit Price required for ' + $scope.ProductToStore[i].ProductName);
                    j = 1;
                    break;
                }

                if ($scope.ProductToStore[i].Quantity === "" || $scope.ProductToStore[i].Quantity === null || $scope.ProductToStore[i].Quantity === 0) {
                    //if ($scope.ProductToStore.Quantity[i] === "" || $scope.ProductToStore.Quantity[i] === null || $scope.ProductToStore.Quantity[i] === 0) {
                        j = 1;
                        toastr.error('Quantity required for ' + $scope.ProductToStore[i].ProductName);
                        break;

                }
                if ($scope.ProductToStore[i].Unit === null) {
                    j = 1;
                    toastr.error('Unit required for ' + $scope.ProductToStore[i].ProductName);
                    break;
                }
                if ($scope.ProductToStore[i].UnitPrice === "" || $scope.ProductToStore[i].UnitPrice === null || $scope.ProductToStore[i].UnitPrice === 0) {
                    j = 1;
                    toastr.error('Unit Price required for ' + $scope.ProductToStore[i].ProductName);
                    break;
                }
            }
            if (j === 0) {
                console.log("ok");
                $http({
                    method: 'POST',
                    url: '/ProductToStore/SaveProductToStore/',
                    data: { SupplierId: $scope.Supplier.SupplierId, StoreId: $scope.Store.StoreId, productList: $scope.ProductToStore }
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
    
});