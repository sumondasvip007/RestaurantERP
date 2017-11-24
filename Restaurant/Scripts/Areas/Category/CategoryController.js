angular.module('myApp').controller('CategoryController', function ($scope, $window, $http, $location, $routeParams, $filter) {
   
    $scope.itemsByPage = 5;
    $scope.Category = {};
    $scope.SaveCategory = function() {
        if ($scope.newCategoryForm.$valid) {
                    $http({
                        method: 'POST',
                        url: '/Category/SaveNewCategory/',
                        data: $scope.Category
                    }).success(function (data) {
                        if (data.success) {
                            toastr.success(data.successMessage);
                            $scope.Category = {};
                            $scope.LoadCategory();
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

    $scope.Reset = function () {
        if ($scope.Category.Id != null && $scope.Category.Id != undefined)
        {
            $scope.EditCategory($scope.Category.Id);
    }
    else{
            $scope.Category = {};
        }
    };


    $scope.LoadCategory = function () {
        $scope.addOrUpdate = 'Add New Category';
        $scope.rowCollection = [];
        $http.get('/Category/GetAllCategory/').
          success(function (data) {
              if (data.success) {
                  $scope.rowCollection = data.result;
                  $scope.displayedCollection = [].concat($scope.rowCollection);
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };


    $scope.EditCategory = function(id) {

        $http.get('/Category/GetCategoryById?id='+id).
         success(function (data) {
             if (data.success) {
                 $scope.Category = data.result;
                 $scope.save = true;
                 $scope.addOrUpdate = 'Edit Category';
             }
             else {
                 toastr.error(data.errorMessage);
             }
         }).
         error(function (XMLHttpRequest, textStatus, errorThrown) {
             toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
         });
    };

    $scope.UpdateCategory = function() {
        if ($scope.newCategoryForm.$valid) {
            $http({
                method: 'POST',
                url: '/Category/UpdateCategory/',
                data: $scope.Category
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.Category = {};
                    $scope.LoadCategory();
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

})