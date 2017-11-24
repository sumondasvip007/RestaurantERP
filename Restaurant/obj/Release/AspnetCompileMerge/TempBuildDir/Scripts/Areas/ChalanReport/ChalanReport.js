angular.module('myApp').controller('ChalanReport', function($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {
    
    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    //-- get all  from chalan report  -- 
    $scope.GetAllChalanReport = function () {
      
        var storeWithDate = {
            fromDate:$scope.FromDate,
            toDate:$scope.ToDate          
        }

        $http({
            method: 'POST',
            url: '/ChalanReportView/GetAllChalanReportByDate',
            data: storeWithDate
        }).success(function(data) {
            if (data.success) {
                $scope.chalanReport = data.result;
                if (data.result.length === 0) {
                    toastr.error("No Data Found");
                }
            } else {
                //toastr.error(data.errorMessage);
                toastr.error("Please select all field");
            }
        }).error(function(XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    } 


});
