﻿(function() {
        var injectParams = ['$http', '$eleme', '$att', '$ctrl'];
        var numbersOnly = function($http, $eleme, $att, $ctrl) {
            return {
                restrict: 'A',
                require: 'ngModel',
                link: function(scope, element, attrs, ctrl) {
                    var validateNumber = function(inputValue) {
                        var maxLength = 6;
                        if (attrs.max) {
                            maxLength = attrs.max;
                        }
                        if (inputValue === undefined) {
                            return '';
                        }
                        var transformedInput = inputValue.replace(/[^0-9]/g, '');
                        if (transformedInput !== inputValue) {
                            ctrl.$setViewValue(transformedInput);
                            ctrl.$render();
                        }
                        if (transformedInput.length > maxLength) {
                            transformedInput = transformedInput.substring(0, maxLength);
                            ctrl.$setViewValue(transformedInput);
                            ctrl.$render();
                        }
                        var isNotEmpty = (transformedInput.length === 0) ? true : false;
                        ctrl.$setValidity('notEmpty', isNotEmpty);
                        return transformedInput;
                    };

                    ctrl.$parsers.unshift(validateNumber);
                    ctrl.$parsers.push(validateNumber);
                    attrs.$observe('notEmpty',
                        function() {
                            validateNumber(ctrl.$viewValue);
                        });
                }
            };
        };
        numbersOnly.$inject = injectParams;
        angular.module('newApp').directive('numbersOnly', numbersOnly);

    }
    ()
);