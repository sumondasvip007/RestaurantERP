angular.module('myApp').controller('SellsPointStatusController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    $scope.GetAllSellsPointStoreList = function () {
        $scope.SellsPointStoreList = [];
        $http.get('/SellsPointStatus/GetAllStoreList/').
          success(function (data) {
              if (data.success) {
                  $scope.SellsPointStoreList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };
    $scope.GetSellsPointProductList = function() {
        $scope.sellsPointProductList = [];
        $http.get('/SellsPointStatus/GetAllSellsTypeProductList/').
          success(function (data) {
              if (data.success) {
                  $scope.sellsPointProductList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    $scope.GetSellsPointProductQuantityList = function () {
        console.log("ok");
        $http({
            method: 'POST',
            url: '/SellsPointStatus/GetSellsPointProductQuantityList/',
            data: { storeId: $scope.Store.StoreId, productList: $scope.sellsPointProductList }
        }).success(function (data) {
            if (data.success) {
                $scope.sellsPointProductList = data.result;
                $scope.totalAmount = data.TotalAmount;
                if ($scope.Store.StoreId!=null) {
                    $scope.reportButton = true;
                } else {
                    $scope.reportButton = false;
                }
              
            } else {
                toastr.error(data.ErrorMessage);
                $scope.reportButton = false;
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    };
});