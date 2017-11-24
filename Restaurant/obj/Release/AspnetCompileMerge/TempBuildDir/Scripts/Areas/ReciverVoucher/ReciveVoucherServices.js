angular.module("myApp").factory('ReciveVoucherServices', function($http,$route) {
    return {
        validationForm: function($scope) {

        },
        SearchOnDropDown: function($scope) {

        },
        GetVoucherId: function() {
            var promise = $http.get('/ReciveVoucher/GetVoucherId').then(function(response) {
                return response.data.result;

            });
            return promise;
        },
        CalculateTotal: function(voucherList, type) {
            var total = 0;

            var i = 0;

            angular.forEach(voucherList, function(voucher) {
                if (voucher.Debit && voucher.isDabit === true) {
                    if (voucher.Debit[i] != null && voucher.active === true && type === "Debit" && voucher.isDabit === true) {
                        total = parseInt(total) + parseInt(voucher.Debit[i]);
                    }
                }

                if (voucher.Credit && voucher.isDabit === false) {
                    if (voucher.Credit[i] != null && voucher.active === true && type === "Credit" && voucher.isDabit === false) {
                        total = parseInt(total) + parseInt(voucher.Credit[i]);
                    }
                }
                i = i + 1;
            });
            return total;
        },
        CheckValidation: function($scope) {
            var debit = $scope.totalDabit;
            var credit = $scope.totalCredit;

            if ($scope.voucherForm.ledgerDropdown.$invalid) {
                toastr.error("Select from DropDown");
                return false;
            }
            if ($scope.voucherForm.checkDesc.$invalid) {
                toastr.error("Check Description");
                return false;
            }
            if (debit != credit) {
                toastr.error("Debit Must Equat to Credit");
                return false;
            }
            return true;
        },
        SaveVoucher: function($scope) {

            var accountVoucherList = [];
            var i = 0;
            angular.forEach($scope.row, function(voucher, key) {

                var debit = 0;
                var credit = 0;
     
                if (voucher.Debit && voucher.isDabit === true) {
                     debit = voucher.Debit[i];
                }
                if (voucher.Credit && voucher.isDabit === false) {
                    credit = voucher.Credit[i];
                }
                var vm_accountVoucher = {
                        Debit: debit,
                        Credit: credit,
                        ChequeNumber: voucher.CheckDesc[i],
                        LedgerID: voucher.ledgerId[i]
                }              
                i++;
                accountVoucherList.push(vm_accountVoucher);
            });
      
               
        
                    $http({
                        method: 'POST',
                        url: '/AccVoucher/SaveReciveVoucher/',
                        data: { VNumber: $scope.voucher.voucherNumber, Narration: $scope.voucher.narration, VoucherDetails: accountVoucherList }
                    }).success(function (data) {
                        if (data.success) {
                            toastr.success(data.successMessage);
                            $route.reload();
                        } else {
                            toastr.error(data.errorMessage);
                        }
                    }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                        console.log("Error");
                        toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                    });
                
          
        },
       GetAllReciveVoucher: function () {

         var promise  =    $http.get('/AccVoucher/GetAllReciveVoucher/').
         success(function (data) {
             
             if (data.success) {
            
                 return  data.result;
             }
             else {
                 toastr.error(data.errorMessage);
             }
         }).
         error(function (XMLHttpRequest, textStatus, errorThrown) {
             toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
         });
            return promise;
       },
       GetVoucherByVoucherId: function (id) {


          var promise =  $http({
               method: 'POST',
               url: '/AccVoucher/GetVoucherDetailsByVoucherID/',
               data: { VoucherID : id }
           }).success(function (data) {
               if (data.success) {
                   return data.result;

               } else {
                   toastr.error(data.errorMessage);
               }
           }).error(function (XMLHttpRequest, textStatus, errorThrown) {
               console.log("Error");
               toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
           });
           return promise;
       },
       Update: function ($scope) {

           var accountVoucherList = [];
           var i = 0;
           console.log("voucherId");
           console.log();
           angular.forEach($scope.row, function (voucher, key) {

               var debit = 0;
               var credit = 0;

               if (voucher.Debit && voucher.isDabit === true) {
                   debit = voucher.Debit[i];
               }
               if (voucher.Credit && voucher.isDabit === false) {
                   credit = voucher.Credit[i];
               }
               console.log(voucher);
               var vm_accountVoucher = {
                   Debit: debit,
                   Credit: credit,
                   ChequeNumber: voucher.CheckDesc[i],
                   LedgerID: voucher.ledgerId[i],
               }
               i++;
               accountVoucherList.push(vm_accountVoucher);
           });
           var PaymentVoucherEntry = {
               VNumber:$scope.voucher.voucherNumber,
               Narration: $scope.voucher.narration,
               VoucherID: $scope.voucher.voucherID
           }
        
           $http({
               method: 'POST',
               url: '/AccVoucher/UpdateReciveVoucher/',
               data: { ReciveVoucherEntry: PaymentVoucherEntry, ReciveVoucherDetails: accountVoucherList }
           }).success(function (data) {
               if (data.success) {
                   toastr.success(data.successMessage);
                   $route.reload();
               } else {
                   toastr.error(data.errorMessage);
               }
           }).error(function (XMLHttpRequest, textStatus, errorThrown) {
               console.log("Error");
               toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
           });


            
        }



           
       }
    
});