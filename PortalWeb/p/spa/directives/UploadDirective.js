(function () {
    var injectParams = ['$http', '$q','$parse'];
    debugger;
    var uploaderModel = function ($http, $q, $parse) {

        return {
            restrict: 'A',
            link: function (scope, iElement, iAttrs) {
                iElement.on('change', function (e) {                    
                    $parse(iAttrs.uploaderModel).assign(scope, iElement[0].files[0]);
                });
            }
        };
    };
    uploaderModel.$inject = injectParams;
    angular.module('awApp').directive('uploaderModel', uploaderModel);
}());