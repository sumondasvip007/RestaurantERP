angular.module('myApp').controller('ProductEntryHistoryInSellPointController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {
    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    $scope.SellsPointStoreChange = function () {
        if (!$scope.SellsPoint.SellsPointStoreId) {
            $scope.reportButton = false;
        }
    }
    

    $scope.GetSellsPointInformation = function () {
        $scope.SellsPointList = [];
        $http.get('/ProductEntryHistoryInSellPoint/GetSellsPointInformation/').
          success(function (data) {
              if (data.success) {
                  $scope.SellsPointList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    $scope.SearchProductTransactionList = function () {
        if ($scope.ProductTransferHistorySearchForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductEntryHistoryInSellPoint/SearchProductTransactionList/',
                data: { sellsPointStoreId: $scope.SellsPoint.SellsPointStoreId, fromDate: $scope.FromDate, toDate: $scope.ToDate }
            }).success(function (data) {
                if (data.success) {
                    //toastr.success(data.successMessage);
                    $scope.ProductList = data.result;
                    $scope.totalAmount = data.TotalAmount;
                    $scope.reportButton = true;
                } else {
                    $scope.ProductList = {};
                    $scope.totalAmount = data.TotalAmount;
                    $scope.reportButton = false;
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("Please fill up all fields");
        }
        
    };
    $scope.GenerateReportForSearchResult = function () {
        if ($scope.ProductTransferHistorySearchForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductEntryHistoryInSellPoint/GenerateReportForSearchResult/',
                data: { sellsPointStoreId: $scope.SellsPoint.SellsPointStoreId, fromDate: $scope.FromDate, toDate: $scope.ToDate, productList: $scope.ProductList,totalAmount: $scope.totalAmount }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.ProductList = data.result;
                    $scope.totalAmount = data.TotalAmount;
                    $scope.pdfViewButton = true;
                } else {
                    $scope.ProductList = {};
                   
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("Please fill up all fields");
        }

    };

    
    $scope.ViewPdfReportForSearchResult = function () {
        if ($scope.ProductTransferHistorySearchForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductEntryHistoryInSellPoint/ViewPdfReportForSearchResult/'
                //data: { sellsPointStoreId: $scope.SellsPoint.SellsPointStoreId, fromDate: $scope.FromDate, toDate: $scope.ToDate, productList: $scope.ProductList, totalAmount: $scope.totalAmount }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.ProductList = data.result;
                    $scope.totalAmount = data.TotalAmount;
                } else {
                    $scope.ProductList = {};

                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("Please fill up all fields");
        }

    };
});