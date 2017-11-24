angular.module("myApp").factory('AccLedgerService', function ($http) {
    return {
        LedgerValidation: function ($scope) {
            var isValidate = true;
            if ($scope.accLedgerForm.ledgerName.$invalid) {
                toastr.error("Please Fill LedgerName");
                return  false;
            }
            if ($scope.accLedgerForm.ledgerCode.$invalid) {
                toastr.error("Please Fill ledgerCode");
                return false;
            }
            if ($scope.accLedgerForm.openingBalance.$invalid) {
                toastr.error("Please Fill opening Balance");
                return false;
            }
            if ($scope.accLedgerForm.accountGroup.$invalid) {
                toastr.error("Please Select Account Group");
                return false;
            }
            if ($scope.accLedgerForm.balanceType.$invalid) {
                toastr.error("Please Select Balance Type");
                return false;
            }
         
        },
        GetBalanceTypes: function () {
            var promise = $http.get('/AccLedger/GetBalanceTypesJsonResult').then(function (response) {
       
                if (response.data.success === true) {
                
                    return response.data.result;
                } else {
                    toastr.error(response.data.errorMessage);
                }
            });

            return promise;
        },
        GetAllGroupList: function () {
            var promise = $http.get('/AccGroup/GetAccGroupInformation').then(function (response) {
                if (response.data.success === true) {
            
                    return response.data.result;
                }
                else {
                    toastr.error(response.data.errorMessage);
                }

           });
            return promise;
        },
       SaveLedger:function(data) {
           var promise = $http.post('/AccLedger/AddLedger', data).then(function (response) {
               if (response.data.success) {
                   toastr.success(response.data.successMessage);
               }
           });
           return promise;
       },

        GetAllLedger:function() {
            var promise = $http.get('/AccLedger/GetAllLedger').then(function(response) {
                if (response.data.success) {
                    return response.data.result;
                } else {
                    toastr.error(response.data.errorMessage);
                }

            });
            return promise;
        },
      GetLedgerById:function(data) {
          var promise = $http.post('/AccLedger/GetLedgerByIdJsonResult', {id:data}).then(function(response) {
              if (response.data.success) {
                  return response.data.result;
              } else {
                  toastr.error(response.data.errorMessage);
              }

          });
          return promise;
      },
     UpdateLedger:function(data) {
         var promise = $http.post('/AccLedger/UpdateLedger', data).then(function (response) {
             if (response.data.success === true) {
                 toastr.success(response.data.successMessage);
             } else {
                 toastr.error(response.data.errorMessage);
             }
         });
         return promise;
     }

    }
});