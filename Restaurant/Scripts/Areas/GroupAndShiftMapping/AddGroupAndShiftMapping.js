angular.module('myApp').controller('AddGroupAndShiftMappingController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];


    $scope.getGroups = function () {
        $http.get('/GroupAndShiftMapping/GetAllGroup')
            .success(function (data) {
                if (data.success) {
                    $scope.groupList = data.result;

                } else {
                    toastr.error(data.errorMessage);
                }
            })
            .error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    }
    $scope.getShifts = function () {
        $http.get('/GroupAndShiftMapping/GetAllShift')
            .success(function (data) {
                if (data.success) {
                    $scope.shiftList = data.result;

                } else {
                    toastr.error(data.errorMessage);
                }
            })
            .error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    }
    $scope.AddGroupAndShiftMapping = function () {

        if ($scope.AddGroupAndShiftMappingForm.$valid) {
            $http({
                method: 'POST',
                url: '/GroupAndShiftMapping/AddGroupAndShiftMapping/',
                data: { groupAndShiftMapping: $scope.GroupAndShiftMapping, fromDate: $scope.FromDate }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    //$scope.OtherExpense = {};
                    $route.reload();
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
        else {
            toastr.error("Please fill up all required fields");
        }

    };

});