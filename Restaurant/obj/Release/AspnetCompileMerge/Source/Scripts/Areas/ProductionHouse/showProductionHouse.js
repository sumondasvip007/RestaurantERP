angular.module('myApp').controller('showProductionHouse', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    $scope.itemsPerPage = [10, 15, 20, 25, 30];


    //################# SHOW ALL DATA ON TABLE ##########################
    $scope.GetAllProductionHouse = function () {
        $http.get('/ProductionHouse/GetAllProductionHouse')
            .success(function (data) {
              
                if (data.success) {
                    $scope.ProductionHouse = data.result;
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
    $scope.edit = function(id) {
        $window.location.href = "#/EditProductionHouse/"+id;
    }

    
});

