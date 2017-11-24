angular.module('myApp')
.service('dateconfigservice', function () {
    this.FullDateUKtoDateKey = function (strDate) {
        return strDate.substring(6, 10) + strDate.substring(3, 5) + strDate.substring(0, 2)
    };
    this.DateTimeToDate = function (strDate) {
        return strDate.substring(8, 10) + '/' + strDate.substring(5, 7) + '/' + strDate.substring(0, 4);
    };
    this.UIDateToServerDate = function (strDate) {
        var ServerDate = new Date(strDate.substring(6, 10) + '-' + strDate.substring(3, 5) + '-' + strDate.substring(0, 2));
        return ServerDate;
    };
});