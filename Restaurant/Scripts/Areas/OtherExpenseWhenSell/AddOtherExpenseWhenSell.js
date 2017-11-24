angular.module('myApp').controller('AddOtherExpenseWhenSellController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];

    $scope.GetAllSellsPoint = function () {
        $http.get('/OtherExpenseWhenSell/GetAllSellsPoint/').
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
        $http.get('/OtherExpenseWhenSell/GetAllGroup')
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
        $http.get('/OtherExpenseWhenSell/GetAllShift')
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
    $scope.AddOtherExpense = function () {

        if ($scope.AddOtherExpenseWhenSellForm.$valid) {
            $http({
                method: 'POST',
                url: '/OtherExpenseWhenSell/AddOtherExpenseWhenSell/',
                data: {
                    otherExpense: $scope.OtherExpense
                    //fromDate: $scope.FromDate
                }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    //$scope.OtherExpense = {};
                    $route.reload();
                } else {
                    toastr.error(data.ErrorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("Please fill up all required fields");
        }

    };
   
});