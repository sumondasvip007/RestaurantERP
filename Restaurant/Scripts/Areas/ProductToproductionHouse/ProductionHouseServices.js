angular.module("myApp").factory('ProductToProductionHouseServices', function($http) {
    return {
        CheckModelValidation: function ($scope) {
            var isvalidate = true;
            if ($scope.productToProductionHouseForm.productionHouse.$invalid) {
                toastr.error("select a production house");
                return false;
            }
            if ($scope.product.length === 0) {
                toastr.error("please select a product");
                return false;
            }
            angular.forEach($scope.product, function (product, key) {

                if (product.Quantity == null || product.Quantity === 0 || product.Quantity === "") {
                    toastr.error("please give a product quantity");
                    isvalidate = false;
                    return;
                }
            });
            angular.forEach($scope.product, function (product, key) {

                if (product.Quantity) {
                    if (product.Quantity[product.QuatityIndex] == null || product.Quantity[product.QuatityIndex] === 0 || product.Quantity[product.QuatityIndex] === "") {
                        toastr.error("please give a product quantity");
                        isvalidate = false;
                        return;
                    }
                }
               
            });

            return isvalidate;
        }
             
    }
});