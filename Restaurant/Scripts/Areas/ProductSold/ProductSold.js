angular.module('myApp').controller('productSoldController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];

    $scope.GetAllSellsPoint = function () {
        $http.get('/ProductSold/GetAllSellsPoint/').
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
    $scope.getGroups = function () {
        $http.get('/ProductSold/GetAllGroup')
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
        $http.get('/ProductSold/GetAllShift')
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
    $scope.ProductLoad = function () {
        if ($scope.sellspoint && $scope.ShiftId) {
            $http({
                method: 'POST',
                url: '/ProductSold/GetProductSold',
                data: {
                    sellsPointId: $scope.sellspoint, shiftId: $scope.ShiftId,
                    fromDate: $scope.FromDate
                }
            }).success(function(data) {
                if (data.success) {
                    
                    //get product id and suppier id 
                    $scope.Product = data.result;
                   
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
            });
        }
        else {
            $scope.Product = null;
            toastr.error("Select all required fields");
            $route.reload();

        }
    };
    //------- SELECTE ----------------
    $scope.productList = [];
    $scope.selectProduct = function (p) {
        var productIndex = $scope.productList.indexOf(p);
        if (productIndex === -1) {
            $scope.productList.push(p);//Add the selected host into array
        } else {
            $scope.productList.splice(productIndex, 1); //Remove the selected host
            //$scope.ProductToStore.push(p);
        }
    };
    $scope.checkValidation = function (index, product, sellspoint) {
        console.log(sellspoint);
            //get the input data
            var requestedQuantity = product.Qty[index];
            $scope.Product[index].Quantity = product.Qty[index];
            var storeProduct = {
                sellsPointId: sellspoint,
                productId: product.ProductId,
                shiftId: $scope.ShiftId
                //fromDate: $scope.FromDate

            }
            //check product available in Production house
            $http({
                method: 'POST',
                url: '/ProductSold/GetAvailableQuantity',
                data: storeProduct
            }).success(function(data) {
                if (data.success) {
                    //get product id and suppier id 
                    var availableQuantity = data.result;
                    var isProductAvailable = (availableQuantity - requestedQuantity);

                    //input data > production House prodcut quantity thne give error 
                    if (isProductAvailable < 0) {
                        var a = requestedQuantity.slice(0, -1);

                        $scope.rowCollection[index].Qty[index] = parseInt(a);
                        $scope.rowCollection[index].Quantity = parseInt(a);
                        toastr.info("Your available Product is " + " " + availableQuantity);
                    } else {
                        $scope.rowCollection[index].Quantity = requestedQuantity;
                    }
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
            });

    }


    //create a list 
    $scope.ProductSoldButton = function () {

        if (!$scope.sellspoint) {
            toastr.error("Please Select a Sells Point");
            return;
        }

        if (!$scope.ShiftId) {
            toastr.error("Please Select a Shift");
            return;
        }

      
        //var sellpoint = $scope.sellspoint;
        ////if (sellpoint == null) {
        ////    toastr.error("olease select");
        ////}
        //var validation = 'true';
        //angular.forEach(product, function (value, key) {

        //    if (productList[key].Quantity == null) {
        //        toastr.error("Enter Valid Quantity");
        //        validation = 'false';
        //    }
        //});

    

        for (var i = 0; i < $scope.productList.length; i++) {
            //if ($scope.productList[i].Quantity === null || $scope.productList[i].Quantity === "" || $scope.productList[i].Quantity === 0) {
            //    toastr.error("Enter a valid product quantity");
            //    return;
            //}
            
            if ($scope.productList[i].Quantity === null || $scope.productList[i].Quantity === "" || $scope.productList[i].Quantity === 0) {
                toastr.error("Enter a valid product quantity");
                return;
            }
            if (!$scope.productList[i].Quantity) {
                toastr.error("Enter a valid product quantity");
                return;
            }

        }
        if ($scope.productList.length === null || $scope.productList.length === 0) {
            toastr.error("Please Select Product");
            return;
        }
      

       

        $http({
            method: 'POST',
            url: '/ProductSold/AddSoldProductInfoToProductTransfer',
            data: {
                productsoldList: $scope.productList, 
                storeId: $scope.sellspoint,
                //groupId: $scope.GroupId,
                shiftId: $scope.ShiftId
                //fromDate: $scope.FromDate
            }
        }).success(function (data) {
            if (data.successMessage) {
                toastr.success(data.successMessage);
                $route.reload();
            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
        });
    }
});