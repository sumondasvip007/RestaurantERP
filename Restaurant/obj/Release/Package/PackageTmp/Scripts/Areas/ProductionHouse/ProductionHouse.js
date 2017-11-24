angular.module('myApp').controller('productionHouse', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {
    $scope.Button = "Save";
    $scope.isUpdate = false;
    $scope.AddnewProductionHouse = function () {

        if ($scope.productionHouseform.$invalid) {
            toastr.error("Please enter All Field ");
            return;
        }
        console.log();
        // ---------- EDIT A  PRODUCT HOUSE --------
        if ($scope.isUpdate === true) {
            var VM_ProductionHouseInfo = {
                ProductionHouseName:$scope.ProductionHouse.ProductionHouseName,
                ProductionHouseId: $scope.ProductionHouse.ProductionHouseId,
                NewMainStore: $scope.ProductionHouse.MainStore,
                NewOwnStore: $scope.ProductionHouse.OwnStore,
                OldMainStore: $scope.oldProductionHouse.oldMainStore,
                OldOwnStore: $scope.oldProductionHouse.oldOwnStore
            }
                $http({
                    method: 'POST',
                    url: '/ProductionHouse/EditProductionHouse',
                    data: VM_ProductionHouseInfo
                }).success(function (data) {
                    if (data.success) {
                        toastr.success(data.successMessage);
                        $window.location.href = "#/ShowProductionHouse";
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
        } else {
            //-------- ADD NEW PRODUCT HOUSE ------
                    $http({
                        method: 'POST',
                        url: '/ProductionHouse/AddProductionHouse',
                        data: $scope.ProductionHouse
                    }).success(function (data) {
                        if (data.success) {
                            toastr.success(data.successMessage);
                            $route.reload();
                        } else {
                            toastr.error(data.errorMessage);
                        }
                    }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                        toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                    });
                }
            }
        
   
    //----- GET ALL MAIN STORE BY [isProductionHouseStore] -----
    $scope.GetAllMainstore = function () {
        $http.get('/ProductionHouse/GetSellsHouseStore')
            .success(function (data) {
                if (data.success) {
                    $scope.selectedMainStoreIndex = 0;
                    var mainStore = data.result;
                    $scope.mainStore = mainStore;

                } else {
                    toastr.error(data.errorMessage);
                }
            })
            .error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };
    var GetAllOwnStore = [];
    // --------- GET ALL OWN STORE BY [isProductionHouseStore] -------
        $http.get('/ProductionHouse/GetALLOwnStoreWhichCanBeAdded')
            .success(function (data) {
                if (data.success) {
                    $scope.selectedOwnStoreIndex = 0;
                    var ownStore = data.result;
            
                    $scope.OwnStore = ownStore;

                    angular.forEach(ownStore, function (value, key) {
                        GetAllOwnStore.push(value);
                    });

                } else {
                    toastr.error(data.errorMessage);
                }
            })
            .error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    // ################# EDIT View Called ##########################
    if ($routeParams.id) {
        $scope.GetPrudctionHouseInformationById =
            $http({
                method: 'POST',
                url: '/ProductionHouse/GetPrudctionHouseInformationById',
                data: $routeParams
            }).success(function (data) {
                if (data.success) {
                    $scope.ProductionHouse = data.result;
                    var productionHouse = {
                        store_id: data.result.OwnStore.Id,
                        store_name: data.result.OwnStore.Name
                    }
                    GetAllOwnStore.push(productionHouse);
                    var ProductionHouse = {
                        ProductionHouseName: data.result.ProductionHouseName,
                        ProductionHouseId: data.result.ProductionHouseId,
                        MainStore: data.result.MainStore.Id,
                        OwnStore: data.result.OwnStore.Id 
                    }
                    var oldProductionHouse = {
                        ProductionHouseName: data.result.ProductionHouseName,
                        ProductionHouseId: data.result.ProductionHouseId,
                        oldMainStore: data.result.MainStore.Id,
                        oldOwnStore: data.result.OwnStore.Id
                    }
                    $scope.OwnStore = GetAllOwnStore;
                    $scope.ProductionHouse = ProductionHouse;
                    $scope.oldProductionHouse = oldProductionHouse;
                    console.log(oldProductionHouse);
                    $scope.Button = "Update";
                    $scope.isUpdate = true;
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
         });
    }
});