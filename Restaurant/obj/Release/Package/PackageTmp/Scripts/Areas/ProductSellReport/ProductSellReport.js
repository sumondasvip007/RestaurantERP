angular.module('myApp').controller('productSellReportController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    $scope.SellspointClick = function () {
       if ($scope.sellspoint==null) {
           $scope.reportButton = false;
       }
   }


    $scope.GetAllSellsPoint = function () {
        $http.get('/ProductSellReport/GetAllSellsPoint/').
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
    $scope.SearchProductSellList = function () {
        if ($scope.SearchProductSellListForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductSellReport/SearchProductSellList/',
                data: { sellsPointId: $scope.sellspoint, fromDate: $scope.FromDate, toDate: $scope.ToDate }
            }).success(function (data) {
                if (data.success) {
                    console.log(data);
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
            $scope.reportButton = false;
        }

    };

    $scope.GenerateReportForSearchResult = function () {
        if ($scope.SearchProductSellListForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductSellReport/GenerateReportForSearchResult/',
                data: { sellsPointId: $scope.sellspoint, fromDate: $scope.FromDate, toDate: $scope.ToDate, productList: $scope.ProductList, totalAmount: $scope.totalAmount }
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
        if ($scope.SearchProductSellListForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductSellReport/ViewPdfReportForSearchResult/',
                data: { sellsPointId: $scope.sellspoint, fromDate: $scope.FromDate, toDate: $scope.ToDate, productList: $scope.ProductList, totalAmount: $scope.totalAmount }
            }).success(function (data) {
                if (data.success) {
                    console.log(data)
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