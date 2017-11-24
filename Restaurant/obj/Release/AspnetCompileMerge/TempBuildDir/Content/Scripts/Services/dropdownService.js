angular.module('myApp')
.service('dropdownservice', function () {
    this.getSelectedText = function (dropdown, value) {
        return $.grep(dropdown, function (ddl) {
            return ddl.value == value;
        })[0].text;
    };

});