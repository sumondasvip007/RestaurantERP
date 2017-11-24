
var stuff = [{
    label: 'Stuff',
    children: [{
        label: 'Hats',
        children: [
          { label: 'Flat cap' },
          { label: 'Fedora' },
          { label: 'Baseball' },
          { label: 'Top hat' },
          { label: 'Gatsby' }
        ]
    }, {
        label: 'Pens',
        selected: true,
        children: [
          { label: 'Fountain' },
          { label: 'Gel ink' },
          { label: 'Roller ball' },
          { label: 'Fiber tip' },
          { label: 'Ballpoint' }
        ]
    }, {
        label: 'Whiskey',
        children: [
          { label: 'Irish' },
          { label: 'Scotch' },
          { label: 'Rye' },
          { label: 'Tennessee' },
          { label: 'Bourbon' }
        ]
    }]
}];

angular.module('myApp').controller('UserPermission', function($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {
    //todo GET ALL USER DATA
    this.stuff0 = stuff;

    $scope.userOnChange = function (a) {

        this.data = "mosaddik";
    }




 
    $scope.GetAllAspNetUser = function() {
        console.log();
        $scope.rowCollection = [];
        $http.get('/UserPermissions/GetAllAspNetUsers')
            .success(function(data) {

                if (data.success) {
                    console.log(data);
                    $scope.displayCollection = data.result;

                } else {
                    toastr.error(data.errorMessage);
                    console.log(data);
                }
            })
            .error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };

});






