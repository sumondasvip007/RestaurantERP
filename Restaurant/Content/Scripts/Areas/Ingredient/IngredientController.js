angular.module('myApp').controller('IngredientController', function ($scope, $window, $http, $location, $routeParams, $filter) {

    $scope.choices = [{ id: '0' }];
    $scope.editchoices = [];

    $scope.addNewChoice = function () {
        if ($scope.choices.length + 1 <= $scope.NutritionList.length) {
            var newItemNo = $scope.choices.length + 1;
            $scope.choices.push({ 'id': newItemNo });
        }
    };

    $scope.removeChoice = function (id) {
        if ($scope.choices.length > 1) {
            $scope.choices.splice(id, 1);
        }
    };


    $scope.isUsed = function (index) {
        var selected = new Array();
        //if (index!=0) {
        //alert(index);
        for (var i = 0; i < $scope.choices.length; i++) {
            for (var j = 0; j < $scope.choices.length; j++) {
                if (i == 0) {
                    selected[0] = $scope.choices[0].nutrition_id;
                }
                else if ($scope.choices[i].nutrition_id == undefined) {
                    continue;
                }
                else {
                    selected[i] = $scope.choices[i].nutrition_id;
                }
            }
        }
        for (var i = 0; i < selected.length; i++) {
            if (index == i) {
                continue;
            }
            else if (selected[i] == $scope.choices[index].nutrition_id) {
                $scope.choices[index].nutrition_id = 0;
                toastr.error("Item Already selected");
            }
        }
    };

    $scope.isUsedEdit = function (index) {
        var selected = new Array();
        //if (index!=0) {
        //alert(index);
        for (var i = 0; i < $scope.editchoices.length; i++) {
            for (var j = 0; j < $scope.editchoices.length; j++) {
                if (i == 0) {
                    selected[0] = $scope.editchoices[0].nutrition_id;
                }
                else if ($scope.editchoices[i].nutrition_id == undefined) {
                    continue;
                }
                else {
                    selected[i] = $scope.editchoices[i].nutrition_id;
                }
            }
        }
        for (var i = 0; i < selected.length; i++) {
            if (index == i) {
                continue;
            }
            else if (selected[i] == $scope.editchoices[index].nutrition_id) {
                $scope.editchoices[index].nutrition_id = 0;
                toastr.error("Item Already selected");
            }
        }
    };

    $scope.addNewEditChoice = function () {
        if ($scope.editchoices.length + 1 <= $scope.NutritionList.length) {
            var newItemNo = $scope.editchoices.length + 1;
            $scope.editchoices.push({ 'id': newItemNo });
        }
    };

    $scope.removeEditChoice = function (id) {
        if ($scope.editchoices.length > 1) {
            //var lastItem = $scope.editchoices.length - 1;
            $scope.editchoices.splice(id, 1);
        }
    };

    $scope.LoadAllDropDown = function () {
        $scope.NutritionList = null;
        $http.get('/Ingredient/GetddlNutritionList/')
        .success(function (data) {
            $scope.NutritionList = data.result;
            $scope.UnitList = data.UnitList;
        }).
      error(function (XMLHttpRequest, textStatus, errorThrown) {
          toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
      })

    };

    $scope.Reset = function () {
        $scope.ingredient = {};
        $scope.choices = {};
        $scope.choices = [{ Choice_id: 'choice1' }];
    };

    $scope.SaveIngredient = function () {
        if($scope.newIngredientForm.$valid && $scope.nutritionListForm.$valid)
        {
            $http({
                method: 'POST',
                url: '/Ingredient/SaveIngredient/',
                data: { ingredient: $scope.ingredient, nutrition: $scope.choices }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.ingredient = {};
                    $scope.choices = {};
                    $scope.choices = [{ Choice_id: 'choice1' }];
                    //$scope.LoadNutrition();
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else
        {
            toastr.error("Please Fill up all fields"); 
        }
    };

    $scope.span = 5;

    $scope.LoadAllIngredients = function () {
        $scope.rowCollection = [];
        $http.get('/NutritionFacts/GetNutritionList/').
          success(function (data) {
              if (data.success) {

               
                  $scope.collCollection = data.result;
                  $scope.span = $scope.collCollection.length + 4;
                  $http.get('/NutritionFacts/GetIngredientList/').
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
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };
    
    $scope.LoadIngredientById = function () {
        $scope.LoadAllDropDown()
        $http.get('/Ingredient/GetIngredientById/' + $routeParams.id)
        .success(function (data) {
            if (data.success) {
                $scope.editIngredient = data.result;
                $scope.editchoices = data.nutritionlist;
                $scope.NutritionList = data.ddlnutrition;
            }
            else {
                toastr.error(data.errorMessage);
            }
     }).
     error(function (XMLHttpRequest, textStatus, errorThrown) {
         toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
     });
    };

    $scope.UpdateIngredient = function () {
        if ($scope.EditIngredientForm.$valid) {
            $http({
                method: 'POST',
                url: '/Ingredient/UpdateIngredient/' + $routeParams.id,
                data: { ingredient: $scope.editIngredient, nutrition: $scope.editchoices }
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

    $scope.ReseteditIngredient = function () {
        $scope.LoadIngredientById();
    };

  
})