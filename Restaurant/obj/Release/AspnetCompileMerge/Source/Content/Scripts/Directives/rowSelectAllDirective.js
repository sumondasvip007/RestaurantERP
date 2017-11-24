angular.module('myApp')
    .directive('rowSelectAll', function () {
        return {
            require: '^stTable',
            template: '<input type="checkbox">',
            scope: {
                all: '=rowSelectAll',
                selected: '='
            },
            link: function (scope, element, attr) {

                scope.isAllSelected = true;

                element.bind('click', function (evt) {

                    scope.$apply(function () {

                        //scope.all.forEach(function (val) {

                        //    val.isSelected = scope.isAllSelected;

                        //});
                        for (var i = 0; i < scope.all.length; i++) {
                            //$scope.selected.push(collection[i].id);
                            scope.all[i].isSelected = scope.isAllSelected;
                        }

                    });

                });

                scope.$watchCollection('selected', function (newVal) {

                    var s = newVal.length;
                    var a = scope.all.length;

                    if ((s == a) && s > 0 && a > 0) {

                        //element.find('input').attr('checked', true);
                        element.find('input').prop('checked', true);
                        scope.isAllSelected = false;

                    } else {

                        //element.find('input').attr('checked', false);
                        element.find('input').prop('checked', false);
                        scope.isAllSelected = true;

                    }

                });
            }
        }
    });