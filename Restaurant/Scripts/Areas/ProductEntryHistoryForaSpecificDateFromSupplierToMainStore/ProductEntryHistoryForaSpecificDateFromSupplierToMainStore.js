angular.module('myApp').controller('ProductEntryHistoryForaSpecificDateFromSupplierToMainStoreController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];

    $scope.SearchProductTransactionFromSupplierToMainStoreList = function () {
        if ($scope.ProductTransferHistoryForaSpecificDateFromSupplierToMainStoreSearchForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductEntryHistoryForaSpecificDateFromSupplierToMainStore/SearchProductTransactionListFromSupplierToMainStore/',
                data: { date: $scope.FromDate }
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
        if ($scope.ProductTransferHistoryForaSpecificDateFromSupplierToMainStoreSearchForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductEntryHistoryForaSpecificDateFromSupplierToMainStore/GenerateReportForSearchResult/',
                data: { date: $scope.FromDate, productList: $scope.ProductList, totalAmount: $scope.totalAmount }
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
        if ($scope.ProductTransferHistoryForaSpecificDateFromSupplierToMainStoreSearchForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductEntryHistoryForaSpecificDateFromSupplierToMainStore/ViewPdfReportForSearchResult/',
                data: { date: $scope.FromDate, productList: $scope.ProductList, totalAmount: $scope.totalAmount }
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