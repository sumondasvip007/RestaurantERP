angular.module('myApp').controller('SubCategoryController', function ($scope, $window, $http, $location, $routeParams, $filter) {

    $scope.SubCategory = {};

    $scope.LoadAll = function() {
        $scope.addOrUpdate = 'Add New SubCategory';
        $scope.LoadCategoryDdl();
        $scope.LoadAllSubCatagory();
    };

    $scope.LoadAllSubCatagory = function (){
        $scope.rowCollection = [];
        $http.get('/SubCategory/GetAllSubCategory/').
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

    $scope.LoadCategoryDdl = function() {
        $http.get('/SubCategory/GetDdlCategory/').
         success(function (data) {
             if (data.success) {
                 $scope.CategoryList = data.result;
             }
             else {
                 toastr.error(data.errorMessage);
             }
         }).
         error(function (XMLHttpRequest, textStatus, errorThrown) {
             toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
         });
    };
   


    $scope.SaveSubCategory = function () {
    if ($scope.newSubCategoryForm.$valid) {
            $http({
                method: 'POST',
                url: '/SubCategory/SaveNewSubCategory/',
                data: $scope.SubCategory
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.SubCategory = {};
                    $scope.LoadAllSubCatagory();
                } else {
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


    $scope.EditSubCategory = function (id) {

        $http.get('/SubCategory/GetSubCategoryById?id=' + id).
         success(function (data) {
             if (data.success) {
                 $scope.SubCategory = data.result;
                 $scope.save = true;
                 $scope.addOrUpdate = 'Edit SubCategory';
             }
             else {
                 toastr.error(data.errorMessage);
             }
         }).
         error(function (XMLHttpRequest, textStatus, errorThrown) {
             toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
         });
    };

    $scope.UpdateSubCategory = function () {
        if ($scope.newSubCategoryForm.$valid) {
            $http({
                method: 'POST',
                url: '/SubCategory/UpdateSubCategory/',
                data: $scope.SubCategory
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.SubCategory = {};
                    $scope.LoadAll();
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
        if ($scope.SubCategory.Id != null && $scope.SubCategory.Id != undefined) {
            $scope.EditSubCategory($scope.SubCategory.Id);
        }
        else {
            $scope.SubCategory = {};
        }
    };

})