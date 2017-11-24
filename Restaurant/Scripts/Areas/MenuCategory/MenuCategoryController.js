
var myapp = angular.module('myApp').controller('MenuCategoryController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, ngCart) {

    //$scope.ngCart = ngCart;
    //$scope.image = '../Images/no_img_available.jpg';
    //$scope.ShowLeftMenuAndSlider = true;
    //$scope.SmalProductGrid = true;
    //$scope.showProdcutGrid = true;
    //$scope.addNewProduct = true;
    //$scope.SearchOrAll = "All Products";
    //$scope.showShoppingCartDetails = false;
    //$scope.Products = true;
    //$scope.Product = [];
    //$scope.Product.NewProductId = 1;
    //$scope.Product.NewProductPrice = 0;
    //$scope.Order = [];
    //$scope.OrderStatus = "Customer Information";
    //$scope.form = true;
    //$scope.Order.ShipmentDate = new Date(new Date().getTime() + (1 * 24 * 60 * 60 * 1000));
   
    var url = $location.path();

    if (url == '/Login') {
        $scope.ShowLeftMenuAndSlider = false;
    } else {
        $scope.ShowLeftMenuAndSlider = true;
    }

    //Paging Code start from here
    $scope.itemsByPage = 28;
    //End of pagination

    var flag = 1;
    $scope.IncrementProductId = function() {
        if ($scope.newProductForm.$valid) {
            $scope.Product.NewProductId += 1;
            $scope.Product = [];
        }
    };

    $scope.resetNewProduct = function() {
        $scope.Product = [];
    };

   // Product units
    //$scope.UnitList = ['kg', 'mg', 'g', 'pound', 'dozen', 'box', 'Pcs', 'L', 'ml'];
    //$scope.CityList = ['Dhaka', 'Rajshahi', 'Khulna', 'Rangpur', 'Chitagong', 'Barishal', 'Jessore'];

    $scope.LoadAll = function () {
        // $scope.generateAll();
        $http.get('/Menu/GetCategoryWithSubCategory/').
            success(function (data) {
                if (data.success) {
                    $scope.Category = data.result;
                    $scope.ServiceAreas = data.ServiceAreas;
                    
                } else {
                    toastr.error(data.errorMessage);
                }
            }).
            error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });

        $scope.ngCart = ngCart;
        $scope.image = '../Images/no_img_available.jpg';
        //$scope.ShowLeftMenuAndSlider = true;
        $scope.SmalProductGrid = true;
        $scope.showProdcutGrid = true;
        $scope.addNewProduct = true;
        $scope.SearchOrAll = "All Products";
        $scope.showShoppingCartDetails = false;
        $scope.Products = true;
        $scope.Product = [];
        $scope.Product.NewProductId = 1;
        $scope.Product.NewProductPrice = 0;
        $scope.Order = [];
        $scope.OrderStatus = "Customer Information";
        $scope.form = true;
        $scope.Order.ShipmentDate = new Date(new Date().getTime() + (1 * 24 * 60 * 60 * 1000));
        var time = $filter('date')(new Date(), 'HH');
        if (time >= 22) {
            $scope.Order.ShipmentDate = new Date(new Date().getTime() + (2 * 24 * 60 * 60 * 1000));
        }
        $scope.sliderDiv = true;
        //$scope.msg = "Hi Angular buddy";
        //$scope.desc = "this is from desc";
    };

    $scope.ClearLeftMenuAndSlider = function() {
        $scope.ShowLeftMenuAndSlider = false;
        $scope.sliderDiv = false;
    };

    $scope.ShowLeftMenu = function() {
        $scope.ShowLeftMenuAndSlider = true;
    };

    $scope.LoadProductForView = function() {
        $scope.rowCollection = [];
        $scope.rowImages = [];
        $http.get('/Product/GetAllProductForView/').
            success(function(data) {
                if (data.success) {
                    $scope.rowCollection = data.result;
                    $scope.ProductGrid = [].concat($scope.rowCollection);

                    //for (var j = 0; j < $scope.ProductGrid.length; j++) {

                    //    if ($scope.ProductGrid[j].ImageString != '') {
                    //        var imgByteCharacters = atob($scope.ProductGrid[j].ImageString);
                    //        var imgByteNumbers = new Array(imgByteCharacters.length);
                    //        for (var i = 0; i < imgByteCharacters.length; i++) {
                    //            imgByteNumbers[i] = imgByteCharacters.charCodeAt(i);
                    //        }
                    //        var imgByteArray = new Uint8Array(imgByteNumbers);
                    //        $scope.ProductGrid[j].Image = new Blob([imgByteArray], { type: 'image' });
                    //        // $scope.displayedCollection[j].Image = new File([$scope.displayedCollection[j].Image], '');
                    //        var img = $scope.ProductGrid[j].Name;
                    //        $scope.rowImages[$scope.ProductGrid[j].ProductId] = new Blob([imgByteArray], { type: 'image' });
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

    $scope.GetProductsForViewBySubCategory = function (subCategoryId) {

        $scope.sliderDiv = false;
        $scope.orderList = "SubCategoryId";
        //$scope.showProdcutGrid = true;
        //$scope.SmalProductGrid = false;
        $scope.CustomerInfo = false;
        $scope.showShoppingCartDetails = false;
        $scope.Products = true;
         flag = 1;
         if (subCategoryId != '') {
            $scope.query = '';
            $scope.filterByCategory = '';
            $scope.filterBySubCategory = subCategoryId //'{SubCategory : ' + subCategoryId + '}';
            $scope.SearchOrAll = "Search Results";
            $scope.addNewProduct = false;
            $scope.labelOfNewProduct = true;
         } else {
            $scope.query = '';
            $scope.filterBySubCategory = '';
            $scope.SearchOrAll = "All Products";
            $scope.addNewProduct = true;
            $scope.labelOfNewProduct = false;
        }
        //$scope.addNewProduct = false;
    };


    

    $scope.GetProductsForViewByCategory = function (CategoryId) {
        $scope.sliderDiv = false;
        $scope.orderList = "CategoryId";
        //$scope.showProdcutGrid = true;
        //$scope.SmalProductGrid = false;
        $scope.CustomerInfo = false;
        $scope.showShoppingCartDetails = false;
        $scope.Products = true;
        flag = 1;
        if (CategoryId != '') {
            $scope.query = '';
            $scope.filterBySubCategory = ''; //'{SubCategory : ' + subCategoryId + '}';
            $scope.filterByCategory = CategoryId;
            $scope.SearchOrAll = "Search Results";
            $scope.addNewProduct = false;
            $scope.labelOfNewProduct = true;
        } else {
            $scope.query = '';
            $scope.filterBySubCategory = '';
            $scope.filterByCategory = '';
            $scope.SearchOrAll = "All Products";
            $scope.addNewProduct = true;
            $scope.labelOfNewProduct = false;
        }
        //$scope.addNewProduct = false;
    };

    $scope.SearchResult = function (string) {
        $scope.sliderDiv = false;
        $scope.filterBySubCategory = '';
        $scope.filterByCategory = '';
        $scope.query = string;

        if (string == "" || string == undefined) {
            $scope.SearchOrAll = "All Products";
            $scope.addNewProduct = true;
            $scope.labelOfNewProduct = false;
            $scope.itemsByPage = 28;
            
        }
        else {
            $scope.SearchOrAll = "Search Results";
            $scope.addNewProduct = false;
            $scope.labelOfNewProduct = true;
            $scope.showShoppingCartDetails = false;
            //$scope.addNewProduct = true;
            $scope.Products = true;
            flag = 1;
            $scope.itemsByPage = 50;
        }
    };
   
    $scope.ShowShoppigCart = function (showShoppingCartDetails, addNewProduct, Products)
    {
        $scope.sliderDiv = false;
        $scope.OrderStatus = "Customer Info";
        $scope.successStatus = false;
        $scope.form = true;

        if (flag == 0) {
            $scope.showShoppingCartDetails = false ;
            $scope.addNewProduct = true;
            $scope.Products = true;
            flag = 1;
            $scope.CustomerInfo = false;
        } else {
            $scope.showShoppingCartDetails = true;
            $scope.addNewProduct = false;
            $scope.Products = false;
            flag = 0;
            $scope.query = '';
            $scope.CustomerInfo = false;
        }
    };

    $scope.Checkout = function() {
        $scope.CustomerInfo = true;
        $scope.showShoppingCartDetails = false;
        $scope.addNewProduct = false;
        $scope.Products = false;

        $scope.focus = function() {
           // $("#CustomerName").focus();
            $scope.focusme = true;
        };
    };

    $scope.back = function ()
    {
        $scope.showShoppingCartDetails = true;
        $scope.CustomerInfo = false;
        $scope.addNewProduct = false;
        $scope.Products = false;
    };


    $scope.generate = function (type, text) {
        var n = noty({
            text: text,
            type: type,
            dismissQueue: true,
            layout: 'topRight',
            closeWith: ['click'],
            theme: 'relax',
            maxVisible: 1,
            animation: {
                open: 'animated swing',
                close: 'animated tada',
                easing: 'swing',
                speed: 100
            }
        });
       // console.log('html: ' + n.options.id);

        setTimeout(function () {
          
            n.close();
            setTimeout(function () {
                $scope.generateAll();
            }, 100000);
        }, 100000);

        return n;
    };

    $scope.generateAll = function () {
        //toastr(id);
        //alert(id);
       // var v = $scope.rowImages[id];
        $scope.generate('warning', '<div class="activity-item" ng-controller="MenuCategoryController"><img style="margin-top:-15px; height: 30px; weight: 30px" src="Images/bag.png"></img></i> <div style="margin-left: 50px;margin-top: -35px; font-size:16px"  class="activity">Call <b style="color:#169b62; font-size:16px;">01939515753 </b>for order</div> </div>');
        // generate('error', notification_html[1]);
        //generate('information', notification_html[2]);
        // generate('success', notification_html[3]);
        //            generate('notification');
        //            generate('success');
    };

    $scope.open = function ($event, opened) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope[opened] = true;
    };

    $scope.placeOrder = function () {
        var cart = $scope.ngCart.$cart.items;
        var customerinfo = $scope.Customer;
        customerinfo.City = $scope.city.ServiceAreaName.ServiceAreaName;
       // var orderinfo = [];
        var ShipmentDate = $filter('date')($scope.Order.ShipmentDate, "yyyy-MM-dd");
        //var address = $scope.Order.ShipmentAddress;
        var ShipmentAddress = $scope.replaceComma($scope.Order.ShipmentAddress);
        if ($scope.ngCart.$cart.items.length > 0)
        {
            if ($scope.CustomerForm.$valid) {
                $http({
                    method: 'POST',
                    url: '/Order/NewOrder/',
                    data: { cart: cart, customerinfo: customerinfo, ShipmentDate: ShipmentDate, ShipmentAddress: ShipmentAddress }
                }).success(function (data) {
                    if (data.success) {
                        toastr.success(data.successMessage);
                        $scope.successMessage = data.successMessage;
                        ngCart.empty();
                        $scope.Customer = [];
                        $scope.Order = [];
                        $scope.successStatus = true;
                        $scope.form = false;
                        $scope.OrderStatus = "Order Status";

                    } else {
                        toastr.error(data.errorMessage);
                    }
                }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                    toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                });
            }
            else {
                toastr.error("Please fill up all fields");
            }
        } else {
            toastr.error("Your Cart is empty, Continue Shopping");
        }
    };


    $scope.placeOrderByUploadBazarList = function(BazarImage) {
        $scope.Imagestatus = '';
        var customerinfo = $scope.Customer;
        customerinfo.City = $scope.city.ServiceAreaName.ServiceAreaName;
        var tempFile = [];
        //if ($scope.customerFormWithBazarList.$valid) {
        if ($scope.Customer.Name == null || $scope.Customer.Name == undefined) {
            toastr.error("Please insert Customer name");

        }
        if ($scope.Customer.Phone == null || $scope.Customer.Phone == undefined) {
            toastr.error("Please insert Customer Phone");
        }

        if ($scope.Order.ShipmentAddress == null || $scope.Order.ShipmentAddress == undefined) {
            toastr.error("Please insert Shipment Address");
        }
        if ($scope.Order.ShipmentDate == null || $scope.Order.ShipmentDate == undefined) {
            toastr.error("Please select Shipment Date");
        }
        if (BazarImage == null) {
            toastr.error("Please upload Bazar list");
        }
        if (BazarImage != null) {
            tempFile = BazarImage;
        }
        if (customerinfo.City == null || customerinfo.City == undefined) {
            toastr.error("Please select Area");
        } else {
            var ShipmentDate = $filter('date')($scope.Order.ShipmentDate, "yyyy-MM-dd");
            //var address = $scope.Order.ShipmentAddress;
            var ShipmentAddress = $scope.replaceComma($scope.Order.ShipmentAddress);
            customerinfo.ShipmentDate = ShipmentDate;
            customerinfo.ShipmentAddress = ShipmentAddress;
            $scope.Customer = customerinfo;
            Upload.upload({
                method: 'POST',
                url: '/Order/placeOrderByUploadBazarList/',
                fields: $scope.Customer,
                file: tempFile,
                async: true
            }).success(function(data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.Customer = {};
                    $scope.BazarImage = '';
                } else {
                    toastr.error(data.errorMessage);
                }
            }).error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });
        }


        //} else {
        //    toastr.error("Please fill up all fields");
        //}
    };

    $scope.replaceComma = function (aString) {
        aString = aString.replace(/\s*,\s*|\s+,/g, '#');
        return aString;
    };

    $scope.resetInPlaceOrder = function() {
        $scope.Customer = [];
        $scope.Order = [];
        $scope.Order.ShipmentDate = new Date(new Date().getTime() + (1 * 24 * 60 * 60 * 1000));
    };

    $scope.ReloadPage = function () {
        $window.location.reload();
    };
    
