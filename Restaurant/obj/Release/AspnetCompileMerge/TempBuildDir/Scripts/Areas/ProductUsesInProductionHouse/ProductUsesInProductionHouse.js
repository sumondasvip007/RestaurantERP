angular.module('myApp').controller('ProductUsesInProductionHouse', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route, ProductServices, ProductionHouseService) {


    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    //load production house to dropdown
    $scope.LoadProductionHouse = function() { 
        ProductionHouseService.GetAllProductionHouseStore().then(function (data) {
            $scope.GetAllProductionHouse = data;
        });
    }

    //load pursable product after select production house from dropdown
    $scope.GetProductionHouse = function (storeId) {
        if (storeId === undefined) {
            $route.reload();
            return;
        }

        ProductServices.GetAllPursableProduct($scope.store.ownStore).then(function (data) {
            $scope.PursableProducts = data;
        });  
    }
    //add or remove product checkbox clicked
    $scope.ProductList = [];
    $scope.productsIndex = 0;
    $scope.selectProduct = function (product, rowIndex) {
        var productgroup = ProductServices.AddRemoveProductByCheckbox(product, $scope.ProductList);
        $scope.ProductList = productgroup;
    }
    //check quantity validation
    $scope.checkAvailableproductValidation = function (product, productionHouse, index) {
        ProductionHouseService.CheckValidationForProductExist(product, productionHouse, index).then(function (data) {
            if (data === false) {
                $scope.PursableProducts[index].Quantity[index] = "";
            }
        });
    }
    //add product 
    $scope.productUseInProductionHouse = function () {

        var validation = ProductionHouseService.CheckFormValidation($scope);
        if (validation === false) {
            return;
        }
        ProductionHouseService.SaveProductUsesInProductionHouse($scope.ProductList, $scope,$route);
    }
});
