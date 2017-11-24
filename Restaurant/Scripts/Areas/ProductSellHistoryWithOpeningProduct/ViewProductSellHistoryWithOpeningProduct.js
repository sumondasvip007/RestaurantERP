angular.module('myApp').controller('ProductSellHistoryWithOpeningProductController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    $scope.SellspointClick = function () {
        if ($scope.sellspoint == null) {
            $scope.reportButton = false;
            //toastr.error("Select a Store");
        }
    }
    $scope.SellsPointStoreList = [];

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
    $scope.getGroups = function () {
        $http.get('/ProductSellHistoryWithOpeningProduct/GetAllGroup')
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
        $http.get('/ProductSellHistoryWithOpeningProduct/GetAllShift')
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

    $scope.GetSellsPointProductList = function () {
        $scope.sellsPointProductList = [];
        $scope.sellsPointAllProductList = [];
        $http.get('/ProductSellHistoryWithOpeningProduct/GetAllSellsTypeProductList/').
          success(function (data) {
              if (data.success) {
                  $scope.sellsPointProductList = data.result;
                  $scope.sellsPointAllProductList = data.result;
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
        if ($scope.ProductSellHistoryWithOpeningProductForm.$valid) {
            $http({
                method: 'POST',
                url: '/ProductSellHistoryWithOpeningProduct/GetSellsPointProductQuantityList/',
                data: {
                    storeId: $scope.sellspoint,
                    productList: $scope.sellsPointAllProductList,
                    fromDate: $scope.FromDate,
                    shiftId: $scope.ShiftId
                }
            }).success(function(data) {
                if (data.success) {
                    $scope.sellsPointProductList = data.result;
                    $scope.totalAmount = data.TotalAmount;
                    $scope.totalProductionCostAmount = data.TotalProductionCostAmount;
                    $scope.less = data.Less;
                    $scope.due = data.Due;
                    $scope.complimen = data.Complimen;
                    $scope.damage = data.Damage;
                    $scope.totalOtherExpense = data.TotalOtherExpense;
                    $scope.netCash = data.NetCash;

                    if ($scope.sellspoint != null) {
                        $scope.reportButton = true;
                    } else {
                        $scope.reportButton = false;
                    }

                } else {
                    toastr.error(data.ErrorMessage);
                    $scope.reportButton = false;
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("Please fill up all fields");
        }
    };



    $scope.GenerateReportForSearchResult = function () {
        if ($scope.ProductSellHistoryWithOpeningProductForm.$valid) {
            $http({

                method: 'POST',
                url: '/ProductSellHistoryWithOpeningProduct/GenerateReportForSearchResult/',
                data: { storeId: $scope.sellspoint, fromDate: $scope.FromDate, shiftId: $scope.ShiftId }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.sellsPointProductList = data.result;
                    $scope.totalAmount = data.TotalAmount;
                    $scope.less = data.Less;
                    $scope.due = data.Due;
                    $scope.complimen = data.Complimen;
                    $scope.damage = data.Damage;
                    $scope.totalOtherExpense = data.TotalOtherExpense;
                    $scope.netCash = data.NetCash;
                    if ($scope.sellspoint != null) {
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
        }
        else {
            toastr.error("Please fill up all fields");
        }

    };


});