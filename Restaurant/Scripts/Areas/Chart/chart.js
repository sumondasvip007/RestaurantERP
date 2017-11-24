angular.module('myApp').controller('ChartShowController', function($scope, $window, $http, $uibModal, Upload, $location, $routeParams, $filter, $route) {

    
        /////////////////////////////////////// from database //////////////////////////////////////////////
        $scope.dataSourceForBar = [];
        $scope.dataSourceForDoughnutFromDatabase = [];
        $scope.dataSourceForLineFromDb = [];
        $scope.dataSourceForPieFromDatabase = [];
        $scope.dataSourceForSpiderForDatabase = [];
        $scope.dataSourceForBarChartMonthlyDayShiftWise = [];
        $scope.dataSourceForBarChartMonthlyNightShiftWise = [];
        $scope.dataSourceForBarChartMonthly = [];
        $scope.dataSourceForLineChartForMonthlyDayShiftWise = [];
        $scope.dataSourceForLineChartForMonthlyNightShiftWise = [];
        $scope.dataSourceForLineChartForMonthlyTotalAmount = [];
        $scope.dataSourceForBarChartWeeklyDayShiftWise = [];
        $scope.dataSourceForBarChartWeeklyNightShiftWise = [];
        $scope.dataSourceForBarChartWeekly = [];
        $scope.dataSourceForLineChartForWeeklyDayShiftWise = [];
        $scope.dataSourceForLineChartForWeeklyNightShiftWise = [];
        $scope.dataSourceForLineChartForWeeklyTotalAmount = [];


        var arrData = new Array();
        var arrLabels = new Array();

        //$scope.ResultForBarChart = function() {
        $http({
                method: 'GET',
                url: '/Home/GetWeeklyTotalProductSellChart'
            }).success(function(data) {
                if (data.success) {


                    arrData = data.ValueData;
                    arrLabels = data.DayData;

                    $scope.data = [];
                    $scope.labels = [];


                    $scope.data.push(arrData.slice(0));

                    for (var i = 0; i < arrLabels.length; i++) {
                        $scope.labels.push(arrLabels[i]);


                    }


                    /////////////bar/////////////////////////
                    for (var j = 0; j < arrLabels.length; j++) {

                        $scope.dataSourceForBar.push(
                            { day: $scope.labels[j], value: arrData[j] }
                        );


                    }
                    $scope.chartOptions = {
                        dataSource: $scope.dataSourceForBar,
                        series: {
                            argumentField: "day",
                            valueField: "value",
                            name: "My oranges",
                            type: "bar",
                            color: '#ffaa66'
                        }
                    };

                    /////////////////////////////end////////////////////////////////////


                    /////////////////////Doughnut/////////////////////

                    for (var k = 0; k < arrLabels.length; k++) {

                        $scope.dataSourceForDoughnutFromDatabase.push(
                            { day: $scope.labels[k], val: arrData[k] }
                        );


                    }




                    $scope.chartOptionsForDoughnut = {
                        type: "doughnut",
                        palette: "Soft Pastel",
                        dataSource: $scope.dataSourceForDoughnutFromDatabase,
                        //title: "Selling Total Product number by Days",
                        tooltip: {
                            enabled: true,
                            //format: "millions",
                            customizeTooltip: function (arg) {
                                var percentText = Globalize.formatNumber(arg.percent, {
                                    style: "percent",
                                    minimumFractionDigits: 2,
                                    maximumFractionDigits: 2
                                });

                                return {
                                    text: arg.valueText + " - " + percentText
                                };
                            }
                        },
                        legend: {
                            horizontalAlignment: "right",
                            verticalAlignment: "top",
                            margin: 0
                        },
                        //"export": {
                        //    enabled: false
                        //},
                        series: [{
                            argumentField: "day",

                            label: {
                                visible: true,
                                //format: "millions",
                                connector: {
                                    visible: true
                                }
                            }
                        }]
                    };


                    $scope.currentType = types[0];
                    ////////////////end/////////////////


                    //////////////////////Line////////////////////////

                    for (var p = 0; p < arrLabels.length; p++) {

                        $scope.dataSourceForLineFromDb.push(
                            { day: $scope.labels[p], value: arrData[p] }
                        );


                    }



                    $scope.chartOptionsForLine = {
                        palette: "violet",
                        dataSource: $scope.dataSourceForLineFromDb,
                        commonSeriesSettings: {
                            argumentField: "day"
                        },
                        bindingOptions: {
                            "commonSeriesSettings.type": "currentType"
                        },
                        margin: {
                            bottom: 20
                        },
                        argumentAxis: {
                            valueMarginsEnabled: false,
                            discreteAxisDivisionMode: "crossLabels",
                            grid: {
                                visible: true
                            }
                        },
                        series: [
                            { valueField: "value" }

                        ],
                        legend: {
                            verticalAlignment: "bottom",
                            horizontalAlignment: "center",
                            itemTextPosition: "bottom"
                        },
                        title: {
                            //text: "Selling Total Product number by Daywise In this week",
                            subtitle: {
                                //text: "(Millions of Tons, Oil Equivalent)"
                            }
                        },
                        //"export": {
                        //    enabled: true
                        //},
                        tooltip: {
                            enabled: true,
                            customizeTooltip: function (arg) {
                                return {
                                    text: arg.valueText
                                };
                            }
                        }
                    };

                    $scope.typesOptions = {
                        dataSource: types,
                        bindingOptions: {
                            value: "currentType"
                        }
                    };


                    //////////////////////end////////////////////////////



                    /////////////////////////////////pie//////////////////////////////////////////


                    for (var m = 0; m < arrLabels.length; m++) {

                        $scope.dataSourceForPieFromDatabase.push(
                            { day: $scope.labels[m], value: arrData[m] }
                        );


                    }


                    $scope.chartOptionsForPie = {
                        size: {
                            width: 500
                        },
                        palette: "bright",
                        dataSource: $scope.dataSourceForPieFromDatabase,
                        series: [
                            {
                                argumentField: "day",
                                valueField: "value",
                                label: {
                                    visible: true,
                                    connector: {
                                        visible: true,
                                        width: 1
                                    }
                                }
                            }
                        ],
                        //title: "Product Selling Value of Days",
                        //"export": {
                        //    enabled: true
                        //},
                        onPointClick: function (e) {
                            var point = e.target;

                            toggleVisibility(point);
                        },
                        onLegendClick: function (e) {
                            var arg = e.target;

                            toggleVisibility(this.getAllSeries()[0].getPointsByArg(arg)[0]);
                        }
                    };

                    function toggleVisibility(item) {
                        if (item.isVisible()) {
                            item.hide();
                        } else {
                            item.show();
                        }
                    }


                    ///////////////////end////////////////////////



                    ////////////////////Spider//////////////////////


                    for (var n = 0; n < arrLabels.length; n++) {

                        $scope.dataSourceForSpiderForDatabase.push(
                            { arg: $scope.labels[n], number: arrData[n] }
                        );


                    }



                    $scope.polarChartOptionsForSpider = {
                        dataSource: $scope.dataSourceForSpiderForDatabase,
                        useSpiderWeb: true,
                        series: [{ valueField: "number", name: "arg" }
                        ],
                        commonSeriesSettings: {
                            type: "line"
                        },
                        //"export": {
                        //    enabled: true
                        //},
                        //title: "Product Selling Value of Daywise in this week",
                        tooltip: {
                            enabled: true
                        }
                    };



                    /////////////////////end/////////////////////////////




                } else {
                    toastr.error(data.errorMessage);
                }
            }).
            error(function(XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
            });









    ////////////////////////////////////////////end//////////////////////////////////////////////////






    /////////////////////////Start Monthly Day Shift/////////////////////////////////////

        $http({
            method: 'GET',
            url: '/Home/GetMonthlyTotalAmountChartByDayShiftWise'
        }).success(function (data) {
            if (data.success) {


                arrData = data.ValueData;
                arrLabels = data.DayData;

                $scope.data = [];
                $scope.labels = [];


                $scope.data.push(arrData.slice(0));

                for (var i = 0; i < arrLabels.length; i++) {
                    $scope.labels.push(arrLabels[i]);


                }


                /////////////bar/////////////////////////
                for (var j = 0; j < arrLabels.length; j++) {

                    $scope.dataSourceForBarChartMonthlyDayShiftWise.push(
                        { DayForMonth: $scope.labels[j], value: arrData[j] }
                    );


                }
                $scope.barchartOptionsForMonthlyDayShift = {
                    dataSource: $scope.dataSourceForBarChartMonthlyDayShiftWise,
                    series: {
                        argumentField: "DayForMonth",
                        valueField: "value",
                        name: "Day Shift",
                        type: "bar",
                        //color:'#8A2BE2'
                        color: '#48D1CC'
                        
                    },
                
                    //customizePoint: function () {
                      
                    //     if (this.DayForMonth === 10) {
                    //        return { color: "#00CED1", hoverStyle: { color: "#8c8cff" } };
                    //    }
                    //    else if (this.DayForMonth === 11) {
                    //        return { color: "#00BFFF", hoverStyle: { color: "#8c8cff" } };
                    //    }
                    //    else if (this.DayForMonth === 12) {
                    //        return { color: "#00688B"};
                    //    }
                    //}

                    //palette: "soft",
                    //title: {
                    //    text: "Age Breakdown of Facebook Users in the U.S.",
                    //    subtitle: "as of January 2017"
                    //},
                    //commonSeriesSettings: {
                    //    type: "bar",
                    //    valueField: "value",
                    //    argumentField: "DayForMonth"
                    //},
                    //valueAxis: {
                    //    label: {
                    //        format: 'millions'
                    //    }
                    //},
                    //equalBarWidth: false,
                    //seriesTemplate: {
                    //    nameField: "DayForMonth"
                    //}
                };

                /////////////////////////////end////////////////////////////////////

                //////////////////////Line////////////////////////

                for (var p = 0; p < arrLabels.length; p++) {

                    $scope.dataSourceForLineChartForMonthlyDayShiftWise.push(
                        { dayOfMonth: $scope.labels[p], value: arrData[p] }
                    );


                }



                $scope.chartOptionsForLineChartForMonthlyDayShiftWise = {
                    palette: "violet",
                    dataSource: $scope.dataSourceForLineChartForMonthlyDayShiftWise,
                    commonSeriesSettings: {
                        argumentField: "dayOfMonth"
                    },
                    bindingOptions: {
                        "commonSeriesSettings.type": "currentType"
                    },
                    margin: {
                        bottom: 20
                    },
                    argumentAxis: {
                        valueMarginsEnabled: false,
                        discreteAxisDivisionMode: "crossLabels",
                        grid: {
                            visible: true
                        }
                    },
                    series: [
                        { valueField: "value" }

                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                        itemTextPosition: "bottom"
                    },
                    title: {
                        //text: "Selling Total Product number by Daywise In this week",
                        subtitle: {
                            //text: "(Millions of Tons, Oil Equivalent)"
                        }
                    },
                    //"export": {
                    //    enabled: true
                    //},
                    tooltip: {
                        enabled: true,
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.valueText
                            };
                        }
                    }
                };

                $scope.typesOptions = {
                    dataSource: types,
                    bindingOptions: {
                        value: "currentType"
                    }
                };


                //////////////////////end////////////////////////////
       




            } else {
                toastr.error(data.errorMessage);
            }
        }).
                   error(function (XMLHttpRequest, textStatus, errorThrown) {
                       toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                   });










    ///////////////////////////////End////////////////////////////////







    /////////////////////////Start Monthly Night Shift/////////////////////////////////////

        $http({
            method: 'GET',
            url: '/Home/GetMonthlyTotalAmountChartByNightShiftWise'
        }).success(function (data) {
            if (data.success) {


                arrData = data.ValueData;
                arrLabels = data.DayData;

                $scope.data = [];
                $scope.labels = [];


                $scope.data.push(arrData.slice(0));

                for (var i = 0; i < arrLabels.length; i++) {
                    $scope.labels.push(arrLabels[i]);


                }


                /////////////bar/////////////////////////
                for (var j = 0; j < arrLabels.length; j++) {

                    $scope.dataSourceForBarChartMonthlyNightShiftWise.push(
                        { DayForMonth: $scope.labels[j], value: arrData[j] }
                    );


                }
                $scope.barchartOptionsForMonthlyNightShift = {
                    dataSource: $scope.dataSourceForBarChartMonthlyNightShiftWise,
                    series: {
                        argumentField: "DayForMonth",
                        valueField: "value",
                        name: "Night Shift",
                        type: "bar",
                        //color: '#ffaa66'
                        color: '#48D1CC'
                        
                    }
                };

                /////////////////////////////end////////////////////////////////////



                //////////////////////Line////////////////////////

                for (var p = 0; p < arrLabels.length; p++) {

                    $scope.dataSourceForLineChartForMonthlyNightShiftWise.push(
                        { dayOfMonth: $scope.labels[p], value: arrData[p] }
                    );


                }



                $scope.chartOptionsForLineChartForMonthlyNightShiftWise = {
                    palette: "violet",
                    dataSource: $scope.dataSourceForLineChartForMonthlyNightShiftWise,
                    commonSeriesSettings: {
                        argumentField: "dayOfMonth"
                    },
                    bindingOptions: {
                        "commonSeriesSettings.type": "currentType"
                    },
                    margin: {
                        bottom: 20
                    },
                    argumentAxis: {
                        valueMarginsEnabled: false,
                        discreteAxisDivisionMode: "crossLabels",
                        grid: {
                            visible: true
                        }
                    },
                    series: [
                        { valueField: "value" }

                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                        itemTextPosition: "bottom"
                    },
                    title: {
                        //text: "Selling Total Product number by Daywise In this week",
                        subtitle: {
                            //text: "(Millions of Tons, Oil Equivalent)"
                        }
                    },
                    //"export": {
                    //    enabled: true
                    //},
                    tooltip: {
                        enabled: true,
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.valueText
                            };
                        }
                    }
                };

                $scope.typesOptions = {
                    dataSource: types,
                    bindingOptions: {
                        value: "currentType"
                    }
                };


                //////////////////////end////////////////////////////



            } else {
                toastr.error(data.errorMessage);
            }
        }).
                   error(function (XMLHttpRequest, textStatus, errorThrown) {
                       toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                   });










    ///////////////////////////////End////////////////////////////////





    /////////////////////////Start Monthly Total Amount/////////////////////////////////////

        $http({
            method: 'GET',
            url: '/Home/GetMonthlyTotalAmountChart'
        }).success(function (data) {
            if (data.success) {


                arrData = data.ValueData;
                arrLabels = data.DayData;

                $scope.data = [];
                $scope.labels = [];


                $scope.data.push(arrData.slice(0));

                for (var i = 0; i < arrLabels.length; i++) {
                    $scope.labels.push(arrLabels[i]);


                }


                /////////////bar/////////////////////////
                for (var j = 0; j < arrLabels.length; j++) {

                    $scope.dataSourceForBarChartMonthly.push(
                        { DayForMonth: $scope.labels[j], value: arrData[j] }
                    );


                }
                $scope.barchartOptionsForMonthly = {
                    dataSource: $scope.dataSourceForBarChartMonthly,
                    series: {
                        argumentField: "DayForMonth",
                        valueField: "value",
                        name: "Full Day",
                        type: "bar",
                        //color: '#ffaa66'
                        color: '#48D1CC'
                    }
                };

                /////////////////////////////end////////////////////////////////////


                //////////////////////Line////////////////////////

                for (var p = 0; p < arrLabels.length; p++) {

                    $scope.dataSourceForLineChartForMonthlyTotalAmount.push(
                        { dayOfMonth: $scope.labels[p], value: arrData[p] }
                    );


                }



                $scope.chartOptionsForLineChartForMonthlyTotalAmount = {
                    palette: "violet",
                    dataSource: $scope.dataSourceForLineChartForMonthlyTotalAmount,
                    commonSeriesSettings: {
                        argumentField: "dayOfMonth"
                    },
                    bindingOptions: {
                        "commonSeriesSettings.type": "currentType"
                    },
                    margin: {
                        bottom: 20
                    },
                    argumentAxis: {
                        valueMarginsEnabled: false,
                        discreteAxisDivisionMode: "crossLabels",
                        grid: {
                            visible: true
                        }
                    },
                    series: [
                        { valueField: "value" }

                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                        itemTextPosition: "bottom"
                    },
                    title: {
                        //text: "Selling Total Product number by Daywise In this week",
                        subtitle: {
                            //text: "(Millions of Tons, Oil Equivalent)"
                        }
                    },
                    //"export": {
                    //    enabled: true
                    //},
                    tooltip: {
                        enabled: true,
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.valueText
                            };
                        }
                    }
                };

                $scope.typesOptions = {
                    dataSource: types,
                    bindingOptions: {
                        value: "currentType"
                    }
                };


                //////////////////////end////////////////////////////




            } else {
                toastr.error(data.errorMessage);
            }
        }).
                   error(function (XMLHttpRequest, textStatus, errorThrown) {
                       toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                   });










    ///////////////////////////////End////////////////////////////////



    /////////////////////////Start Weekly Day Shift/////////////////////////////////////

        $http({
            method: 'GET',
            url: '/Home/GetWeeklyTotalAmountChartByDayShiftWise'
        }).success(function (data) {
            if (data.success) {


                arrData = data.ValueData;
                arrLabels = data.DayData;

                $scope.data = [];
                $scope.labels = [];


                $scope.data.push(arrData.slice(0));

                for (var i = 0; i < arrLabels.length; i++) {
                    $scope.labels.push(arrLabels[i]);


                }


                /////////////bar/////////////////////////
                for (var j = 0; j < arrLabels.length; j++) {

                    $scope.dataSourceForBarChartWeeklyDayShiftWise.push(
                        { DayForWeek: $scope.labels[j], value: arrData[j] }
                    );


                }
                $scope.barchartOptionsForWeeklyDayShift = {
                    dataSource: $scope.dataSourceForBarChartWeeklyDayShiftWise,
                    series: {
                        argumentField: "DayForWeek",
                        valueField: "value",
                        name: "Day Shift",
                        type: "bar",
                        //color:'#8A2BE2'
                        color: '#48D1CC'

                    },

                    //customizePoint: function () {

                    //     if (this.DayForMonth === 10) {
                    //        return { color: "#00CED1", hoverStyle: { color: "#8c8cff" } };
                    //    }
                    //    else if (this.DayForMonth === 11) {
                    //        return { color: "#00BFFF", hoverStyle: { color: "#8c8cff" } };
                    //    }
                    //    else if (this.DayForMonth === 12) {
                    //        return { color: "#00688B"};
                    //    }
                    //}

                    //palette: "soft",
                    //title: {
                    //    text: "Age Breakdown of Facebook Users in the U.S.",
                    //    subtitle: "as of January 2017"
                    //},
                    //commonSeriesSettings: {
                    //    type: "bar",
                    //    valueField: "value",
                    //    argumentField: "DayForWeek"
                    //},
                    //valueAxis: {
                    //    label: {
                    //        format: 'millions'
                    //    }
                    //},
                    //equalBarWidth: false,
                    //seriesTemplate: {
                    //    nameField: "DayForWeek"
                    //}
                };

                /////////////////////////////end////////////////////////////////////

                //////////////////////Line////////////////////////

                for (var p = 0; p < arrLabels.length; p++) {

                    $scope.dataSourceForLineChartForWeeklyDayShiftWise.push(
                        { dayOfWeek: $scope.labels[p], value: arrData[p] }
                    );


                }



                $scope.chartOptionsForLineChartForWeeklyDayShiftWise = {
                    palette: "violet",
                    dataSource: $scope.dataSourceForLineChartForWeeklyDayShiftWise,
                    commonSeriesSettings: {
                        argumentField: "dayOfWeek"
                    },
                    bindingOptions: {
                        "commonSeriesSettings.type": "currentType"
                    },
                    margin: {
                        bottom: 20
                    },
                    argumentAxis: {
                        valueMarginsEnabled: false,
                        discreteAxisDivisionMode: "crossLabels",
                        grid: {
                            visible: true
                        }
                    },
                    series: [
                        { valueField: "value" }

                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                        itemTextPosition: "bottom"
                    },
                    title: {
                        //text: "Selling Total Product number by Daywise In this week",
                        subtitle: {
                            //text: "(Millions of Tons, Oil Equivalent)"
                        }
                    },
                    //"export": {
                    //    enabled: true
                    //},
                    tooltip: {
                        enabled: true,
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.valueText
                            };
                        }
                    }
                };

                $scope.typesOptions = {
                    dataSource: types,
                    bindingOptions: {
                        value: "currentType"
                    }
                };


                //////////////////////end////////////////////////////





            } else {
                toastr.error(data.errorMessage);
            }
        }).
                   error(function (XMLHttpRequest, textStatus, errorThrown) {
                       toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                   });







    ///////////////////////////////End////////////////////////////////



    /////////////////////////Start Weekly Night Shift/////////////////////////////////////

        $http({
            method: 'GET',
            url: '/Home/GetWeeklyTotalAmountChartByNightShiftWise'
        }).success(function (data) {
            if (data.success) {


                arrData = data.ValueData;
                arrLabels = data.DayData;

                $scope.data = [];
                $scope.labels = [];


                $scope.data.push(arrData.slice(0));

                for (var i = 0; i < arrLabels.length; i++) {
                    $scope.labels.push(arrLabels[i]);


                }


                /////////////bar/////////////////////////
                for (var j = 0; j < arrLabels.length; j++) {

                    $scope.dataSourceForBarChartWeeklyNightShiftWise.push(
                        { DayForWeek: $scope.labels[j], value: arrData[j] }
                    );


                }
                $scope.barchartOptionsForWeeklyNightShift = {
                    dataSource: $scope.dataSourceForBarChartWeeklyNightShiftWise,
                    series: {
                        argumentField: "DayForWeek",
                        valueField: "value",
                        name: "Night Shift",
                        type: "bar",
                        //color: '#ffaa66'
                        color: '#48D1CC'

                    }
                };

                /////////////////////////////end////////////////////////////////////



                //////////////////////Line////////////////////////

                for (var p = 0; p < arrLabels.length; p++) {

                    $scope.dataSourceForLineChartForWeeklyNightShiftWise.push(
                        { dayOfWeek: $scope.labels[p], value: arrData[p] }
                    );


                }



                $scope.chartOptionsForLineChartForWeeklyNightShiftWise = {
                    palette: "violet",
                    dataSource: $scope.dataSourceForLineChartForWeeklyNightShiftWise,
                    commonSeriesSettings: {
                        argumentField: "dayOfWeek"
                    },
                    bindingOptions: {
                        "commonSeriesSettings.type": "currentType"
                    },
                    margin: {
                        bottom: 20
                    },
                    argumentAxis: {
                        valueMarginsEnabled: false,
                        discreteAxisDivisionMode: "crossLabels",
                        grid: {
                            visible: true
                        }
                    },
                    series: [
                        { valueField: "value" }

                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                        itemTextPosition: "bottom"
                    },
                    title: {
                        //text: "Selling Total Product number by Daywise In this week",
                        subtitle: {
                            //text: "(Millions of Tons, Oil Equivalent)"
                        }
                    },
                    //"export": {
                    //    enabled: true
                    //},
                    tooltip: {
                        enabled: true,
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.valueText
                            };
                        }
                    }
                };

                $scope.typesOptions = {
                    dataSource: types,
                    bindingOptions: {
                        value: "currentType"
                    }
                };


                //////////////////////end////////////////////////////



            } else {
                toastr.error(data.errorMessage);
            }
        }).
                   error(function (XMLHttpRequest, textStatus, errorThrown) {
                       toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                   });










    ///////////////////////////////End////////////////////////////////





    /////////////////////////Start Weekly Total Amount/////////////////////////////////////

        $http({
            method: 'GET',
            url: '/Home/GetWeeklyTotalAmountChart'
        }).success(function (data) {
            if (data.success) {


                arrData = data.ValueData;
                arrLabels = data.DayData;

                $scope.data = [];
                $scope.labels = [];


                $scope.data.push(arrData.slice(0));

                for (var i = 0; i < arrLabels.length; i++) {
                    $scope.labels.push(arrLabels[i]);


                }


                /////////////bar/////////////////////////
                for (var j = 0; j < arrLabels.length; j++) {

                    $scope.dataSourceForBarChartWeekly.push(
                        { DayForWeek: $scope.labels[j], value: arrData[j] }
                    );


                }
                $scope.barchartOptionsForWeekly = {
                    dataSource: $scope.dataSourceForBarChartWeekly,
                    series: {
                        argumentField: "DayForWeek",
                        valueField: "value",
                        name: "Full Day",
                        type: "bar",
                        //color: '#ffaa66'
                        color: '#48D1CC'
                    }
                };

                /////////////////////////////end////////////////////////////////////


                //////////////////////Line////////////////////////

                for (var p = 0; p < arrLabels.length; p++) {

                    $scope.dataSourceForLineChartForWeeklyTotalAmount.push(
                        { dayOfWeek: $scope.labels[p], value: arrData[p] }
                    );


                }



                $scope.chartOptionsForLineChartForWeeklyTotalAmount = {
                    palette: "violet",
                    dataSource: $scope.dataSourceForLineChartForWeeklyTotalAmount,
                    commonSeriesSettings: {
                        argumentField: "dayOfWeek"
                    },
                    bindingOptions: {
                        "commonSeriesSettings.type": "currentType"
                    },
                    margin: {
                        bottom: 20
                    },
                    argumentAxis: {
                        valueMarginsEnabled: false,
                        discreteAxisDivisionMode: "crossLabels",
                        grid: {
                            visible: true
                        }
                    },
                    series: [
                        { valueField: "value" }

                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                        itemTextPosition: "bottom"
                    },
                    title: {
                        //text: "Selling Total Product number by Daywise In this week",
                        subtitle: {
                            //text: "(Millions of Tons, Oil Equivalent)"
                        }
                    },
                    //"export": {
                    //    enabled: true
                    //},
                    tooltip: {
                        enabled: true,
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.valueText
                            };
                        }
                    }
                };

                $scope.typesOptions = {
                    dataSource: types,
                    bindingOptions: {
                        value: "currentType"
                    }
                };


                //////////////////////end////////////////////////////




            } else {
                toastr.error(data.errorMessage);
            }
        }).
                   error(function (XMLHttpRequest, textStatus, errorThrown) {
                       toastr.error(XMLHttpRequest + ": " + textStatus + ": " + errorThrown, 'Error!!!');
                   });










    ///////////////////////////////End////////////////////////////////







    $scope.polarChartOptions = {
        dataSource: dataSourceForPolar,
        series: [{ type: "line" }],
        legend: {
            visible: false
        },
        argumentAxis: {
            inverted: true,
            startAngle: 90,
            tickInterval: 30
        },
        //"export": {
        //    enabled: true
        //},
        title: "Rose in Polar Coordinates"
    };










    $(function () {
        var highAverage = 77,
            lowAverage = 58;

        $("#chartForMonthly").dxChart({
            dataSource: dataSourceForMonthly,
            commonSeriesSettings: {
                argumentField: "day",
                valueField: "temperature",
                type: "bar",
                color: "#e7d19a"
            },
            customizePoint: function () {
                if (this.value > highAverage) {
                    return { color: "#ff7c7c", hoverStyle: { color: "#ff7c7c" } };
                } else if (this.value < lowAverage) {
                    return { color: "#8c8cff", hoverStyle: { color: "#8c8cff" } };
                }
            },
            customizeLabel: function () {
                if (this.value > highAverage) {
                    return {
                        visible: true,
                        backgroundColor: "#ff7c7c",
                        customizeText: function () {
                            return this.valueText + "&#176F";
                        }
                    };
                }
            },
            //"export": {
            //    enabled: true
            //},
            valueAxis: {
                min: 40,
                maxValueMargin: 0.1,
                label: {
                    customizeText: function () {
                        return this.valueText + "&#176F";
                    }
                },
                constantLines: [{
                    label: {
                        text: "Low Average"
                    },
                    width: 2,
                    value: lowAverage,
                    color: "#8c8cff",
                    dashStyle: "dash"
                }, {
                    label: {
                        text: "High Average"
                    },
                    width: 2,
                    value: highAverage,
                    color: "#ff7c7c",
                    dashStyle: "dash"
                }]
            },
            series: [{}],
            title: {
                //text: "Daily Temperature in May"
            },
            legend: {
                visible: false
            }
        });
    });







});
