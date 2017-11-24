angular.module("myApp").factory('ProductionHouseToSellsPointServices', function ($http) {
    return {
        CheckModelValidation: function ($scope) {
            var isvalidate = true;
            if ($scope.PHtoSPProductTransfer.SellsPointStoreId.$invalid) {
                toastr.error("select a Sells Point Store");
                return false;
            }
            if ($scope.PHtoSPProductTransfer.ProductionHouseId.$invalid) {
                toastr.error("select a production house");
                return false;
            }
            //if ($scope.PHtoSPProductTransfer.GroupId.$invalid) {
            //    toastr.error("select a Group");
            //    return false;
            //}
            if ($scope.PHtoSPProductTransfer.ShiftId.$invalid) {
                toastr.error("select a Shift");
                return false;
            }
            if ($scope.PHtoSPProductTransfer.$invalid) {
                toastr.error("select All Required Field");
                return false;
            }
            if ($scope.ProductToStore.length === 0) {
                toastr.error("please select a product");
                return false;
            }
            angular.forEach($scope.ProductToStore, function (ProductToStore, key) {

                if (ProductToStore.Quantity == null || ProductToStore.Quantity === 0 || ProductToStore.Quantity === "") {
                    toastr.error("please give a product quantity");
                    isvalidate = false;
                    return;
                }
            });
            angular.forEach($scope.ProductToStore, function (ProductToStore, key) {

                if (ProductToStore.Quantity) {
                    if (ProductToStore.Quantity[ProductToStore.QuatityIndex] == null || ProductToStore.Quantity[ProductToStore.QuatityIndex] === 0 || ProductToStore.Quantity[ProductToStore.QuatityIndex] === "") {
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