angular.module('myApp').controller('ProductController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];

    $scope.GetAllProductType = function () {
        $scope.productTypeList = [];
        $http.get('/Product/GetAllProductType/').
          success(function (data) {
              if (data.success) {
                  $scope.productTypeList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
            }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    $scope.GetStoreInformation = function (ProductTypeId) {
        $scope.StoreList = [];
        $http.get('/Product/GetStoreInformation/?id=' + ProductTypeId).
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

    $scope.GetAllUnit = function () {
        $scope.unitList = [];
        $http.get('/ProductEntryToProductionHouse/GetUnits/').
          success(function (data) {
              if (data.success) {
                  $scope.unitList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    $scope.SaveProduct = function () {
        
        if ($scope.ProductAddForm.$valid) {
            $http({
                method: 'POST',
                url: '/Product/SaveProduct/',
                data: $scope.Product
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.Product = {};
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

    $scope.GetAllProduct = function () {
        $scope.EditFormDiv = true;
        $scope.productList = [];
        $http.get('/Product/GetAllProduct/').
          success(function (data) {
              if (data.success) {
                  $scope.productList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };


    $scope.DeleteProduct = function (Id) {
        var deleteItem = $window.confirm('Are you absolutely sure you want to delete?');
        if (deleteItem) {
            $http.get('/Product/DeleteProduct/?id=' + Id).
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


    $scope.EditProduct = function (row) {
        $scope.Product = row;
        $scope.GetStoreInformation(row.ProductTypeId);
        $scope.EditFormDiv = false;
        $scope.ProductListDiv = true;
        
    };

    $scope.CancelEdit=function() {
        $scope.EditFormDiv = true;
        $scope.ProductListDiv = false;
        $route.reload();
    }

    $scope.UpdateProduct = function () {
        
        if ($scope.ProductUpdateForm.$valid) {
            $http({
                method: 'POST',
                url: '/Product/UpdateProduct/',
                data: $scope.Product
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.Product = {};
                    $scope.EditFormDiv = true;
                    $scope.ProductListDiv = false;
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


});