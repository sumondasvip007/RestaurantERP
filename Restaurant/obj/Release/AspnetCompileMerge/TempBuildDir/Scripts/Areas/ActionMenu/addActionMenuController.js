
angular.module('myApp').controller('addActionMenuController', function ($scope, $http) {

    $scope.initialize = function () {
        $scope.getModules();
    }


     

    $scope.addAction = function () {

        if ($scope.ActionForm.$invalid) {
            toastr.error("Please enter All Field ");
            return;
        }

        $scope.data = {
            name: $scope.name,
            url: $scope.url,
            display_name: $scope.display_name,
            ui_module_id: $scope.ui_module_id,
            is_view: $scope.is_view,
            is_in_menu: $scope.is_in_menu
        };

        $http({
            method: 'POST',
            url: '/MenuAction/AddActionToDb',
            data: $scope.data
        }).success(function(data) {
            if (data.success) {
                toastr.success(data.successMessage);
                clearField();
            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function(XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    }

    function clearField() {
        $scope.display_name = '',
        $scope.name = '',
         $scope.ui_module_id = '',
          $scope.is_view = '',
         $scope.is_in_menu = ''
    }
    $scope.getModules = function () {
     
        $http.get('/MenuAction/GetModules')
       .success(function (data) {
           if (data.success) {
               $scope.modules = data.result;
           }
           else {
               toastr.error(data.errorMessage);
           }
       }).
       error(function (XMLHttpRequest, textStatus, errorThrown) {
           toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
       });
    }

})