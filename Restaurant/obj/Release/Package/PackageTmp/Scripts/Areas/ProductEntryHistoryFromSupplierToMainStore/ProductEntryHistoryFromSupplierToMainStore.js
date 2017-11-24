angular.module('myApp').controller('ProductEntryHistoryFromSupplierToMainStoreController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    $scope.SupplierClick = function () {

        if ($scope.SupplierInformation) {
            if (!$scope.SupplierInformation.SupplierId) {
                $scope.reportButton = false;
            }
        } else {
            $scope.reportButton = false;
        }
    }

    $scope.GetAllSuppier = function () {
        $scope.supplierList = [];
        $http.get('/ProductEntryHistoryFromSupplierToMainStore/GetAllSuppier/').
          success(function (data) {
              if (data.success) {
                  $scope.supplierList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };
  

    $scope.SearchProductTransactionFromSupplierToMainStoreList = function () {
        if ($scope.ProductTransferHistoryFromSupplierToMainStoreSearchForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductEntryHistoryFromSupplierToMainStore/SearchProductTransactionListFromSupplierToMainStore/',
                data: { supplierId: $scope.SupplierInformation.SupplierId, fromDate: $scope.FromDate, toDate: $scope.ToDate }
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
            $scope.reportButton = false;
        }

    };


    $scope.GenerateReportForSearchResult = function () {
        if ($scope.ProductTransferHistoryFromSupplierToMainStoreSearchForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductEntryHistoryFromSupplierToMainStore/GenerateReportForSearchResult/',
                data: { supplierId: $scope.SupplierInformation.SupplierId, fromDate: $scope.FromDate, toDate: $scope.ToDate, productList: $scope.ProductList, totalAmount: $scope.totalAmount }
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
        if ($scope.ProductTransferHistoryFromSupplierToMainStoreSearchForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductEntryHistoryFromSupplierToMainStore/ViewPdfReportForSearchResult/',
                data: { supplierId: $scope.SupplierInformation.SupplierId, fromDate: $scope.FromDate, toDate: $scope.ToDate, productList: $scope.ProductList, totalAmount: $scope.totalAmount }
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