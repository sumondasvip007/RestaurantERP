angular.module("myApp").factory('ChangePasswordService', function($http, $route,$window) {
    return {
        formValidation: function ($scope) {
            if ($scope.ChangePasswordForm.oldPassword.$invalid) {
                toastr.error("Enter old password");
                return false;
            }
            if ($scope.ChangePasswordForm.newPassword.$invalid) {
                toastr.error("Enter new  password");
                return false;
            }
            if ($scope.ChangePasswordForm.confirmPassword.$invalid) {
                toastr.error("Enter confirm  password");
                return false;
            }
            return true;
        },
        CheckOldPassword:function(oldPasswrod) {
            var promise = $http({
                method: 'POST',
                url: 'url',
                data: {password : oldPasswrod}
            }).success(function (data) {
                if (data.success) {
                    return data.result;
                } else {
                    toastr.error("Please enter valid password");
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
            return promise;
        },
        confirmPassword: function (newPassword, confirmPassword) {
            var isPasswordConfirm = false;
            if (newPassword === confirmPassword) {
                isPasswordConfirm = true;
            } else {
                toastr.error("New passwrod and confirm passwrod  doesnot match");
            }
     
            return isPasswordConfirm;
        },
        ChangePassword: function (password) {
            var acountViewModel  = {
                OldPassword: password.oldPassword,
                NewPassword: password.newPassword,
                ConfirmPassword: password.confirmPassword
            }
            var promise = $http({
                method: 'POST',
                url: '/Account/Manage/',
                data: { model: acountViewModel }
            }).success(function (data) {
                if (data.success) {

                    toastr.success("Password Change Successfully");
                    $window.location.href = "#/";

                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });

            
        }
        

        
    }

});