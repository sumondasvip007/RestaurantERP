

angular.module('myApp').controller('showSuppierController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {
    //####### PagiNation ##############
    $scope.itemsPerPage = [10, 15, 20, 25, 30];

    
    //########### GET ALL Supplier  ####################
    $scope.GetAllSupplier = function () {
       
        $http.get('/SupplierInformation/GetAllSuppier')
            .success(function (data) {
                if (data.success) {           
                    $scope.ShowAllSupplier = data.result;
                    
                } else {
                    toastr.error(data.errorMessage);
                }
            })
            .error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };

    //########### Delete   Supplier By Id   ####################
    $scope.delete = function (id) {

    

      
            //IF CLICK YES THEN MESSEGE WILL BE HERE 
            $http({
                method: 'POST',
                url: '/SupplierInformation/DeleteSuppierById',
                data: { id: id }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $route.reload();
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });

       

     
    }

    //########### EDIT   ####################
    $scope.edit = function (id) {

      


            $window.location.href = "#/EditSupplierInformation/"+id;
      

    }
   





});