angular.module('myApp').controller('EmployeeInformationController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route, fileUpload, $timeout) {
    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    $scope.myValue = true;

    $scope.GetAllDesignation = function () {
        $scope.designationList = [];
        $http.get('/EmployeeInformation/GetAllDesignation/').
          success(function (data) {
              if (data.success) {
                  $scope.designationList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };



    $scope.SaveEmployeeInformation = function () {
        if ($scope.EmployeeInformationAddForm.$valid) {
        var file = $scope.myFile;
        console.dir(file);
        console.log('file is ');

        var fd = new FormData();
        fd.append('file', file);
        fd.append('EmployeeName', $scope.Employee.EmployeeName);
        fd.append('EmployeeAddress', $scope.Employee.EmployeeAddress);
        fd.append('ContactNumber', $scope.Employee.ContactNumber);
        fd.append('EmployeeNid', $scope.Employee.EmployeeNid);
        fd.append('EmployeeEmail', $scope.Employee.EmployeeEmail);
        fd.append('DesignationId', $scope.Employee.DesignationId);
      
        console.log(fd);
        uploadUrl = "/EmployeeInformation/SaveEmployeeInformation";

        // Simple GET request example:
        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        
        }).success(function (data) {
            if (data.success) {
                toastr.success(data.successMessage);
                $scope.Employee = {};
                $route.reload();
            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
        }
        else {
            toastr.error("Please fill required fields");
        }
    };



    $scope.fileReaderSupported = window.FileReader != null;


    $scope.photoChanged = function (files) {
        $scope.myValue = false;
        $scope.myInsertValue = true;
        if (files != null) {
            var file = files[0];
            if ($scope.fileReaderSupported && file.type.indexOf('image') > -1) {
                $timeout(function () {
                    var fileReader = new FileReader();
                    fileReader.readAsDataURL(file);
                    fileReader.onload = function (e) {
                        $timeout(function () {
                            $scope.a = e.target.result;
                        });
                    }
                });
            }
        }
    };






    $scope.GetAllEmployeeInformation = function () {
        $scope.EditFormDiv = true;
        $scope.employeeInformationList = [];
        $http.get('/EmployeeInformation/GetAllEmployeeInformation/').
          success(function (data) {
              if (data.success) {
                  $scope.employeeInformationList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    $scope.DeleteEmployeeInformation = function (Id) {
        var deleteItem = $window.confirm('Are you absolutely sure you want to delete?');
        if (deleteItem) {
            $http.get('/EmployeeInformation/DeleteEmployeeInformation/?id=' + Id).
            success(function (data) {
                if (data.success) {
                    toastr.success(data.message);

                }
                else {
                    toastr.error(data.errorMessage);
                }
            }).
            error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });

            $route.reload();
        };
    };


       $scope.EditEmployeeInformation = function (row) {
        $scope.Employee = row;
        $scope.Employee.EmployeeImage = row.EmployeeImage;
        $scope.EditFormDiv = false;
        $scope.EmployeeInformationListDiv = true;

    };
    $scope.CancelEdit = function () {
        $scope.EditFormDiv = true;
        $scope.EmployeeInformationListDiv = false;
    }



    $scope.UpdateEmployeeInformation = function () {

        if ($scope.EmployeeInformationUpdateForm.$valid) {
            //var file = $scope.myFile;
            var file = $scope.Employee.EmployeeImage;

            console.log('file is ');

            var fd = new FormData();
            fd.append('file', file);
            fd.append('EmployeeId', $scope.Employee.EmployeeId);
            fd.append('EmployeeName', $scope.Employee.EmployeeName);
            fd.append('EmployeeAddress', $scope.Employee.EmployeeAddress);
            fd.append('ContactNumber', $scope.Employee.ContactNumber);
            fd.append('EmployeeNid', $scope.Employee.EmployeeNid);
            fd.append('EmployeeEmail', $scope.Employee.EmployeeEmail);
            fd.append('DesignationId', $scope.Employee.DesignationId);

            console.log(fd);
            uploadUrl = "/EmployeeInformation/UpdateEmployeeInformation";

            // Simple GET request example:
            $http.post(uploadUrl, fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.Employee = {};
                    $scope.EditFormDiv = true;
                    $scope.EmployeeInformationListDiv = false;
                    $route.reload();
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });

        }
        else {
            toastr.error("Please fill up required fields");
        }
    };




});