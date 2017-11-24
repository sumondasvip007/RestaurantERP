angular.module('myApp').controller('SellsPointController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];

    $scope.GetStoreInformation = function () {
        $scope.StoreList = [];
        $http.get('/SellsPoint/GetStoreInformation/').
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


    $scope.SaveSellsPoint = function () {
        
        if ($scope.SellsPointAddForm.$valid) {
            $http({
                method: 'POST',
                url: '/SellsPoint/SaveSellsPoint/',
                data: $scope.SellsPoint
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.SellsPoint = {};
                    $route.reload();
                } else {
                    toastr.error(data.ErrorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("Please fill up all fields");
        }
        
    };

    
    $scope.GetAllSellsPoint = function () {
        $scope.EditSellsPointDiv = true;
        $scope.sellsPointList = [];
        $http.get('/SellsPoint/GetAllSellsPoint/').
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

    $scope.DeleteSellsPoint = function (Id) {
        var deleteItem = $window.confirm('Are you absolutely sure, you want to delete?');
        if (deleteItem) {
            $http.get('/SellsPoint/DeleteSellsPoint/?id=' + Id).
            success(function (data) {
                if (data.success) {
                    toastr.success(data.message);
                }
                else {
                    toastr.error(data.errorMessage);
                }
            }).
            error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
            $route.reload();
        };
    };
    $scope.EditSellsPoint = function (row) {
        var obj = { 'StoreId': row.SellsPointStoreId, 'StoreName': row.SellsPointStoreName };

        $scope.StoreList = [];
        $http.get('/SellsPoint/GetStoreInformation/').
          success(function (data) {
              if (data.success) {
                  $scope.StoreList = data.result;
                  $scope.StoreList.push(obj);
                  

              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });

        $scope.SellsPoint = row;
        $scope.SellsPointListDiv = true;
        $scope.EditSellsPointDiv = false;

    };


    $scope.CancelEdit = function () {
        $scope.EditSellsPointDiv = true;
        $scope.SellsPointListDiv = false;
        $route.reload();
    }

    $scope.UpdateSellsPoint = function () {
       
        if ($scope.SellsPointUpdateForm.$valid) {
            $http({
                method: 'POST',
                url: '/SellsPoint/UpdateSellsPoint/',
                data: $scope.SellsPoint
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    //$scope.SellsPoint = {};

                } else {
                   
                    toastr.error(data.ErrorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
            $scope.EditSellsPointDiv = true;
            $scope.SellsPointListDiv = false;
            $route.reload();
        }
        else {
            toastr.error("Please fill up all fields");
            $scope.EditSellsPointDiv = false;
            $scope.SellsPointListDiv = true;
        }
    };
});