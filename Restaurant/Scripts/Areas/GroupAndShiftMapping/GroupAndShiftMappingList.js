angular.module('myApp').controller('GroupAndShiftMappingController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];

    $scope.EditGroupAndShiftMappingDiv = true;
    $scope.GroupAndShiftMappingListDiv = false;


    $scope.GetAllGroupAndShiftMapping = function () {
        //$scope.EditSellsPointDiv = true;
        $scope.groupAndShiftMappingList = [];
        $http.get('/GroupAndShiftMapping/GetAllGroupAndShiftMapping/').
          success(function (data) {
              if (data.success) {
                  $scope.groupAndShiftMappingList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

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


    $scope.DeleteGroupAndShiftMapping = function (Id) {
        
        $http.get('/GroupAndShiftMapping/DeleteGroupAndShiftMapping/?id=' + Id).
            success(function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    $route.reload();
                }
                else {
                    toastr.error(data.errorMessage);
                }
            }).
            error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
            
       
    };
    $scope.EditGroupAndShiftMapping = function (row) {
        $scope.GroupAndShiftMapping = row;
        if ($scope.GroupAndShiftMapping.Day === "A") {
            $scope.GroupAndShiftMapping.GroupId = 1;
            $scope.GroupAndShiftMapping.ShiftId = 1;
            $scope.FromDate = $scope.GroupAndShiftMapping.Date;
        } else {
            $scope.GroupAndShiftMapping.GroupId = 2;
            $scope.GroupAndShiftMapping.ShiftId = 1;
            $scope.FromDate = $scope.GroupAndShiftMapping.Date;
        }
        
       
        $scope.EditGroupAndShiftMappingDiv = false;
        $scope.GroupAndShiftMappingListDiv = true;
    };
    $scope.CancelEdit = function () {
        $scope.EditGroupAndShiftMappingDiv = true;
        $scope.GroupAndShiftMappingListDiv = false;
    }

    $scope.UpdateGroupAndShiftMapping = function () {

        if ($scope.GroupAndShiftMappingForm.$valid) {
            $http({
                method: 'POST',
                url: '/GroupAndShiftMapping/UpdateGroupAndShiftMapping/',
                data: { groupAndShiftMapping: $scope.GroupAndShiftMapping, fromDate: $scope.FromDate }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.EditGroupAndShiftMappingDiv = true;
                    $scope.GroupAndShiftMappingListDiv = false;
                    $route.reload();

                } else {

                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
            //$scope.EditGroupAndShiftMappingDiv = true;
            //$scope.GroupAndShiftMappingListDiv = false;
            //$route.reload();
        }
        else {
            toastr.error("Please fill up all fields");
            $scope.EditGroupAndShiftMappingDiv = false;
            $scope.GroupAndShiftMappingListDiv = true;
        }
    };
});