angular.module('myApp').controller('AccLedgerController', function($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route, AccLedgerService) {
    $scope.isUpdate = false;
    $scope.button = "Save";

    $scope.loadBalanceType = function() {
        AccLedgerService.GetBalanceTypes().then(function(data) {
            $scope.balanceTypeList = data;
        });
    }

    $scope.loadGroupList = function() {
        AccLedgerService.GetAllGroupList().then(function(data) {
            $scope.AccGroupList = data;
        });
    }


    $scope.saveLedger = function() {

        var isValidate = AccLedgerService.LedgerValidation($scope);
        if (isValidate === false) {
            return;
        }

        if ($scope.accLedgerForm.$valid || isValidate) {
            console.log($scope.accLedger);
            if ($scope.isUpdate === false) {
                AccLedgerService.SaveLedger($scope.accLedger);
                $route.reload();
            } else {
                AccLedgerService.UpdateLedger($scope.accLedger);
            }
  
      
            
        } else {
            toastr.error("Please fill all fields");
        }
    }
    $scope.loadAllLedger = function() {
        AccLedgerService.GetAllLedger().then(function(data) {
            $scope.LedgerList = data;
         
        });
    }
    $scope.editViewCalled = function (data) {      
        $scope.accLedger = data;
        $scope.isUpdate = true;
        $scope.button = "Update";
    }
    $scope.cancelUpdate = function() {
        $route.reload();
    }
   


});





