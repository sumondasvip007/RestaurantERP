angular.module('myApp').directive('removeStyle', function () {
    return {
        link: function (scope, element, attrs) {
            element.removeClass("md-button md-ink-ripple");
        }
    };
});