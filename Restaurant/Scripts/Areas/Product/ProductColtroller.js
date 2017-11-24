angular.module('myApp').controller('ProductController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter) {
    $scope.image = '../Images/no_img_available.jpg';
    $scope.SubCategory = {};
    $scope.addOrUpdate = 'Add New Product';
    $scope.LoadAll = function() {

        $scope.LoadCategoryDdl();
        // $scope.LoadSubCategoryDdl();
        // $scope.LoadAllSubCatagory();
    };

    $scope.LoadCategoryDdl = function() {
        $http.get('/Product/GetDdlCategory/').
            success(function(data) {
                if (data.success) {
                    $scope.CategoryList = data.result;
                } else {
                    toastr.error(data.errorMessage);
                }
            }).
            error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };

    $scope.LoadSubCategoryDdl = function(id) {
        if (id != 0 && id != undefined) {
            $http.get('/Product/GetDdlSubCategory/?id=' + id).
                success(function(data) {
                    if (data.success) {
                        $scope.SubCategoryList = data.result;
                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).
                error(function(XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
        }
    };

    $scope.SaveNewProduct = function(productImage) {
        $scope.Product.Imagestatus = '';
        var tempFile = [];
        if ($scope.newProductForm.$valid) {
            if (productImage != null) {
                tempFile[0] = productImage;
                $scope.Product.Imagestatus = 'y';
            }
            Upload.upload({
                method: 'POST',
                url: '/Product/SaveNewProduct/',
                file: tempFile,
                fields: $scope.Product,
                async: true
            }).success(function(data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.Product = {};
                    $scope.LoadAll();
                    $scope.ProductImage = '';
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        } else {
            toastr.error("Please fill up all fields");
        }
    };

    $scope.SaveSuggestedProduct = function (productImage) {
        $scope.Product.Imagestatus = '';
        var tempFile = [];
        if ($scope.suggestedProductForm.$valid) {
            if (productImage != null) {
                tempFile[0] = productImage;
                $scope.Product.Imagestatus = 'y';
            }
            Upload.upload({
                method: 'POST',
                url: '/Product/SaveSuggestedProduct/',
                file: tempFile,
                fields: $scope.Product,
                async: true
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $window.location.href = '#/SuggestedProducts';
                    $scope.Product = {};
                    $scope.LoadAll();
                    $scope.ProductImage = '';
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        } else {
            toastr.error("Please fill up all fields");
        }
    };

    $scope.Reset = function() {
        $scope.Product = {};
        $scope.LoadAll();
        $scope.ProductImage = '';
    };

    $scope.LoadAllProduct = function() {
        $scope.rowCollection = [];
        $scope.rowImages = [];
        $http.get('/Product/GetAllProduct/').
            success(function(data) {
                if (data.success) {

                    $scope.rowCollection = data.result;
                    $scope.displayedCollection = [].concat($scope.rowCollection);

                    //for (var j = 0; j < $scope.displayedCollection.length; j++) {

                    //    if ($scope.displayedCollection[j].ImageString != '') {
                    //        var imgByteCharacters = atob($scope.displayedCollection[j].ImageString);
                    //        var imgByteNumbers = new Array(imgByteCharacters.length);
                    //        for (var i = 0; i < imgByteCharacters.length; i++) {
                    //            imgByteNumbers[i] = imgByteCharacters.charCodeAt(i);
                    //        }
                    //        var imgByteArray = new Uint8Array(imgByteNumbers);
                    //        $scope.displayedCollection[j].Image = new Blob([imgByteArray], { type: 'image' });
                    //        // $scope.displayedCollection[j].Image = new File([$scope.displayedCollection[j].Image], '');
                    //        var img = $scope.displayedCollection[j].Name;
                    //        $scope.rowImages[$scope.displayedCollection[j].ProductId] = new Blob([imgByteArray], { type: 'image' });
                    //    }
                    //}

                } else {
                    toastr.error(data.errorMessage);
                }
            }).
            error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };

    $scope.LoadProductById = function() {
        $scope.LoadAll();
        $scope.addOrUpdate = 'Edit Product';
        $scope.Product = {};

        $http.get('/Product/GetProductById?id=' + $routeParams.id).
            success(function(data) {
                if (data.success) {
                    $scope.Product = data.result;
                    $scope.Product.Discount = parseInt($scope.Product.Discount);
                    //if (data.result.ImageString != '') {
                    //    var imgByteCharacters = atob(data.result.ImageString);
                    //    var imgByteNumbers = new Array(imgByteCharacters.length);
                    //    for (var i = 0; i < imgByteCharacters.length; i++) {
                    //        imgByteNumbers[i] = imgByteCharacters.charCodeAt(i);
                    //    }
                    //    var imgByteArray = new Uint8Array(imgByteNumbers);
                    //    $scope.ProductImage = new Blob([imgByteArray], { type: 'image' });
                    //}

                    $scope.LoadSubCategoryDdl(data.result.CategoryId);
                } else {
                    toastr.error(data.errorMessage);
                }
            }).
            error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
    };

    $scope.UpdateProduct = function(productImage) {
        $scope.Product.Imagestatus = '';
        var tempFile = [];
        if ($scope.editProductForm.$valid) {
            if (productImage != null) {
                tempFile[0] = productImage;
                $scope.Product.Imagestatus = 'y';
                $scope.Product.Image = null;
            }
            Upload.upload({
                method: 'POST',
                url: '/Product/UpdateProduct/',
                file: tempFile,
                fields: $scope.Product,
                async: true
            }).success(function(data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.Product = {};
                    $scope.LoadAll();
                    $scope.ProductImage = '';
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        } else {
            toastr.error("Please fill up all fields");
        }
    };

    $scope.ResetById = function() {
        $scope.LoadProductById();
    };

    $scope.ResetSuggestedProductId = function () {
        $scope.LoadSuggestedProductById();
    };

    $scope.LoadSuggestedProductList = function () {
        $http.get('/Product/LoadSuggestedProductList/').
        success(function (data) {
            if (data.success) {
                $scope.rowCollection = data.result;
                $scope.displayedCollection = [].concat($scope.rowCollection);
            }
            else {
                toastr.error(data.errorMessage);
            }
        }).
        error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    };

    $scope.LoadSuggestedProductById = function() {
        $scope.LoadAll();
        $scope.Product = {};
        $http.get('/Product/LoadSuggestedProductById?id=' + $routeParams.id).
           success(function (data) {
               if (data.success) {
                   $scope.Product = data.result;
                   //$scope.Product.Discount = parseInt($scope.Product.Discount);
                   //if (data.result.ImageString != '') {
                   //    var imgByteCharacters = atob(data.result.ImageString);
                   //    var imgByteNumbers = new Array(imgByteCharacters.length);
                   //    for (var i = 0; i < imgByteCharacters.length; i++) {
                   //        imgByteNumbers[i] = imgByteCharacters.charCodeAt(i);
                   //    }
                   //    var imgByteArray = new Uint8Array(imgByteNumbers);
                   //    $scope.ProductImage = new Blob([imgByteArray], { type: 'image' });
                   //}

                   $scope.LoadSubCategoryDdl(data.result.CategoryId);
               } else {
                   toastr.error(data.errorMessage);
               }
           }).
           error(function (XMLHttpRequest, textStatus, errorThrown) {
               toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
           });
    };

    $scope.openDeleteModal = function (size, tempUrl, name, ProductId) {
        $scope.Name = name;
        $scope.ProductId = ProductId;
        //$scope.Price = Price;
        //$scope.Discount = Discount;
        var animationList = ['bottom', 'right', 'news', 'sidefall', 'fall', 'sticktotop', '3Dfliphorizontal', '3Dflipvertical', '3Dsign', 'Superscaled', 'Justme'];
        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: tempUrl,
            controller: 'DeleteInstanceController',
            scope: $scope,

            backdrop: true, //disables modal closing by click on the background
            keyboard: true, //dialog box is closed by hitting ESC key
            windowClass: 'modal modal-slide-in-' + animationList[Math.floor(Math.random() * 11) + 0], //class that is added to styles the window template
            resolve: {
                items: function () {
                    return $scope.Name;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            //$scope.selected = selectedItem;
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    };

   
});

angular.module('myApp').controller('DeleteInstanceController', function ($scope, $uibModalInstance, $window, $http, $location, $routeParams, $filter, ngCart, items) {

    $scope.ok = function (ProductId) {
        $uibModalInstance.close($scope.data);
        $http.get('/Product/DeleteSuggestedProductById/?id=' + ProductId).
          success(function (data) {
              if (data.success) {
                  $scope.LoadSuggestedProductList();
                  toastr.success(data.successMessage);
                 
              } else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});