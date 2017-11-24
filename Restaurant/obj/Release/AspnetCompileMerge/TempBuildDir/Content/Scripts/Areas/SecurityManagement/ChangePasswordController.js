angular.module('myApp').controller('ChangePasswordController', function ($scope, $window, $http, $location, $routeParams, $filter, dateconfigservice, isundefinedornullservice, globalvalueservice) {
    $scope.LoadInitial = function () {
        $scope.ChangePassword = {};
    };
    $scope.CheckPassword = function () {
        if ($scope.ChangePassword.NewPassword != $scope.ChangePassword.ConfirmPassword) {
            $scope.ChangePasswordForm.ConfirmPassword.$setValidity('mismatch', false)
            
        }
            // else if()
        else {
            toastr.success('Password Matched');
            $scope.ChangePasswordForm.ConfirmPassword.$setValidity('mismatch', true);
        }
    };
    $scope.ChangeUserPassword = function ()
    {
        if ($scope.ChangePasswordForm.$valid) {
            $http({
                method: 'POST',
                url: '/Account/ChangePassword',
                data: $scope.ChangePassword
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.ChangePassword = {};

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