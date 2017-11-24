'use strict';
angular.module('myApp', ['ngRoute', 'smart-table', 'ngAnimate', 'ivh.treeview', 'ui.bootstrap', 'ngFileUpload', 'ngTinyScrollbar', 'fancyboxplus', 'blockUI', 'anguFixedHeaderTable', 'textAngular', 'googlechart', 'ui.select', 'ngSanitize'])
.config(function ($routeProvider, $locationProvider, blockUIConfig) {


    var id = ':ViewId';
    $routeProvider
        .when('/',
        {
            //redirectTo: '/'
            templateUrl: 'Home/Index'
        })

        .when('/Index',
        {
            templateUrl: 'Home/Index'
        })

        .when('/RedirectToMain',
        {
            templateUrl: 'Home/RedirectToMain'
        })
         .when('/Error',
        {

            controller: function () {
                toastr.error('sdsddad');
            },
            //templateUrl: $location.path()
            template: 'You do not have permission'
        })

        .when('/Login',
        {
            templateUrl: '/Account/Login'
        })
        .when('/Account/Login',
        {
            templateUrl: '/Account/Login'
        })
        .when('/Contact',
        {
            templateUrl: '/Home/Contact'
        })
        .when('/Register',
        {
            templateUrl: '/Account/Register',
            controller: 'RegisterController'
        })
         .when('/About',
        {
            templateUrl: '/Home/About'
        })
        .when('/ChangePassword',
        {
            templateUrl: '/Account/Manage'
        })
         .when('/Nutritions',
        {
            templateUrl: '/Nutrition/Nutritions',
            controller: 'NutritionController',
           
        })
         .when('/EditNutrition/:id',
        {
            templateUrl: '/Nutrition/EditNutrition',
            controller: 'NutritionController',
           
        })
         .when('/Ingredients',
        {
            templateUrl: '/Ingredient/Ingredients',
            controller: 'IngredientController',
        })
        .when('/IngredientList',
        {
            templateUrl: '/Ingredient/IngredientList',
            controller: 'IngredientController',
        })
         .when('/EditIngredient/:id',
        {
            templateUrl: '/Ingredient/EditIngredient',
            controller: 'IngredientController',
        })
         .when('/NutritionFacts',
        {
            templateUrl: '/NutritionFacts/NutritionFacts',
            controller: 'NutritionFactsController'
        })
         .when('/StandardRecipe',
        {
            templateUrl: '/StandardRecipe/StandardRecipe',
            controller: 'StandardRecipeController'
        })
         .when('/Calculator',
        {
            templateUrl: '/Calculator/Calculator',
            controller: 'CalculatorController'
        })
          .when('/Unit',
        {
            templateUrl: '/Unit/AddUnit',
            controller: 'UnitController'
        })
         .when('/EditUnit/:id',
        {
            templateUrl: '/Unit/EditUnit',
            controller: 'UnitController',
        })
        
        //If link not found
    .otherWise
    {
        redirectTo: '/'
    }


});
