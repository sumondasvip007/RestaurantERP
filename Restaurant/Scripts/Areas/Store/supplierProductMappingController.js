
angular.module('myApp').controller('supplierProductMappingController',
    function($scope, $window, $http, $uibModal, $location, $routeParams, $filter, $route) {
        $scope.itemsPerPage = [10, 15, 20, 25, 30];
        $scope.initialze = function() {
            $scope.getSupplier();
            $scope.supplierMappedEnable = false;
            $scope.productList = [];
        }

      
        

        $scope.getSupplier = function() {
            $http.get('/SupplierProductMapping/GetSupplierList')
                .success(function(data) {
                    if (data.success) {
                        $scope.supplier = data.result;
                        
                        //console.log($scope.supplier);
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
        }

        $scope.getProducts = function (SupplierId) {
            if (SupplierId != null) {
                $http.get('/SupplierProductMapping/GetPurchaseProductList/' + $scope.suppliersProductForm.SupplierId)
                    .success(function(data) {
                        if (data.success) {
                            $scope.product = data.result;
                            console.log($scope.product);
                            $scope.supplierMappedEnable = true;
                        } else {
                            toastr.error(data.errorMessage);
                        }
                    }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                        toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                    });
            } else {
                toastr.error("Select Supplier");
                $scope.supplierMappedEnable = false;
                $scope.product = null;
            }
        }

        $scope.selectProduct = function (p) {
            
            var productIndex = $scope.product.indexOf(p);
            if (productIndex == -1) {
                $scope.product.push(p); //Add the selected host into array
            } else {
                $scope.product.splice(productIndex, 1); //Remove the selected host
                $scope.product.push(p);
            }
        }


        $scope.Submit = function()
        {
            $http({
                method: 'POST',
                url: '/SupplierProductMapping/AddSupplierProductList/',
                data: $scope.product
            }).success(function (data) {
                if (data.success) {
                    $route.reload();
                    toastr.success(data.successMessage);
                    $scope.successMessage = data.successMessage;
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }

    });

