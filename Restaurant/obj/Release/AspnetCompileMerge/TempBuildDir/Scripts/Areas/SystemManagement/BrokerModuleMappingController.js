angular.module('myApp').controller('BrokerModuleMappingController', function($scope, $window, $http, $location, $routeParams, $filter, dateconfigservice, isundefinedornullservice, globalvalueservice) {
    $scope.getDdlBroker = function() {

        $http.get('/SystemManagement/BrokerModuleMapping/getDdlBroker').
            success(function(data) {
                $scope.brokerList = data.result;
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };
    $scope.GetModuleListByBroker = function(id) {
        
        $scope.rowCollection = [];
        $http.get('/SystemManagement/BrokerModuleMapping/GetModuleListByBroker/' + id).
            success(function(data) {
                $scope.rowCollection = data.result;
                $scope.displayCollection = [].concat($scope.rowCollection);
                //isAllSelected();
            }).
            error(function(data) {
                toastr.error(data.errorMessage);
            });
    };

    $scope.SaveBrokerModuleMapping = function() {
        var selectedMenu = [];
        for (var i = 0; i < $scope.displayCollection.length; i++) {
            if ($scope.displayCollection[i].selected) {
                selectedMenu.push($scope.displayCollection[i].id);
            }
        }
        $http({
            method: 'POST',
            url: '/SystemManagement/BrokerModuleMapping/SaveBrokerModuleMapping',
            data: { id: selectedMenu, membership_id: $scope.BrokerModuleMapping.membership_id }
        }).success(function (data) {
            if (data.success) {
                toastr.success("Submitted Successfully");

            }
            else {
                toastr.error(data.errorMessage);
            }
        });
    }

    $scope.selectAll = function(displayedCollection) {
        if ($scope.allSelect === true) {
            for (var i = 0; i < displayedCollection.length; i++) {
                displayedCollection[i].selected = true;
            }
        } else {
            for (var i = 0; i < displayedCollection.length; i++) {
                displayedCollection[i].selected = false;
            }
        }

    };
    $scope.changeSelectAll = function(selected) {
        if (!selected) {
            $scope.allSelect = false;
        }
        else {
            isAllSelected();
        }
    };

    function isAllSelected() {
        //if ($scope.displayedCollection.length === 0) {
        //    var selectAll = false;
        //}
        var selectAll = true;
        var goLoop = true;
        for (var i = 0; i < $scope.displayedCollection.length; i++) {
            if ($scope.displayedCollection[i].selected === false) {
                goLoop = false;
                selectAll = false;
            }
        }
        $scope.allSelect = selectAll;
    }
});