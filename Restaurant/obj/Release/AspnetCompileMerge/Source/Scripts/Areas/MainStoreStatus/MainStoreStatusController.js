angular.module('myApp').controller('MainStoreStatusController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];

    $scope.GetAllMainStoreList = function () {
        $scope.MainStoreList = [];
        $http.get('/MainStoreStatus/GetAllMainStoreList/').
          success(function (data) {
              if (data.success) {
                  $scope.MainStoreList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };
    
    $scope.GetMainStoreProductList = function () {
        $scope.MainStoreProductList = [];
        $http.get('/MainStoreStatus/GetAllPurchaseTypeProductList/').
          success(function (data) {
              if (data.success) {
                  $scope.MainStoreProductList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };
    $scope.GetMainStoreProductQuantityList = function () {
        console.log("ok");
        $http({
            method: 'POST',
            url: '/MainStoreStatus/GetMainStoreProductQuantityList/',
            data: { storeId: $scope.Store.StoreId, productList: $scope.MainStoreProductList }
        }).success(function (data) {
           
            if (data.success) {
                
                    $scope.MainStoreProductList = data.result;
                    $scope.totalAmount = data.TotalAmount;
                    if ($scope.Store.StoreId != null) {
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