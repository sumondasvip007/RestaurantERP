angular.module('myApp').controller('BrokerController', function ($scope, $window, $http, $location, $routeParams, $filter,Upload, dateconfigservice,dropdownservice, isundefinedornullservice, globalvalueservice) {
   
    $scope.limage = '../Images/logo.jpg';
    $scope.himage = '../Images/header.jpg';

    $scope.open = function ($event, opened) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.RegisterBroker = {};
        $scope[opened] = true;

    };

    $scope.broker = {};
    $scope.LoadAllDropDown = function()
    {
        $scope.LoadStatus();
        $scope.loadStockExchange();
    }

    $scope.LoadStatus = function () {
        $scope.StatusList = null;
        $http.get('/SystemManagement/Broker/GetddlStatus/')
        .success(function (data) {
            $scope.StatusList = data;
        })
        .error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    };

    $scope.loadStockExchange = function () {

        $http.get('/SystemManagement/Broker/GetddlStockExchange')
        .success(function (data) {
            $scope.StockList = data.data;
        })
        .error(function (XMLHttpRequest, textStatus, errorThrown) {
            toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
        });
    }

    $scope.NextOne = function () {
        if ($scope.newBrokerFormSteepOne.$valid) {
            $scope.showme = true;
        }
        else {
            toastr.error("Please Fill up all required fields");
        }
    };
    
    $scope.NextTwo = function () {
        if ($scope.newBrokerFormSteepTwo.$valid) {
            $scope.hideme = true;
        }
        else {
            toastr.error("Please Fill up all required fields");
        }
    };


    $scope.AddNewBroker = function (file, sigPic) {
        var tempFile = [];
        $scope.broker.Imagestatus = '';
        if ($scope.brokerAvailable == 1)
        {
            if ($scope.newBrokerForm.$valid) 
            {
                if (angular.isUndefined(sigPic)) {
                    $scope.broker.Imagestatus = 'l';
                    tempFile[0] = file;

                }
                if (angular.isUndefined(file)) {
                    tempFile[0] = sigPic;
                    $scope.broker.Imagestatus = 'h';
                }
                if (angular.isUndefined(sigPic) == false && angular.isUndefined(file) == false) {
                    tempFile[0] = file;
                    tempFile[1] = sigPic;
                }
                if(angular.isUndefined(sigPic) && angular.isUndefined(file)){
                    tempFile = [];
                }

                var datefilter = $filter('date');
                $scope.broker.registration_date = datefilter($scope.broker.registration_date, 'dd/MM/yyyy');
                $scope.broker.registration_date = dateconfigservice.UIDateToServerDate($scope.broker.registration_date);
               
                Upload.upload({
                    method: 'POST',
                    url: '/SystemManagement/Broker/AddNewBroker',
                    fields: $scope.broker,
                    data:  $scope.broker.Imagestatus,
                    file: tempFile,
                    async: true
                }).success(function (data) {
                    if (data.success) {
                        $scope.Registration();
                        toastr.success("Registration Successful");
                    }
                    else {
                        toastr.error(data.errorMessage);
                    }
                });
            }
        }
    };

    $scope.AddNewBrokerAnonimus = function (file, sigPic) {
        var tempFile = [];
        $scope.broker.Imagestatus = '';
        if ($scope.brokerAvailable == 1) {
            if (angular.isUndefined(sigPic)) {
                $scope.broker.Imagestatus = 'l';
                tempFile[0] = file;

            }

            if (angular.isUndefined(file)) {
                tempFile[0] = sigPic;
                $scope.broker.Imagestatus = 'h';
            }
            if (angular.isUndefined(sigPic) == false && angular.isUndefined(file) == false) {
                tempFile[0] = file;
                tempFile[1] = sigPic;
            }
            if (angular.isUndefined(sigPic) && angular.isUndefined(file)) {
                tempFile = [];
            }

            var datefilter = $filter('date');
            $scope.broker.registration_date = datefilter($scope.broker.registration_date, 'dd/MM/yyyy');
            $scope.broker.registration_date = dateconfigservice.UIDateToServerDate($scope.broker.registration_date);
            //$scope.broker.user = $scope.RegisterBroker.UserName;

            Upload.upload({
                method: 'POST',
                url: '/SystemManagement/Broker/AddNewBroker',
                fields: $scope.broker,
                data: $scope.broker.Imagestatus,
                file: tempFile,
                async: true
            }).success(function (data) {
                if (data.success) {
                    $scope.RegistrationAnonimus();
                    toastr.success("Registration Successful");
                }
                else {
                    toastr.error(data.errorMessage);
                }
            });           
        }
    };

    $scope.brokerAvailable;
    $scope.CheckBroker = function () {
        if ($scope.broker.membership_id != undefined)
        {
            $http.get('/SystemManagement/Broker/CheckBroker?membership_id=' + $scope.broker.membership_id)
            .success(function (data) {
                if (data == 'NoAvailable') {
                    $scope.brokerAvailable = 0;
                    toastr.error('This Broker Already Registered');
                }
                else {
                    $scope.brokerAvailable = 1;
                    toastr.success('Broker is available');
                }
            })
        }
       
    };

    $scope.CheckUser = function () {
        if ($scope.RegisterBroker.UserName != undefined) {
            $scope.RegisterBroker.Email = $scope.broker.email;
            $scope.RegisterBroker.PhoneNumber = $scope.broker.contact_no;
            //$scope.RegisterBroker.membership_id = $scope.broker.membership_id;
            $http({
                method: 'POST',
                url: '/SystemManagement/Broker/CheckUser',
                data: { model: $scope.RegisterBroker }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);

                }
                else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            })
        }

    };

    $scope.UploadRecPayFile = function(excelFile)
    {
        if ($('#RecPayFileUpload').val().substr($('#RecPayFileUpload').val().length - 5) != '.xlsx' && $('#RecPayFileUpload').val().substr($('#RecPayFileUpload').val().length - 4) != '.xls') {
            toastr.warning('Please select excel file');
        }
        else {           
            var tempFile = [];
            tempFile[0] = excelFile;
            Upload.upload(
                   {
                       url: '/SystemManagement/Broker/UploadClientRecPayFile',
                       method: 'POST',
                       file: tempFile,
                       async: true
                   }).success(function (data) {
                       if (data.data == 'failed') {
                           toastr.error(data.errorMessage);
                       }
                       else {
                           $scope.Filedate = data.data;
                           $(".loader").hide();
                           toastr.success('Uploaded Successfully!!!');
                           $scope.isFileAlreadyExecuted();
                       }
                   }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                       toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                   });
        }
    }

   
    $scope.UploadCashFile = function (xmlFile) {

        if ($('#CashFileUpload').val().substr($('#CashFileUpload').val().length - 4) != '.xml') {
            toastr.warning('Please select cash limit xml file');
        }
        else {           
            var tempFile = [];
            tempFile[0] = xmlFile;
            Upload.upload(
                   {
                       url: '/SystemManagement/Broker/UploadCashLimitFile',
                       method: 'POST',
                       file: tempFile,
                       fields: { membership_id: $scope.broker.membership_id },
                       async: true
                   }).success(function (data) {
                       if (data.data == 'failed') {
                           toastr.error(data.errorMessage);
                       }
                       else {
                           $scope.Filedate = data.data;                        
                           toastr.success('Uploaded Successfully!!!');
                           // $scope.isFileAlreadyExecuted();
                       }
                   })
        }
    };

    $scope.UploadShareFile = function(xmlFile)
    {
        if ($('#ShareFileUpload').val().substr($('#ShareFileUpload').val().length - 4) != '.xml'){
            toastr.warning('Please select share limit xml file');  
        }
        else
        {
            var tempFile = [];
            tempFile[0] = xmlFile;
            Upload.upload(
                {
                    url: '/SystemManagement/Broker/UploadShareLimitFile',
                    method: 'POST',
                    file: tempFile,
                    fields: { membership_id: $scope.broker.membership_id },
                    async: true
                }).success(function(data){
                    if (data.data == 'failed') {
                        toastr.error(data.errorMessage);
                    }
                    else
                    {
                        $scope.Filedate = data.data;
                        toastr.success('Uploaded Successfully');
                    }
                })
            
        }
    }

    $scope.UploadPriceFile = function (xmlFile) {
        
        if ($('#FileUpload').val().substr($('#FileUpload').val().length - 4) != '.xml') {
            toastr.warning('Please select xml file');
        }        
        else
        {
            $(".loader").show();
            var tempFile = [];
            tempFile[0] = xmlFile;
            Upload.upload(
                   {
                       url: '/SystemManagement/Broker/UploadPriceFile',
                       method: 'POST',                       
                       file: tempFile,
                       fields: {membership_id: $scope.broker.membership_id },
                       async: true
                   }).success(function(data)
                   {
                       if (data.data == 'failed') {
                           toastr.error(data.errorMessage);
                       }
                       else
                       {
                           $scope.Filedate = data.data;
                           $(".loader").hide();
                           toastr.success('Uploaded Successfully!!!');
                           $scope.isFileAlreadyExecuted();
                       }
                   }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                       toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                   });
        }
    };

    $scope.UploadTradeFile = function (xmlFile) {
        if ($scope.broker.stock_exchange_id == undefined)
        {
            toastr.error('Please select stock exchange');
        }
        else if ((dropdownservice.getSelectedText($scope.StockList, $scope.broker.stock_exchange_id) == 'Dhaka Stock Exchange Limited') && ($('#FileUpload').val().substr($('#FileUpload').val().length - 4) != '.xml')) {
            toastr.warning('Please select xml file for DSE');
        }
        else if ((dropdownservice.getSelectedText($scope.StockList, $scope.broker.stock_exchange_id) == 'Chittagong Stock Exchange Limited') && ($('#FileUpload').val().substr($('#FileUpload').val().length - 4) != '.txt')) {
            toastr.warning('Please select txt file for CSE');
        }
        else {           
            var tempFile = [];
            tempFile[0] = xmlFile;
            Upload.upload(
                   {
                       url: '/SystemManagement/Broker/UploadTradeFile',
                       method: 'POST',
                       //fields: $scope.broker,
                       fields: { stock_exchange_id: $scope.broker.stock_exchange_id,  membership_id: $scope.broker.membership_id },
                       file: tempFile,
                       async: true
                   }).success(function (data) {
                       if (data.data == 'failed') {
                           toastr.error(data.errorMessage);
                       }
                       else {
                           $scope.Filedate = data.data;
                           $(".loader").hide();
                           toastr.success('Uploaded Successfully!!!');
                           $scope.isFileAlreadyExecuted();
                       }
                   }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                       toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                   });
        }       
    };

    $scope.CheckPassword = function () {

        if ($scope.RegisterBroker.Password != undefined) {
            if ($scope.RegisterBroker.Password != $scope.RegisterBroker.ConfirmPassword) {
                $scope.newBrokerForm.ConfirmPassword.$setValidity('mismatch', false)
            }
                // else if()
            else {
                toastr.success('Password Matched');
                $scope.newBrokerForm.ConfirmPassword.$setValidity('mismatch', true)
            }
        }
    };

    $scope.CheckPasswordAnonimus = function () {

        if ($scope.RegisterBroker.Password != undefined) {
            if ($scope.RegisterBroker.Password != $scope.RegisterBroker.ConfirmPassword) {
                $scope.newBrokerFormSteepThree.ConfirmPassword.$setValidity('mismatch', false)
            }
                // else if()
            else {
                toastr.success('Password Matched');
                $scope.newBrokerFormSteepThree.ConfirmPassword.$setValidity('mismatch', true)
            }
        }
    };


    $scope.Registration = function () {
        $scope.RegisterBroker.Email = $scope.broker.email;
        $scope.RegisterBroker.PhoneNumber = $scope.broker.contact_no;
        //$scope.RegisterBroker.membership_id = $scope.broker.membership_id;
        
        if ($scope.newBrokerForm.$valid) {
            $http({
                method: 'POST',
                url: '/Account/RegisterDefaultBrokerUser',
                data: { model: $scope.RegisterBroker, membership_id: $scope.broker.membership_id }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.RegisterBroker = {};

                }
                else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            })
        }
    }
    
    $scope.RegistrationAnonimus = function () {
        $scope.RegisterBroker.Email = $scope.broker.email;
        $scope.RegisterBroker.PhoneNumber = $scope.broker.contact_no;
        //$scope.RegisterBroker.membership_id = $scope.broker.membership_id;

        if ($scope.newBrokerFormSteepThree.$valid) {
            $http({
                method: 'POST',
                url: '/Account/RegisterDefaultBrokerUser',
                data: { model: $scope.RegisterBroker, membership_id: $scope.broker.membership_id }
            }).success(function (data) {
                if (data.success) {
                    toastr.success(data.successMessage);
                    $scope.RegisterBroker = {};

                }
                else {
                    toastr.error(data.errorMessage);
                }
            }).error(function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            })
        }
    }

    $scope.ResetForAuthUser = function () {

        $scope.broker.Name = "";
        $scope.broker.mail_address = "";
        $scope.broker.short_name = "";
        $scope.broker.trec_number = "";
        $scope.broker.membership_id = "";
        $scope.broker.active_status_id = 0;
        $scope.broker.registration_no = "";
        $scope.broker.paid_up_capital = "";
        $scope.broker.authorized_capital = "";
        $scope.broker.compliance_authority = "";
        $scope.broker.no_of_ar="";
        $scope.broker.contact_no="";
        $scope.broker.email="";
        $scope.broker.registration_date = "";
        //$scope.LogoFile = "Logo";

    };


    $scope.ResetForAnonimusOne = function () {

        $scope.broker.Name = "";
        $scope.broker.mail_address = "";
        $scope.broker.short_name = "";
        $scope.broker.trec_number = "";
        $scope.broker.membership_id = "";
        $scope.broker.active_status_id = 0;
        $scope.broker.registration_no = "";
        $scope.broker.paid_up_capital = "";
        $scope.broker.authorized_capital = "";
        $scope.broker.compliance_authority = "";
        $scope.broker.no_of_ar = "";
        $scope.broker.contact_no = "";
        $scope.broker.email = "";
        $scope.broker.registration_date = "";
        //$scope.LogoFile = "Logo";

    };

    $scope.ResetForAnonimusTwo = function () {

    };

    $scope.ResetForAnonimusThree = function () {

    };



});