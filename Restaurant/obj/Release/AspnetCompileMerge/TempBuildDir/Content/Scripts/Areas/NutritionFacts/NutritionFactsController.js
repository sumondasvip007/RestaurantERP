angular.module('myApp').controller('NutritionFactsController', function ($scope, $window, $http, $location, $routeParams, $filter) {

    $scope.span = 5;

    $scope.LoadAllIngredients = function () {
        $scope.rowCollection = [];
        $http.get('/NutritionFacts/GetNutritionList/').
          success(function (data) {
              if (data.success) {

                  //$scope.rowCollection = data.result;
                  //$scope.displayedCollection = [].concat($scope.rowCollection);
                  $scope.collCollection = data.result;
                  $scope.span = $scope.collCollection.length + 3;
                  $http.get('/NutritionFacts/GetIngredientList/').
                     success(function (data) {
                         if (data.success) {
                             $scope.rowCollection = data.result;
                             $scope.displayedCollection = [].concat($scope.rowCollection);
                           
                             //$scope.displayedCollection = $scope.rowCollection;
                             //$scope.collCollection = data.colNames;
                             //for (i = 0; i < $scope.collCollection.length; i++)
                             //{
                             //    $scope.collCollection[i].retention_factor = $scope.rowCollection[i].retention_factor;
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
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    $scope.Reset = function () {
        $scope.ingredient = {};
        $scope.choices = {};
        $scope.choices = [{ Choice_id: 'choice1' }];
    };

})