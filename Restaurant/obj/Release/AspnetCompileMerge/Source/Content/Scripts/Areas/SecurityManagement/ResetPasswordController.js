angular.module('myApp').controller('ResetPasswordController', function ($scope, $window, $http, $location, $routeParams, $filter, dateconfigservice, isundefinedornullservice, globalvalueservice) {
    $scope.LoadInitial = function () {
        $scope.ResetPassword = {};
        $scope.GetUserName();
    };
    $scope.CheckPassword = function () {
        if ($scope.ResetPassword.NewPassword != $scope.ResetPassword.ConfirmPassword) {
            $scope.ResetPasswordForm.ConfirmPassword.$setValidity('mismatch', false)

        }
            // else if()
        else {
            toastr.success('Password Matched');
            $scope.ResetPasswordForm.ConfirmPassword.$setValidity('mismatch', true);
        }
    };

    $scope.GetUserName = function () {
        $http.get('/Account/GetUserName?userId=' + $routeParams.id)
        .success(function (data) {
            if (data.success) {
                $scope.ResetPassword.UserName = data.result;
                $scope.ResetPassword.UserId = $routeParams.id;
            }
            else {
                toastr.error(data.errorMessage);
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        })
    };

    $scope.ResetUserPassword = function () {
        if ($scope.ResetPasswordForm.$valid) {
            $http({
                method: 'POST',
                url: '/Account/ResetPassword',
                data: $scope.ResetPassword,
                headers: {
                    'RequestVerificationToken': $('#token').val(),
                    //    'X-Requested-With': 'XMLHttpRequest'
                }
                //}
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $location.path('/UserList');
                }
                else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            })
        }
    }
});