angular.module('myApp')
.service('isundefinedornullservice', function () {
    this.isUndefinedOrNull = function (val) {
        return angular.isUndefined(val) || val === null
    };

});