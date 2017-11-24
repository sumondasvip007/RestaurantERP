angular.module('myApp')
.service('globalvalueservice', function () {
    this.getProcessDate = function () {
        return $('#processDate').val().substring(0, 10)
    };

});