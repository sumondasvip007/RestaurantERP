angular.module('myApp').controller('showAllModule', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    //################# SHOW ALL DATA ON TABLE ##########################
    $scope.dataload = function () {
  
        $http.get('/Module/GetAllMoudle')
            .success(function (data) {
                if (data.success) {
                    console.log("hello");
                    $scope.GetAllModule = data.result;
                } else {
                    toastr.error(data.errorMessage);
                }
            })
            .error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };
    //################# EDIT IN TABLE ##########################
    //1.Pass to the View
    $scope.edit = function (id) {
        $window.location.href = "#/EditModule/" + id;
    }
    //---- DELETE -----
    //delete module 
    $scope.delete = function (id) {

        $http({
            method: 'POST',
            url: '/Module/DeleteModulebyId',
            data: { id: id }
        }).success(function (data) {
            if (data.success) {
                $route.reload();
                toastr.success(data.successMessage);


            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    }


});
