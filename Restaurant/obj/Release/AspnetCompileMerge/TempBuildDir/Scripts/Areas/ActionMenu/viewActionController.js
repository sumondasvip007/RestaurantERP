

angular.module('myApp').controller('viewActionController', function ($scope, $http) {
    $scope.itemsPerPage = [10, 15, 20, 25, 30];
    $scope.initialize = function () {
        $scope.getAction();
    }

    $scope.getAction = function () {

        
        $scope.rowCollection = [];
        $http.get('/MenuAction/ViewAllAction/')
        .success(function (data) {
        if (data.success) {
            $scope.rowCollection = data.result;
        }
        else {
            toastr.error(data.errorMessage);
        }
        }).
            error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    }

    $scope.delete = function (id) {



        $scope.rowCollection = [];
        $http.get('/MenuAction/DeleteAction/'+id)
        .success(function (data) {
            if (data.success) {
                toastr.success(data.successMessage);
                $scope.getAction();
            }
            else {
                toastr.error(data.errorMessage);
            }
        }).
            error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    }


    $scope.makeYesNo = function (x) {
        if (x) {
            return 'Yes';
        } else {
            return 'No';
        }
    }

});