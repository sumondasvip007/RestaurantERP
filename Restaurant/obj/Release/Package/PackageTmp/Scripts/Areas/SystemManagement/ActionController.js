angular.module('myApp').controller('ActionController', function ($scope, $window, $http, $location, $routeParams, $filter, dateconfigservice, isundefinedornullservice, globalvalueservice) {
    $scope.GetActionsList = function () {
        $scope.rowCollection = [];
        $http.get('/SystemManagement/Action/GetActionsList/').
            success(function (data) {
                $scope.rowCollection = data.result;
                $scope.displayCollection = [].concat($scope.rowCollection);

            }).
            error(function (data) {
                toastr.error(data.errorMessage);
            });
    };

    $scope.GetReportsList = function () {
        $scope.rowCollection = [];
        $http.get('/SystemManagement/Action/GetReportsList/').
            success(function (data) {
                $scope.rowCollection = data.result;
                $scope.displayCollection = [].concat($scope.rowCollection);

            }).
            error(function (data) {
                toastr.error(data.errorMessage);
            });
    };

    $scope.LoadNewAction = function () {
        $scope.action = {};
    };
    $scope.GetDdlModule = function () {
        $scope.LoadNewAction();
        $http.get('/SystemManagement/Action/GetDdlModule/').
            success(function (data) {
                $scope.ModuleList = data.result;
            })
            .error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };

    $scope.GetDdlModuleForReport = function () {
        $scope.LoadNewAction();
        $http.get('/SystemManagement/Action/GetDdlModuleForReport/').
            success(function (data) {
                $scope.ModuleList = data.result;
            })
            .error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };

    $scope.GetDdlController = function (module_id) {
        
        $http.get('/SystemManagement/Action/GetDdlController/?module_id=' + module_id).
            success(function (data) {
                $scope.ControllerList = data.result;
            })
            .error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };
    $scope.AddNewAction = function () {
        if ($scope.AddNewActionForm.$valid) {
            $http({
                method: 'POST',
                url: '/SystemManagement/Action/AddNewAction/',
                data: $scope.action
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    $scope.action = {};

                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
    };
    $scope.AddNewReport = function () {
        if ($scope.AddNewActionForm.$valid) {
            $http({
                method: 'POST',
                url: '/SystemManagement/Action/AddNewReport/',
                data: $scope.action
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    $scope.action = {};

                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
    };

    $scope.GetActionById = function () {
        $http.get('/SystemManagement/Action/GetActionById/' + $routeParams.id)
            .success(function (data) {
                if (data.success) {
                    $scope.editAction = data.result;
                    $scope.GetDdlController($scope.editAction.ui_module_id);
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };
    $scope.UpdateAction = function () {
        if ($scope.EditActionForm.$valid) {
            $http({
                method: 'POST',
                url: '/SystemManagement/Action/UpdateAction/',
                data: $scope.editAction
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.message);

                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
    };

    $scope.UpdateReport = function () {
        if ($scope.EditActionForm.$valid) {
            $http({
                method: 'POST',
                url: '/SystemManagement/Action/UpdateReport/',
                data: $scope.editAction
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.message);

                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }
    };

})