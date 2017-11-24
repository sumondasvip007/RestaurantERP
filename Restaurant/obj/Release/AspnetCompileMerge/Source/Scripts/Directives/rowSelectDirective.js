angular.module('myApp')
    .directive('rowSelect', function () {
        return {
            require: '^stTable',
            template: '<input type="checkbox">',
            scope: {
                row: '=rowSelect'
            },
            link: function (scope, element, attr, ctrl) {

                element.bind('click', function (evt) {

                    scope.$apply(function () {

                        ctrl.select(scope.row, 'multiple');

                    });

                });

                scope.$watch('row.isSelected', function (newValue,oldValue) {

                    if (newValue === true || (newValue === false && oldValue===undefined)) {

                        element.parent().addClass('st-selected');
                        //element.find('input').attr('checked', true);
                        element.find('input').prop('checked', true);

                    } else {

                        element.parent().removeClass('st-selected');
                        //element.find('input').attr('checked', false);
                        element.find('input').prop('checked', false);

                    }
                });
            }
        }
    });