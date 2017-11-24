// Common directive for Focus
angular.module('myApp').directive('onloadFocus',
	function ($timeout) {
	    return {
	        scope: {
	            trigger: '@onloadFocus'
	        },
	        link: function (scope, element) {
	            scope.$watch('trigger', function (value) {
	                if (value === "true") {
	                    $timeout(function () {
	                        element[0].focus();
	                    });
	                }
	            });
	        }
	    };
	}
);


// Common directive for Focus on next field when enter key pressed
angular.module('myApp').directive('enterAsTab', 
	function () {
	    return function (scope, element, attrs) {
	        element.bind("keydown keypress", function (event) {
	            if (event.which === 13) {
	                event.preventDefault();
	                var fields = $(this).parents('form:eq(0),body').find('input, textarea, select');
	                var index = fields.index(this);
	                if (index > -1 && (index + 1) < fields.length)
	                    fields.eq(index + 1).focus();
	            }
	        });
	    };
	});


// Reload page
angular.module('myApp').directive('refresh',
	function ($window, $location) {
	    return function (scope, element, attrs) {
	        element.bind('click', function () {
	            $window.location.reload();
	        });
	    }
	});

angular.module('myApp').directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9-.]/g, '');
                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            } 
            ngModelCtrl.$parsers.push(fromUser);
        }
    };   
});



angular.module('myApp').directive('positiveNumbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9.]/g, '');
                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});


angular.module('myApp').directive('ncgRequestVerificationToken', ['$http', function ($http) {
    return function (scope, element, attrs) {
        $http.defaults.headers.common['RequestVerificationToken'] = attrs.ncgRequestVerificationToken || "no request verification token";
    };
}]);

// added by shohid

angular.module('myApp').directive('rangeValidate', function () {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$parsers.unshift(function (viewValue) {

                var maxLangth = attrs.ngMaxlength;
                var minLangth = attrs.ngMinlength;
                var maxValue = attrs.ngMaxvalue;
                var minValue = attrs.ngMinvalue;
                var inputLen = viewValue.length;
                var inputVal = viewValue;
                if ((maxValue && minValue) != undefined)
                {
                    if (parseInt(maxValue) >= parseInt(inputVal) && parseInt(minValue) <= parseInt(inputVal)) {
                        ctrl.$setValidity('num', true);
                        return viewValue;
                    }
                    else {
                        ctrl.$setValidity('num', false)
                        return undefined;
                    }
                }
                if ((maxLangth && minLangth) != undefined)
                {
                    if (parseInt(maxLangth) >= parseInt(inputLen) && parseInt(minLangth) <= parseInt(inputLen)) {
                        ctrl.$setValidity('text', true);
                        return viewValue;
                    }
                    else {
                        ctrl.$setValidity('text', false);
                        return undefined;
                    }
                }  

            });
        }
    };
});


angular.module('MyModule', []).directive('autoComplete', function ($timeout) {
    return function (scope, iElement, iAttrs) {
        iElement.autocomplete({
            source: scope[iAttrs.uiItems],
            select: function () {
                $timeout(function () {
                    iElement.trigger('input');
                }, 0);
            }
        });
    };
});
