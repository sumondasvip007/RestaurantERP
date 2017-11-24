angular.module('myApp').controller('CalculatorController', function ($scope, $window, $http, $location, $routeParams, $filter) {

    $scope.choices = [{ Choice_id: 'choice1' }, { Choice_id: 'choice2' }, { Choice_id: 'choice3' }];

   // $scope.standardRecipe.yield_factor = {};
    //$scope.selectedYF = 0;
    $scope.addNewChoice = function () {
        if ($scope.choices.length + 1 <= $scope.IngredientList.length) {
            var newItemNo = $scope.choices.length + 1;
            $scope.choices.push({ 'Choice_id': 'choice' + newItemNo });
        }
    };

    $scope.removeChoice = function (id) {
        if ($scope.choices.length > 1) {
            //var lastItem = $scope.choices.length - 1;
            $scope.choices.splice(id, 1);
            $scope.loadIngredients();
        }

    };
    $scope.span = 3;

    $scope.isUsed = function (index) {
        var selected = new Array();
       // alert($select.selected.Ingredient_name);
        $scope.selectedYF = $scope.standardRecipe.yield_factor;
       // $scope.s = $scope.standardRecipe.yield_factor;
     // alert($scope.selectedYF);
        //if (index!=0) {
        //alert(index);
        for (var i = 0; i < $scope.choices.length; i++) {
            for (var j = 0; j < $scope.choices.length; j++) {
                if (i == 0) {
                    selected[0] = $scope.choices[0].ingredient_id;
                }
                else if ($scope.choices[i].ingredient_id == undefined) {
                    continue;
                }
                else {
                    selected[i] = $scope.choices[i].ingredient_id;
                }
            }
        }
        //alert(selected.length);
        for (var i = 0; i < selected.length; i++) {
            if (index == i) {
                continue;
            }
            else if (selected[i] == $scope.choices[index].ingredient_id) {
                $scope.choices[index].ingredient_id = 0;
               // $scope.choices[index].ingredient_id = "";
                toastr.error("Item Already selected");
            }
        }
        //alert('mor');
        $scope.loadIngredients();

    };

    $scope.LoadAllDropDown = function () {
        $scope.loadIngredients();
        $scope.IngredientList = null;
        $http.get('/Calculator/GetddlIngredientList/')
        .success(function (data) {
            $scope.IngredientList = data.result;
            $scope.countries = data.result;
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

    $scope.GetStandardRecipeById = function (id) {
        if (id != 0 && id != undefined) {
            $http({
                method: 'POST',
                url: '/Calculator/GetStandardRecipeById/',
                data: { stdrecipeid: id }
            }).success(function (data) {
                if (data.success) {
                    $scope.choices = data.ingredientList;
                    $scope.loadIngredients();
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
    };

    $scope.loadIngredients = function () {
        $scope.rowCollection = [];
        $scope.displayedCollection = [];
        //if ($scope.standardRecipeForm.$valid && $scope.yieldFactorForm.$valid) {
        //var choice = [];
        //for (var i = 0 ; i < $scope.choices.length ; i++)
        //{
        //    choice[i] = {};
        //    choice[i] = $scope.choices[i].ingredient_id;
        //}
        $http({
            method: 'POST',
            url: '/Calculator/GetIngredientList/',
            data: { ingredientList: $scope.choices }
        }).success(function (data) {
            if (data.success) {
                $scope.collCollection = data.result;
                $scope.span = $scope.collCollection.length + 3;
                $scope.rowCollection = data.ingredients;
                $scope.displayedCollection = [].concat($scope.rowCollection);
                $scope.YieldfactorList = data.Yieldfactors;
                $scope.Total = data.total;
                var array = data.array;
                chart1.data = [                    
                    ['Component', 'Nutrient'],
                ];
                colchart.data = [
                   ['Component'], [],
                ];
               
                for (var i = 0; i < $scope.collCollection.length; i++) {
                    if ($scope.collCollection[i].nutrition_name != undefined)
                    {
                        chart1.data.push([$scope.collCollection[i].nutrition_name, array[i]]);
                        colchart.data[0].push($scope.collCollection[i].nutrition_name);
                    }                    
                }
                var chartValue = [];
                chartValue[0] = "Nutrition in Gram";
                for (var j = 1; j <= array.length; j++)
                {
                     chartValue.push(array[j - 1]);
                    //chart1.data[j - 1].push(array[j - 1]);
                }
                colchart.data[1] = chartValue;
            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
      
    };



    var chart1 = {};
    chart1.type = "PieChart";
    chart1.data = [
       ['Component', 'Nutrition'],
    ];
  //  chart1.data.push(['Services', 20000]);

   
    chart1.options = {
        displayExactValues: true,
        //width: 100,
        //legend: 'none',
        sliceVisibilityThreshold: 0,
        height: 400,
        is3D: true,
        chartArea: { left: 100, top: 10, bottom: 10, height: "100%" }
    };

    chart1.formatters = {
        number: [{
            columnNum: 1,
            //pattern: "$ #,##0.00"
        }]
    };


    var colchart = {};
    colchart.type = "ColumnChart";
    colchart.data = [
       ['Component', 'Nutrition'],
    ];
    //  chart1.data.push(['Services', 20000]);


    colchart.options = {
        displayExactValues: true,
        //width: 100,
        //legend: 'none',
        height: 400,
        is3D: true,
        chartArea: { left: 10, top: 10, bottom: 50, height: "100%" }
    };

    colchart.formatters = {
        number: [{
            columnNum: 1,
            //pattern: "$ #,##0.00"
        }]
    };


    $scope.chart = chart1;
    $scope.colchart = colchart;

    $scope.selected = { value: $scope.ingredientList };
   
})