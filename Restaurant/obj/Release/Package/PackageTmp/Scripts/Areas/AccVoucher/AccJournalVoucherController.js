angular.module('myApp').controller('AccJournalVoucherController', function ($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    var today = new Date();
    $scope.JournalVoucherEntry = {
        TransactionDate: today
    };
    $scope.TotalDebit = 0;
    $scope.TotalCredit = 0;
    $scope.AddButton = true;
    $scope.choices = [{ id: 'choice1', 'LedgerID': "", 'ChequeNumber': "", 'Debit': "", 'Credit': "", 'DrCrButton': "Dr", 'DrTextBox': false, 'CrTextBox': true }, { id: 'choice2', 'LedgerID': "", 'ChequeNumber': "", 'Debit': "", 'Credit': "", 'DrCrButton': "Cr", 'DrTextBox': true, 'CrTextBox': false }];

    $scope.addNewChoice = function () {
        var newItemNo = $scope.choices.length + 1;
        $scope.choices.push({ 'id': 'choice' + newItemNo, 'LedgerID': "", 'ChequeNumber': "", 'Debit': "", 'Credit': "", 'DrCrButton': "Dr", 'DrTextBox': false, 'CrTextBox': true });
    };

    $scope.removeChoice = function (id) {
        if ($scope.choices.length > 2) {
            $scope.TotalDebit = parseInt($scope.TotalDebit) - (parseInt($scope.choices[id].Debit) | 0);
            $scope.TotalCredit = parseInt($scope.TotalCredit) - (parseInt($scope.choices[id].Credit) | 0);
            $scope.choices.splice(id, 1);
        }
    };

    $scope.DrCrButtonClick = function (id) {
        if ($scope.choices[id].DrCrButton === "Dr") {
            $scope.choices[id].DrCrButton = "Cr";
            $scope.choices[id].Debit = "";
            $scope.choices[id].DrTextBox = true;
            $scope.choices[id].CrTextBox = false;
        } else {
            $scope.choices[id].DrCrButton = "Dr";
            $scope.choices[id].Credit = "";
            $scope.choices[id].DrTextBox = false;
            $scope.choices[id].CrTextBox = true;
        }
        $scope.TotalDebitCalculate();
        $scope.TotalCreditCalculate();
    };

    $scope.GetJournalVoucherNumber = function () {
        //$scope.VNumber = [];
        $http.get('/AccVoucher/GetJournalVoucherNumber/').
          success(function (data) {
              if (data.success) {
                  $scope.JournalVoucherEntry.VNumber = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    $scope.GetJournalLadgerList = function () {
        $scope.LadgerList = [];
        $http.get('/AccVoucher/GetJournalLadgerList/').
          success(function (data) {
              if (data.success) {
                  $scope.LadgerList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    $scope.TotalDebitCalculate = function () {
        $scope.TotalDebit = 0;
        for (var i = 0; i < $scope.choices.length; i++) {
            $scope.TotalDebit = parseInt($scope.TotalDebit) + (parseInt($scope.choices[i].Debit) | 0);
        }
    };

    $scope.TotalCreditCalculate = function () {
        $scope.TotalCredit = 0;
        for (var i = 0; i < $scope.choices.length; i++) {
            $scope.TotalCredit = parseInt($scope.TotalCredit) + (parseInt($scope.choices[i].Credit) | 0);
        }
    };

    $scope.SaveJournalVoucher = function () {
        //if ($scope.AccPaymentVoucherForm.$valid) {

        if ($scope.TotalDebit === $scope.TotalCredit && $scope.TotalCredit !== 0 && $scope.TotalDebit !== 0) {
            $http({
                method: 'POST',
                url: '/AccVoucher/SaveJournalVoucher/',
                data: { VNumber: $scope.JournalVoucherEntry.VNumber, Narration: $scope.JournalVoucherEntry.Narration, VoucherDetails: $scope.choices }
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
        else {
            toastr.error("Debit an Credit Amount must have to be same and Can't be zero. So, Enter valid input.");
        }
        //}
        //else {
        //    toastr.error("Please fill up all fields");
        //}
    };

    $scope.GetAllJournalVoucherList = function () {
        $scope.JournalVoucherEntryList = [];
        $http.get('/AccVoucher/GetAllJournalVoucherList/').
          success(function (data) {
              if (data.success) {
                  $scope.JournalVoucherEntryList = data.result;
              }
              else {
                  toastr.error(data.errorMessage);
              }
          }).
          error(function (XMLHttpRequest, textStatus, errorThrown) {
              toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
          });
    };

    $scope.EditJournalVoucher = function (x) {
        $http({
            method: 'POST',
            url: '/AccVoucher/GetVoucherDetailsByVoucherID/',
            data: { VoucherID: x.VoucherID }
        }).success(function (data) {
            if (data.success) {
                $scope.choices = data.result;
                for (var i = 0; i < $scope.choices.length; i++) {
                    var debit = parseInt($scope.choices[i].Debit);
                    if (debit > 0) {
                        $scope.choices[i].DrCrButton = 'Dr';
                        $scope.choices[i].DrTextBox = false;
                        $scope.choices[i].CrTextBox = true;
                        $scope.choices[i].Credit = "";

                    }
                    else {
                        $scope.choices[i].DrCrButton = 'Cr';
                        $scope.choices[i].DrTextBox = true;
                        $scope.choices[i].CrTextBox = false;
                        $scope.choices[i].Debit = "";
                    }
                }
                $scope.JournalVoucherEntry = x;
                $scope.TotalDebitCalculate();
                $scope.TotalCreditCalculate();
                $scope.AddButton = false;
                $scope.UpdateButton = true;
                $scope.CancelButton = true;
            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    };

    $scope.CancelEdit = function (x) {
        $scope.AddButton = true;
        $scope.UpdateButton = false;
        $scope.CancelButton = false;
        $route.reload();
    };
    $scope.UpdateJournalVoucher = function () {
        //if ($scope.AccJournalVoucherForm.$valid) {
        $http({
            method: 'POST',
            url: '/AccVoucher/UpdateJournalVoucher/',
            data: { JournalVoucherEntry: $scope.JournalVoucherEntry, JournalVoucherDetails: $scope.choices }
        }).success(function (data) {
            if (data.success) {
                toastr.success(data.successMessage);
                $scope.AddButton = true;
                $scope.UpdateButton = false;
                $scope.CancelButton = false;
                $route.reload();
            } else {
                toastr.error(data.errorMessage);
            }
        }).error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
        //}
        //else {
        //    toastr.error("Please fill up all fields");
        //}
    };
});