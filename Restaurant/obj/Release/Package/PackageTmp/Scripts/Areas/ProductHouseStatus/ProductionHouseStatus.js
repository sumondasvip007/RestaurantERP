angular.module('myApp').controller('ProductionHouseStatus', function($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    //1. Production House Load 
    $scope.LoadProductionHouse = function() {

        $http.get('/ProductionHouse/GetAllOwnStore/').
            success(function(data) {
                if (data.success) {
                    $scope.productionHouseList = data.result;
                    
                } else {
                    
                    toastr.error(data.errorMessage);
                }
            }).
            error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });


        //load purchasable product status 
        $scope.PursableProductByProductionHouse = function(productionHouse) {


            if (productionHouse != null) {


                $http({
                    method: 'POST',
                    url: 'ProductionHouseStatus/GetPursableProductByProductionHouse',
                    data: { storeId: productionHouse }
                }).success(function(data) {
                    if (data.success) {
                            $scope.reportButton = true;                     
                        $scope.PursableProduct = data.result;
                     

                    } else {
                        toastr.error(data.errorMessage);
                        $scope.reportButton = false;
                    }
                }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });

                //load Sellable  product status 
                $http({
                    method: 'POST',
                    url: 'ProductionHouseStatus/GetSellAbleProductByProductionHouse',
                    data: { storeId: productionHouse }
                }).success(function(data) {
                    if (data.success) {
                       
                      

                            $scope.SellAbleProduct = data.result;
                        
                   

                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
            }
            else {
                toastr.error("Please Select a Productiuon House ");
                $scope.reportButton = false;
            }

           
        } 


    }






    



    







});
