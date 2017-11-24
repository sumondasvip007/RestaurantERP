angular.module('myApp').controller('StandardRecipeController', function ($scope, $window, $http, $location, $routeParams, $filter) {

    $scope.choices = [{ Choice_id: 'choice1' }, { Choice_id: 'choice2' }, { Choice_id: 'choice3' }];

    

    $scope.addNewChoice = function () {
        if ($scope.choices.length+1<=$scope.IngredientList.length) {
            var newItemNo = $scope.choices.length + 1;
            $scope.choices.push({ 'Choice_id': 'choice' + newItemNo });
        }
    };

    $scope.removeChoice = function (id) {
        if ($scope.choices.length > 1)
        {
            //var lastItem = $scope.choices.length - 1;
            $scope.choices.splice(id, 1);
            $scope.loadIngredients();
        }
       
    };
    $scope.span = 3;

    $scope.isUsed = function (index) {
        var selected = new Array();
        //if (index!=0) {
        //alert(index);
        for (var i = 0; i < $scope.choices.length; i++) {         
            for (var j = 0; j < $scope.choices.length; j++) {
                if (i == 0)
                {
                    selected[0] = $scope.choices[0].ingredient_id;
                }
                else if( $scope.choices[i].ingredient_id == undefined)
                {
                    continue;
                }
                else {                  
                    selected[i] = $scope.choices[i].ingredient_id;
                    }
                }
        }
        //alert(selected.length);
        for (var i = 0; i < selected.length; i++) {
            if(index == i)
            {
                continue;
            }
            else if (selected[i] == $scope.choices[index].ingredient_id) {
                $scope.choices[index].ingredient_id = 0;
                toastr.error("Item Already selected");
            }
        }
        //alert('mor');
        $scope.loadIngredients();
        
    };

    $scope.LoadAllDropDown = function () {
        $scope.loadIngredients();
        $scope.IngredientList = null;
        $http.get('/StandardRecipe/GetddlIngredientList/')
        .success(function (data) {
            $scope.IngredientList = data.result;
            $scope.UnitList = data.UnitList;
            $scope.StdRecipeList = data.StdRecipeList;
        }).
      error(function (XMLHttpRequest, textStatus, errorThrown) {
          toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
      })

    };

    $scope.Reset = function () {
        $scope.standardRecipe = {};
        $scope.choices = {};
        $scope.choices = [{ Choice_id: 'choice1' }];
    };

    $scope.SaveStandardRecipe = function () {
        if ($scope.standardRecipeForm.$valid && $scope.yieldFactorForm.$valid) {
            $http({
                method: 'POST',
                url: '/StandardRecipe/SaveStandardRecipe/',
                data: { standardrecipe: $scope.standardRecipe, ingredient: $scope.choices }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.standardRecipe = {};
                    $scope.choices = {};
                    $scope.choices = [{ Choice_id: 'choice1' }, { Choice_id: 'choice2' }, { Choice_id: 'choice3' }];
                 
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

    $scope.loadIngredients = function () {
        $scope.rowCollection = [];
        $scope.displayedCollection = [];
        //if ($scope.standardRecipeForm.$valid && $scope.yieldFactorForm.$valid) {
            $http({
                method: 'POST',
                url: '/StandardRecipe/GetIngredientList/',
                data: { ingredientList: $scope.choices }
            }).success(function (data) {
                if (data.success) {
                    $scope.collCollection = data.result;
                    $scope.span = $scope.collCollection.length + 3;
                    $scope.rowCollection = data.ingredients;
                    $scope.displayedCollection = [].concat($scope.rowCollection);
                    $scope.Total = data.total;
                    //var column = $scope.collCollection;
                    //for (var i=0; i< $scope.collCollection.length; i++) {
                    //    var sum = 0;
                    //    for(var j=0; j<$scope.rowCollection.length; j++)
                    //    {
                    //        var nutrition = $scope.collCollection[i].nutrition_name;
                    //        //var value = $sce.trustAsHtml($scope.rowCollection[j] + "." + $scope.collCollection[i].nutrition_name);
                    //        sum += $scope.rowCollection[j].$scope.collCollection[i].nutrition_name;
                    //    }
                    //    alert(sum);
                    //}

                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        //}
        //else {
        //    toastr.error("Please Fill up all fields");
        //}
    };

    $scope.GetSummOfRF = function (group) {
        var summ = 0;
        for (var i in group) {
            summ = summ + Number(group[i].nutrition_name);
        }
        return summ;
    };

})