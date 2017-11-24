'use strict';
angular.module('myApp', ['ngRoute', 'ui.router', 'smart-table', 'ngAnimate', 'ngInputDate', 'ui.bootstrap', 'ngCart', 'ngFileUpload', 'fancyboxplus', 'blockUI', 'ui.select', 'ngSanitize', 'angular-elevate-zoom', 'ivh.treeview', '720kb.datepicker', 'ui.select', 'ngSanitize', 'dx'])
    .config(function($routeProvider, $locationProvider, blockUIConfig) {


        var id = ':ViewId';
        $routeProvider
            .when('/',
            {
                //redirectTo: '/'
                templateUrl: 'Home/Index'
            })
            .when('/Index',
            {
                templateUrl: 'Home/Index'
            })
            .when('/RedirectToMain',
            {
                templateUrl: 'Home/RedirectToMain'
            })
            .when('/Error',
            {
                controller: function() {
                    toastr.error('sdsddad');
                },
                //templateUrl: $location.path()
                template: 'You do not have permission'
            })
            .when('/Login',
            {
                templateUrl: '/Account/Login'

            })
            .when('/Account/Login',
            {
                templateUrl: '/Account/Login'
            })
            .when('/Contact',
            {
                templateUrl: '/Home/Contact'
            })
            .when('/Register',
            {
                templateUrl: '/Account/Register',
                controller: 'RegisterController'
            })
            //##################### MAMUN'S WORK START'S HERE ######################
            .when('/UserAdd',
            {
                templateUrl: '/Account/AddAspNetUser'
            })
            .when('/UserList',
            {
                templateUrl: '/Account/AspNetUserList'
            })
            .when('/ProductAdd',
            {
                templateUrl: '/Product/ProductAdd'
            })
            .when('/ProductList',
            {
                templateUrl: '/Product/GetAllProduct'
            })
            .when('/SellsPointAdd',
            {
                templateUrl: '/SellsPoint/SellsPointAdd'
            })
            .when('/SellsPointList',
            {
                templateUrl: '/SellsPoint/SellsPointDetails'
            })
            .when('/SupplierProductToStore',
            {
                templateUrl: '/ProductToStore/ProductToStore'
            })
            .when('/SellsPointStatus',
            {
                templateUrl: '/SellsPointStatus/SellsPointProductStatus'
            })
            .when('/ProductEntryHistoryInSellsPoint' +
                '',
                {
                    templateUrl: '/ProductEntryHistoryInSellPoint/ProductEntryHistoryInSellPoint'
                })
            .when('/MainStoreStatus',
            {
                templateUrl: '/MainStoreStatus/MainStoreProductStatus'
            })
            .when('/AccGroup',
            {
                templateUrl: '/AccGroup/AccGroup'
            })
            .when('/AccPaymentVoucher',
            {
                templateUrl: '/AccVoucher/AccPaymentVoucher'
            })
            //##################### MAMUN'S WORK END'S HERE ######################

            //##################### 05/03/17: TODAY'S WORK END'S HERE ######################

            //############### 15/03/2017 Sumon  #############################
            .when('/EmployeeInformationAdd',
            {
                templateUrl: '/EmployeeInformation/SaveEmployeeInformation'
            })
            .when('/EmployeeInformationList',
            {
                templateUrl: '/EmployeeInformation/EmployeeInformationList'
            })
            .when('/ProductEntryHistoryFromSupplierToMainStoreReport',
            {
                templateUrl: '/ProductEntryHistoryFromSupplierToMainStore/ProductEntryHistoryFromSupplierToMainStore'
            })
            .when('/ProductAndSupplierHistoryForSpecificDate',
            {
                templateUrl: '/ProductEntryHistoryForaSpecificDateFromSupplierToMainStore/ProductEntryHistoryForaSpecificDateFromSupplierToMainStore'
            })
            .when('/AccContraVoucher',
            {
                templateUrl: '/AccVoucher/AccContraVoucher'
            })
            .when('/AccJournalVoucher',
            {
                templateUrl: '/AccVoucher/AccJournalVoucher'
            })
            .when('/DbBackUp',
            {
                templateUrl: '/DbBackUp/DbBackUp'
            })
           .when('/OpeningProductBalanceInStore',
            {
                templateUrl: '/OpeningProductBalanceInStore/OpeningProductBalanceInStore'
            })
        .when('/ViewProductSellHistoryWithOpeningProduct',
            {
                templateUrl: '/ProductSellHistoryWithOpeningProduct/ViewProductSellHistoryWithOpeningProduct'
            })
         .when('/AddOtherExpenseWhenSell',
            {
                templateUrl: '/OtherExpenseWhenSell/AddOtherExpenseWhenSell'
            })
        .when('/AddGroupAndShiftMapping',
            {
                templateUrl: '/GroupAndShiftMapping/AddGroupAndShiftMapping'
            })
         .when('/ViewGroupAndShiftMappingList',
            {
                templateUrl: '/GroupAndShiftMapping/ViewGroupAndShiftMappingList'
            })
          .when('/ProductEntryHistoryForSpecificDateInSellPoint',
            {
                templateUrl: '/ProductEntryHistoryForSpecificDateInSellPoint/ProductEntryHistoryForSpecificDateInSellPoint'
            })


            //############### 15/03/2017 Sumon  #############################
            .when('/About',
            {
                templateUrl: '/Home/About'
            })
            .when('/ChangePassword',
            {
                templateUrl: '/Account/Manage',
                controller: 'ChangePassword'
    })
            .when('/Account/Manage#/', {
                templateUrl: '/Account/Manage'
            })
            .when('/AddCategory',
            {
                templateUrl: '/Category/AddCategory',
                controller: 'CategoryController'
            })
            .when('/AddSubCategory',
            {
                templateUrl: '/SubCategory/AddSubCategory',
                controller: 'SubCategoryController'
            })
            .when('/EditCategory/:id',
            {
                templateUrl: '/Category/GetCategoryById',
                controller: 'CategoryController'
            })
            .when('/AddProduct',
            {
                templateUrl: '/Product/AddNewProduct',
                controller: 'ProductController'
            })
            .when('/ProductList',
            {
                templateUrl: '/Product/ProductList',
                controller: 'ProductController'
            })
            .when('/EditProduct/:id',
            {
                templateUrl: '/Product/EditProduct',
                controller: 'ProductController'
            })
            .when('/TodaysOrders',
            {
                templateUrl: '/Order/TodaysOrder',
                controller: 'DashboardController'
            })
            .when('/SuggestedProducts',
            {
                templateUrl: '/Product/SuggestedProducts',
                controller: 'ProductController'
            })
            .when('/AddSuggestedProduct/:id',
            {
                templateUrl: '/Product/AddSuggestedProduct',
                controller: 'ProductController'
            })
            .when('/ReportOrderWise',
            {
                templateUrl: '/Order/ReportOrderWise',
                controller: 'DashboardController'
            })
            .when('/Invoice',
            {
                templateUrl: '/Order/Invoice',
                controller: 'DashboardController'
            })
            .when('/UploadedBazarList',
            {
                templateUrl: '/Order/UploadedBazarList',
                controller: 'DashboardController'
            })
            .when('/UpdateOrder/:id',
            {
                templateUrl: '/Order/UpdateOrderView',
                controller: 'DashboardController',
            })


            /*=============================================
                         MASUD AND HASIB START
            ==============================================*/
            .when('/AddAction',
            {
                templateUrl: '/MenuAction/AddAction',
                controller: 'addActionMenuController'
            })
            .when('/viewAction',
            {
                templateUrl: '/MenuAction/ViewAction',
                controller: 'viewActionController'
            })
            .when('/Store',
            {
                templateUrl: '/Store/Store',
                controller: 'storeController'
            })
            .when('/StoPMapping',
            {
                templateUrl: '/SupplierProductMapping/Mapping',
                controller: 'supplierProductMappingController'
            })
            .when('/PHtoPMapping',
            {
                templateUrl: '/ProductionHouseProductMapping/PhpMapping',
                controller: 'productionHouseProductMappingController'
            })
            .when('/PtoPHMapping',
            {
                templateUrl: '/ProductEntryToProductionHouse/ProductToHouse',
                controller: 'productEntryToProductionHouseController'
            })
            /*=============================================
                        MASUD AND HASIB END
           ==============================================*/
            //##################### 06/03/17: TODAY'S WORK START'S HERE Mosaddik ######################
            .when('/Module',
            {
                templateUrl: '/Module/Index'

            })
            .when('/ShowAllModule',
            {
                templateUrl: '/Module/Edit',
                controller: "showAllModule"

            })
            .when('/EditModule/:id',
            {
                templateUrl: '/Module/Index'

            })
            .when('/ChalanReport', {
                templateUrl: '/ChalanReportView/ChalanReportView'
            })
            .when('/UserPermissions',
            {
                templateUrl: '/Account/UserActionMapping',
                controller: 'UserPermissionController'

            })
            .when('/ProductionHouse',
            {
                templateUrl: '/ProductionHouse/Index'

            })
            .when('/MainStoreProductTransferStatus',
            {
                templateUrl: '/MainStoreProductTransferStatus/MainStoreToProductionHouse',
            })
            .when('/ShowProductionHouse',
            {
                templateUrl: '/ProductionHouse/ShowProductionHouse'

            })
            .when('/ShowAllSupplier',
            {
                templateUrl: '/SupplierInformation/ShowAllSupplier'

            })
            .when('/SupplierInformation',
            {
                templateUrl: '/SupplierInformation/Index'

            })
            .when('/EditProductionHouse/:id',
            {
                templateUrl: '/ProductionHouse/Index'

            })
            .when('/EditSupplierInformation/:id',
            {
                templateUrl: '/SupplierInformation/Index'

            })
            .when('/ProductUsesInProductionHouse',
            {
                templateUrl: '/ProductUsesInProductionHouse/ProductUsesInProduintionHouse',
                controller: "ProductUsesInProductionHouse"
            })
            .when('/ProductToProductionHouse',
            {
                templateUrl: '/ProductToProductionHouse/ProductToProductionHouse'

            })
            .when('/AccLedger',
            {
                templateUrl: '/AccLedger/AccLedger',
                controller: 'AccLedgerController'
            })
            .when('/ReciveVoucher',
            {
                templateUrl: '/ReciveVoucher/ReciveVoucher',
                controller: 'ReciveVoucherController'
            })
            //------------   15/03/17  ---------------  // 
              .when('/ProductionHouseStatus',
            {
                templateUrl: '/ProductionHouseStatus/ProductionHouseStatus'

            })
             .when('/phtosptransfer',
            {
                templateUrl: '/PHtoSPProductTransfer/PHtoSPProductTransfer'

            })

             .when('/phtosptransfer22',
            {
                templateUrl: '/PHtoSPProductTransfer/ProductionHouseToSellsPointReport'

            })
               //##################### 05/03/17: TODAY'S WORK END'S HERE ######################
             .when('/EditAction/:id',
            {
                templateUrl: '/MenuAction/EditAction',
                controller: 'editActionController'
            })

            .when('/productsold',
            {
                templateUrl: '/ProductSold/ProductSoldForm'

            })
             .when('/productsellreport',
            {
                templateUrl: '/ProductSellReport/ProductSellReport'

            })
            //If link not found Invoice  UploadedBazarList
            .otherWise
        {
            redirectTo: '/'
        }
    }).filter("dateFilter", function () {
        return function (item) {
            if (item != null) {
                return new Date(parseInt(item.substr(6)));
            }
            return "";
        };
    }).directive('addASpaceBetween', [function () {
        'use strict';
        return function (scope, element) {
            element.after(' ');
        }
    }
    ])
.directive("dropdown", ["$interval", function ($interval) {
    return {
        restrict: "A",
        link: function (scope, elem, attrs) {
            $(elem).click(function (e) {
                e.preventDefault();
                $(this).parent().find('ul').slideToggle();
            });
            }
        }
    
}]);;

