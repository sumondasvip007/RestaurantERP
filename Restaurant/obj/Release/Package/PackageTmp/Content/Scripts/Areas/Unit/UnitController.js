angular.module('myApp').controller('UnitController', function ($scope, $window, $http, $location, $routeParams, $filter) {

    $scope.LoadUnits = function () {
        $scope.rowCollection = [];
        $http.get('/Unit/GetUnitList/').
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

    $scope.SaveNewUnit = function () {
        if ($scope.newUnitForm.$valid) {
            $http({
                method: 'POST',
                url: '/Unit/SaveNewUnit/',
                data: $scope.unit
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.unit = {};
                    $scope.LoadUnits();
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("Please Fill up all fields");
        }
    };

    $scope.LoadUnitById = function () {
        $http.get('/Unit/GetUnitById/' + $routeParams.id)
      .success(function (data) {
          if (data.success) {
              $scope.editunit = data.result;             
          }
          else {
              toastr.error(data.errorMessage);
          }
      }).
      error(function (XMLHttpRequest, textStatus, errorThrown) {
          toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
      });
    }

    $scope.UpdateUnit = function () {

        if ($scope.EditUnitForm.$valid) {
            $http({
                method: 'POST',
                url: '/Unit/UpdateUnit/' + $routeParams.id,
                data: $scope.editNutrition
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.editNutrition = {};
                    //$scope.LoadNutrition();
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("Please insert all data");
        }
    };

    $scope.DeleteNutrition = function (row) {
        var isDelete = confirm("Are you sure you want to delete employee!");
        if (isDelete == true) {
            $http({
                method: 'POST',
                url: '/Unit/DeleteUnit/',
                data: { id: row.id }
            }).success(function (data) {
                if (data.success) {
                    toastr.success("Deleted Successfully");

                    var index = $scope.rowCollection.indexOf(row);
                    if (index !== -1) {
                        $scope.rowCollection.splice(index, 1);
                    }
                }
                else {
                    toastr.error(data.errorMessage);
                }
            }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
        }
    };



    $scope.Reset = function () {
        $scope.nutrition = {};
    };

    $scope.ResetNutrition = function () {
        $scope.LoadNutritionById();
    };

})