//Modal Code start from here

    $scope.msg = null;
    $scope.desc = "this is from desc";
    $scope.animationsEnabled = true;

    $scope.openModal = function (size, tempUrl, name, ProductId, Price, Discount, Unit, Image) {
        $scope.Name = name;
        $scope.ProductId = ProductId;
        $scope.Price = Price;
        $scope.Unit = Unit;
        $scope.itemImage = Image;
        $scope.Discount = Discount;
        var animationList = ['bottom', 'right', 'news', 'sidefall', 'fall', 'sticktotop', '3Dfliphorizontal', '3Dflipvertical', '3Dsign', 'Superscaled', 'Justme'];
        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: tempUrl,
            controller: 'InstanceController',
            scope: $scope,
          
            backdrop: true, //disables modal closing by click on the background
            keyboard: true, //dialog box is closed by hitting ESC key
            windowClass: 'modal modal-slide-in-' + animationList[Math.floor(Math.random() * 11) + 0], //class that is added to styles the window template
            resolve: {
                items: function() {
                    return $scope.Name;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            //$scope.selected = selectedItem;
        }, function() {
           // $log.info('Modal dismissed at: ' + new Date());
        });
    };
});


angular.module('myApp').controller('InstanceController', function ($scope, $uibModalInstance, $window, $http, $location, $routeParams, $filter, ngCart,items) {
   
    $scope.addToCart = function (Name, ProductId, Price, Quantity, Description) {
        if (Quantity == undefined) {
            Quantity = 1;
        }
        if (Description == '' || Description == undefined || Description == null) {
            Description = Name;
        }
        if (Name != "" && Name != undefined && ProductId != undefined && Quantity > 0 && Price != undefined) {
            ngCart.addItem(ProductId, Name, Price, Quantity, Description);
        } else {
            toastr.error("Please add to cart from product view");
        }
    };

    $scope.ok = function (email, name, password) {
        $uibModalInstance.close($scope.data);
    };
    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});

