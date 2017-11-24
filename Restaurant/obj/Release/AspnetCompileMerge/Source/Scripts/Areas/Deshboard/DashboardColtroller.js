angular.module('myApp').controller('DashboardController', function ($scope, $window, $http,$uibModal, Upload, $location, $routeParams, $filter) {
   

    $scope.Login = function () {
        $scope.menu = true;
        $scope.pagename = true;
    };

    

    $scope.ForLoginSuccess = function () {
        $scope.menu = false;
        $scope.pagename = false;

    };


    $scope.init = function () {
        $scope.LoadModules();
    };
    $scope.LoadModules = function () {
        $scope.module = null;
        $http.get('/Account/LoadModules')
        .success(function (data) {
            $scope.module = (data.result);
            $scope.mapped_module = $scope.module;
        });
    };
    

    //$scope.ModuleClick = function (e) {
    //    e.preventDefault();

    //    $(this).parent().find('ul').slideToggle();
    //    angular.element($event.currentTarget).slideToggle();
    //};
});