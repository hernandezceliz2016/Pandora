(function () {
    var injectParams = ['$scope', '$location', '$routeParams', 'UploadService'];
    var UploadController = function ($scope, $location, $routeParams,
    UploadService) {
        
        $scope.uploadFile = function()
        {
            debugger;
            var name = $scope.name;
            var file = $scope.file;
            console.log(name);
            console.log(file);
            UploadService.uploadFile(name, file).then(function (data) {
                console.log(data);
            }, function (data) {
                console.log('gugo');
            });
        }    
    };  

    var uploaderModel = function ($parse) {

        return {
            restrict: 'A',
            link: function (scope, iElement, iAttrs) {
                iElement.on('change', function (e) {                    
                    $parse(iAttrs.uploaderModel).assign(scope, iElement[0].files[0]);
                });
            }
        };
    };

    UploadController.$inject = injectParams;
    angular.module('awApp').controller('UploadController', UploadController);
    angular.module('awApp').directive('uploaderModel', uploaderModel);

}());