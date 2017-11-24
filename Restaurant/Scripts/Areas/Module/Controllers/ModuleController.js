angular.module('myApp').controller('demoController', function ($scope, $window, $http,$uibModal, Upload, $location, $routeParams, $filter,$route) {
    $scope.names = [5,10, 15, 20, 25, 30];
    $scope.buttonName = "Save";

    $scope.submit = function () {

        //if module form valid then run it 
        if ($scope.moduleForm.$valid) {
            //edit save change
            if ($scope.module.module_id) {

                $http({
                    method: 'POST',
                    url: '/Module/EditMoudleById',
                    data: $scope.module
                }).success(function(data) {
                    if (data.success) {
                        $route.reload();
                        toastr.success(data.successMessage);


                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
            } //if end 
            //else start
            else {
                //pass data to controller to save  
                $http({
                    method: 'POST',
                    url: '/Module/AddModule/',
                    data: $scope.module
                }).success(function(data) {
                    if (data.success) {
                        $route.reload();
                        toastr.success(data.successMessage);
                        $scope.successMessage = data.successMessage;
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
            }
        } else {
            toastr.error("Please Fill Up All The Fields");
        }
    }//submit close 


   

    //edit module show data
    if ($routeParams.id) {
        $http({
            method: 'POST',
            url: '/Module/GetModuleById',
            data: { id: $routeParams.id }
        }).then(function successCallback(response) {
            console.log(response.data.result);
            $scope.module = response.data.result;
            $scope.buttonName = "Update";

        }, function errorCallback(response) {
        });
    }
   



});

