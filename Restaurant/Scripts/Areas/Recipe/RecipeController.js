angular.module('myApp').controller('NutritionController', function ($scope, $window, $http, $location, $routeParams, $filter) {
    
    $scope.LoadNutrition = function () {
        $scope.rowCollection = [];
        $http.get('/Nutrition/GetNutritionList/').
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

    $scope.SaveNewNutrition = function () {
        if ($scope.newNutritionForm.$valid) {
            $http({
                method: 'POST',
                url: '/Nutrition/SaveNewNutrition/',
                data: $scope.nutrition
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.nutrition = {};
                    $scope.LoadNutrition();
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

    $scope.LoadNutritionById = function()
    {
        $http.get('/Nutrition/GetNutritionById/' + $routeParams.id)
      .success(function (data) {
          if (data.success) {
              $scope.editNutrition = data.result;
              //if ($scope.editInstrument.premium != 0) {
              //    $scope.premiumRadio = 'Premium';
              //}
              //if ($scope.editInstrument.discount != 0) {
              //    $scope.premiumRadio = 'Discount';
              //}
          }
          else {
              toastr.error(data.errorMessage);
          }
      }).
      error(function (XMLHttpRequest, textStatus, errorThrown) {
          toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
      });
    }

    $scope.UpdateNutrition = function () {

        if ($scope.EditNutritionForm.$valid) {
            $http({
                method: 'POST',
                url: '/Nutrition/UpdateNutrition/'+ $routeParams.id,
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

    $scope.Reset = function () {
        $scope.nutrition = {};
    };

    $scope.ResetNutrition = function () {
        $scope.LoadNutritionById();
    };

})