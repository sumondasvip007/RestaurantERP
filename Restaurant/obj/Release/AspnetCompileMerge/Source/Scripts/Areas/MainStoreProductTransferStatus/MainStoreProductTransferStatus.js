angular.module('myApp').controller('MainStoreProductTransferStatus', function($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {
    //get all main store 
    $scope.GetAllMainstore = function() {
        $http.get('ProductionHouse/GetSellsHouseStore')
           .success(function (data) {
               if (data.success) { 
                   $scope.mainStore = data.result;
               } else {
                   toastr.error(data.errorMessage);
               }
           })
           .error(function (XMLHttpRequest, textStatus, errorThrown) {
               toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
           });
    }
    //get id from main store and request  for how many product out from production store in a given date 
    $scope.GetMainStoreInfo = function (toDate, fromDate,storeId) {
        var storeWithDate = {
            fromDate:fromDate,
            toDate:toDate,
            storeId:storeId
        }
        if (storeId != null) {
            $http({
                method: 'POST',
                url: '/MainStoreProductTransferStatus/GetMainStoreToProductionHouseInfo',
                data: storeWithDate
            }).success(function(data) {
                if (data.success) {
                    $scope.Product = data.result;
                    if (data.result.length === 0) {
                        toastr.error("No Data Found");
                    }
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        } else {
            toastr.error("Select Store");
            $scope.Product = null;
        }


    }



    


});


