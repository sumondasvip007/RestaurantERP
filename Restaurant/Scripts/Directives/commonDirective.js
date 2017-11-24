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

angular.module('myApp').directive(
    'dateInput',
    function(dateFilter) {
        return {
            require: 'ngModel',
            template: '<input type="date"></input>',
            replace: true,
            link: function(scope, elm, attrs, ngModelCtrl) {
                ngModelCtrl.$formatters.unshift(function (modelValue) {
                    return dateFilter(modelValue, 'yyyy-MM-dd');
                });

                ngModelCtrl.$parsers.unshift(function(viewValue) {
                    return new Date(viewValue);
                });
            },
        };
    });

//angular.module('myApp', ['ngCart.fulfilment'])
//.directive('ngcartAddtocart', ['ngCart', function(ngCart){
//    return {
//        restrict : 'E',
//        controller : 'CartController',
//        scope: {
//            id:'@',
//            name:'@',
//            quantity:'@',
//            quantityMax:'@',
//            price:'@',
//            data:'='
//        },
//        transclude: true,
//        templateUrl: function(element, attrs) {
//            if ( typeof attrs.templateUrl == 'undefined' ) {
//                return 'template/ngCart/addtocart.html';
//            } else {
//                return attrs.templateUrl;
//            }
//        },
//        link:function(scope, element, attrs){
//            scope.attrs = attrs;
//            scope.inCart = function(){
//                return  ngCart.getItemById(attrs.id);
//            };

//            if (scope.inCart()){
//                scope.q = ngCart.getItemById(attrs.id).getQuantity();
//            } else {
//                scope.q = parseInt(scope.quantity);
//            }

//            scope.qtyOpt =  [];
//            for (var i = 1; i <= scope.quantityMax; i++) {
//                scope.qtyOpt.push(i);
//            }

//        }

//    };
//}])

//   .directive('ngcartCart', [function(){
//       return {
//           restrict : 'E',
//           controller : 'CartController',
//           scope: {},
//           templateUrl: function(element, attrs) {
//               if ( typeof attrs.templateUrl == 'undefined' ) {
//                   return 'template/ngCart/cart.html';
//               } else {
//                   return attrs.templateUrl;
//               }
//           },
//           link:function(scope, element, attrs){

//           }
//       };
//   }])

//   .directive('ngcartSummary', [function(){
//       return {
//           restrict : 'E',
//           controller : 'CartController',
//           scope: {},
//           transclude: true,
//           templateUrl: function(element, attrs) {
//               if ( typeof attrs.templateUrl == 'undefined' ) {
//                   return 'template/ngCart/summary.html';
//               } else {
//                   return attrs.templateUrl;
//               }
//           }
//       };
//   }])

//   .directive('ngcartCheckout', [function(){
//       return {
//           restrict : 'E',
//           controller : ('CartController', ['$rootScope', '$scope', 'ngCart', 'fulfilmentProvider', function($rootScope, $scope, ngCart, fulfilmentProvider) {
//               $scope.ngCart = ngCart;

//               $scope.checkout = function () {
//                   fulfilmentProvider.setService($scope.service);
//                   fulfilmentProvider.setSettings($scope.settings);
//                   fulfilmentProvider.checkout()
//                       .success(function (data, status, headers, config) {
//                           $rootScope.$broadcast('ngCart:checkout_succeeded', data);
//                       })
//                       .error(function (data, status, headers, config) {
//                           $rootScope.$broadcast('ngCart:checkout_failed', {
//                               statusCode: status,
//                               error: data
//                           });
//                       });
//               }
//           }]),
//           scope: {
//               service:'@',
//               settings:'='
//           },
//           transclude: true,
//           templateUrl: function(element, attrs) {
//               if ( typeof attrs.templateUrl == 'undefined' ) {
//                   return 'template/ngCart/checkout.html';
//               } else {
//                   return attrs.templateUrl;
//               }
//           }
//       };
//   }]);
