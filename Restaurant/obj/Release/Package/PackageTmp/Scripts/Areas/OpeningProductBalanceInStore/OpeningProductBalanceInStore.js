angular.module('myApp').controller('OpeningProductBalanceInStoreController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];

    $scope.GetAllStoreList = function () {
        $scope.StoreList = [];
        $http.get('/OpeningProductBalanceInStore/GetAllStoreList/').
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

    $http.get('/OpeningProductBalanceInStore/GetAllProductList/').
                success(function (data) {
                    if (data.success) {
                        $scope.ProductList = data.result;
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).
                error(function (XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
  

    $scope.OpeningBalanceProductList = [];
    $scope.selectProduct = function (p, index, $event) {
        var productIndex = $scope.OpeningBalanceProductList.indexOf(p);
        if (productIndex === -1) {
            $scope.OpeningBalanceProductList.push(p); //Add the selected host into array

        } else {
            $scope.OpeningBalanceProductList.splice(productIndex, 1); //Remove the selected host

        }
    };

    $scope.SaveOpeningProductBalance = function () {

        if ($scope.OpeningProductBalanceInStoreForm.$invalid) {
            toastr.error("Please enter All Field ");
            return;
        }

        var j = 0;

        if ($scope.OpeningBalanceProductList.length > 0) {
            for (var i = 0; i < $scope.OpeningBalanceProductList.length; i++) {

                if (!$scope.OpeningBalanceProductList[i].OpeningBalance) {
                    toastr.error('Quantity required for ' + $scope.OpeningBalanceProductList[i].ProductName);
                    j = 1;
                    break;
                }
         

                if ($scope.OpeningBalanceProductList[i].OpeningBalance === "" || $scope.OpeningBalanceProductList[i].OpeningBalance === null || $scope.OpeningBalanceProductList[i].OpeningBalance === 0) {
                    j = 1;
                    toastr.error('Opening Balance required for ' + $scope.OpeningBalanceProductList[i].ProductName);
                    break;

                }
                if ($scope.OpeningBalanceProductList[i].Unit === null) {
                    j = 1;
                    toastr.error('Unit required for ' + $scope.OpeningBalanceProductList[i].ProductName);
                    break;
                }
            
            }
            if (j === 0) {
                console.log("ok");
                $http({
                    method: 'POST',
                    url: '/OpeningProductBalanceInStore/OpeningProductBalanceInStore/',
                    data: { StoreId: $scope.Store.StoreId, productList: $scope.OpeningBalanceProductList }
                }).success(function (data) {
                    if (data.success) {
                        toastr.success(data.successMessage);
                        $scope.OpeningBalanceProductList = {};
                        $route.reload();
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
            };
        } else {
            toastr.error("please select product");
        }
    };

});