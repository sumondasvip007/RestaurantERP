angular.module('myApp').controller('AspNetUserController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    $scope.CheckPassword = function () {
        if ($scope.AspNetUser.Password != $scope.AspNetUser.ConfirmPassword) {
            $scope.AspNetUserForm.ConfirmPassword.$setValidity('mismatch', false);
        }
            // else if()
        else {
            //toastr.success('Password Matched');
            $scope.AspNetUserForm.ConfirmPassword.$setValidity('mismatch', true);
        }
    };

    $scope.SaveUser = function () {
        if ($scope.AspNetUserForm.$valid) {
            $http({
                method: 'POST',
                url: '/Account/Register/',
                data: $scope.AspNetUser
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.AspNetUser = {};
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("Please fill up all fields");
        }
    };


    $scope.AllUserList = function () {
        $scope.UserEditHideDiv = true;
        $scope.UserList = [];
        $http.get('/Account/GetAllUser/').
          success(function (data) {
              if (data.success) {
                  $scope.UserList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    $scope.DeleteUser = function (Id) {
        var deleteItem = $window.confirm('Are you absolutely sure you want to delete?');
        if (deleteItem) {
            $http.get('/Account/DeleteUser/?id=' + Id).
                success(function(data) {
                    if (data.success) {
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).
                error(function(XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
            $route.reload();
        };
    };
    
    $scope.EditUser = function (row) {
        $scope.AspNetUser = row;
        $scope.UserEditHideDiv = false;
        $scope.UserListHideDiv = true;
        
    };
    $scope.CancelEdit = function () {
        $route.reload();
        $scope.UserEditHideDiv = true;
        $scope.UserListHideDiv = false;
    }
    
    $scope.UpdateUser = function () {
        
        if ($scope.AspNetUserForm.$valid) {
            $http({
                method: 'POST',
                url: '/Account/UpdateUser/',
                data: $scope.AspNetUser
            }).success(function (data) {
                if (data.success) {
                    $route.reload();
                    toastr.success(data.successMessage);
                    $scope.UserEditHideDiv = true;
                    $scope.UserListHideDiv = false;
                    $scope.AspNetUser = {};
                } else {
                    toastr.error(data.ErrorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
           
        }
        else {
            toastr.error("Please fill up all fields");
        }
    };

});