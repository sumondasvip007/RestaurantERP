angular.module('myApp').controller('ReciveVoucherController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route, AccLedgerService, ReciveVoucherServices) {

    var voucher = { id: 1, active: true, isDabit: true}
    $scope.button = "save";
    $scope.isUpdate = false;
    $scope.row = [];
    $scope.row.push(voucher);
    $scope.totalDabit = 0;
    $scope.totalCredit = 0;
    
    $scope.remove = function (voucher, voucherList) {
        var type = "";
        var total = 0;
        voucher.active = false;
        type = "Debit";
        total = ReciveVoucherServices.CalculateTotal(voucherList, type);
        $scope.totalDabit = total;
        type = "Credit";
        total = ReciveVoucherServices.CalculateTotal(voucherList, type);
        $scope.totalCredit = total;
  
    }
    $scope.AddRow = function(data) {
        $scope.rowCount = $scope.row.length;
        $scope.rowCount = $scope.rowCount + 1;
        var newVoucher = {
            id: $scope.rowCount,
            active: true,
            isDabit: true
        }
        $scope.row.push(newVoucher);
    }

    $scope.changeState = function (voucher, voucherList) {
        var type = "";
        var total = 0;

    

        if (!voucher.isDabit) {

            voucher.isDabit = true;
            type = "Debit";
            total = ReciveVoucherServices.CalculateTotal(voucherList, type);
            $scope.totalDabit = total;
            type = "Credit";
            total = ReciveVoucherServices.CalculateTotal(voucherList, type);
            $scope.totalCredit = total;



        } else {
            voucher.isDabit = false;
            type = "Debit";
            total = ReciveVoucherServices.CalculateTotal(voucherList, type);
            $scope.totalDabit = total;
            type = "Credit";
            total = ReciveVoucherServices.CalculateTotal(voucherList, type);
            $scope.totalCredit = total;

        }
    }
   
    $scope.GetAllLedger = function() {
        AccLedgerService.GetAllLedger().then(function (data) {
      
            $scope.LedgerList = data;
        });
    }
    $scope.genarateVoucherNumber  =  function() {
        ReciveVoucherServices.GetVoucherId().then(function (data) {
            $scope.voucher = {
                voucherNumber: data
            }
            console.log($scope.voucher.voucherId);
        });
    }
    $scope.calculateDabit = function (voucherList, index) {
        var type = "Debit";
        var total = ReciveVoucherServices.CalculateTotal(voucherList, type);
        $scope.totalDabit = total;
    }
    $scope.calculateCredit = function (voucherList) {
        var type = "Credit";
        var total = ReciveVoucherServices.CalculateTotal(voucherList, type);
        $scope.totalCredit = total;
    }
    $scope.SaveVoucher = function () {

        var validate = ReciveVoucherServices.CheckValidation($scope);
        
        if (validate) {
            if ($scope.isUpdate === true) {
                ReciveVoucherServices.Update($scope, $scope.row);
            } else {
                ReciveVoucherServices.SaveVoucher($scope, $scope.row);
            }
         
        }
    }
    $scope.GetAllReciveVoucherList = function () {
     
        ReciveVoucherServices.GetAllReciveVoucher().then(function (response) {
            console.log(response.data.result);
            $scope.ReciveVoucherList = response.data.result;
            });
        }
    $scope.editViewCalled = function (id,index) {

        ReciveVoucherServices.GetVoucherByVoucherId(id).then(function(response) {
            var voucherList = [];
            $scope.voucher = {
                narration: $scope.ReciveVoucherList[index].Narration,
                voucherNumber: $scope.ReciveVoucherList[index].VNumber,
                voucherID:id
            };
            $scope.row = [];
            var ledgerId = [];
            var Debit = [];
            var Credit = [];
            var CheckDesc = [];
           

            var i = 0;
            angular.forEach(response.data.result, function (myVoucher, key) {
           
                console.log(myVoucher);
                ledgerId.push(myVoucher.LedgerID);
                Debit.push(myVoucher.Debit);
                Credit.push(myVoucher.Credit);
                CheckDesc.push(myVoucher.ChequeNumber);

                var voucher = {
                    ledgerId: ledgerId,
                    Debit:Debit,
                    Credit:Credit,
                    active: true,
                    isDabit: true,
                    CheckDesc: CheckDesc,
                    isUpdate: true
                }
                $scope.button = "Update";
                $scope.isUpdate = true;

                if (myVoucher.Credit > 0) {
                    voucher.isDabit = false;
                }
                $scope.row.push(voucher);
                i = i + 1;
            });
            console.log($scope.row);
            var type = "Debit";
            $scope.totalDabit = ReciveVoucherServices.CalculateTotal($scope.row, type);
            var type = "Credit";
            $scope.totalCredit = ReciveVoucherServices.CalculateTotal($scope.row, type);
        });
    }
    $scope.isSeachable = true;


    $scope.search = function () {
        $scope.isSeachable = false;
        console.log("click event called ");
    }




    $scope.itemArray = [
     { id: 1, name: 'first' },
     { id: 2, name: 'second' },
     { id: 3, name: 'third' },
     { id: 4, name: 'fourth' },
     { id: 5, name: 'fifth' },
    ];

    $scope.selected = { value: $scope.itemArray[0] };
});
