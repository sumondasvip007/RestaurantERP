angular.module('myApp').controller('RegisterController', function ($scope, $window, $http, $location, $routeParams, $filter) {
    $scope.LoadInitial = function () {
        $scope.Register = {};
        
    };

    $scope.CheckPassword = function () {
        if ($scope.Register.Password != $scope.Register.ConfirmPassword) {
            $scope.RegistrationForm.ConfirmPassword.$setValidity('mismatch', false)
        }
           // else if()
        else
        {
            toastr.success('Password Matched');
            $scope.RegistrationForm.ConfirmPassword.$setValidity('mismatch', true)
        }
    };

    $scope.Registration = function () {
        if ($scope.RegistrationForm.$valid) {
            $http({
                method: 'POST',
                url: '/Account/Register',
                data: $scope.Register
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.Register = {};
                }
                else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            })
        }
    }
})