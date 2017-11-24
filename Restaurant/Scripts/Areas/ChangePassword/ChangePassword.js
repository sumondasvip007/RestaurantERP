angular.module('myApp').controller('ChangePassword', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route, ChangePasswordService) {


    
   $scope.ChangePassword = function() {
        var isformValidate = ChangePasswordService.formValidation($scope);
        if (isformValidate === false)
            return;
        if ($scope.password) {
            var confirmPassword = ChangePasswordService.confirmPassword($scope.password.newPassword, $scope.password.confirmPassword);
        }
        
        if (isformValidate === true && confirmPassword === true) {
            ChangePasswordService.ChangePassword($scope.password);
        }
      
   }


});