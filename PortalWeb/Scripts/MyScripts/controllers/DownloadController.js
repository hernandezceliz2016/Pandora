angular.module('MyAppD', [])

.controller('DownloadController', function ($scope, $http) {
   
    $scope.verData = function () {
        var url = "/Data/ListarFiles";
        return $http.get(url).then(function (results) {
            debugger;
            $scope.FilesData = results.data;
        });
    };

    $scope.makeUrl = function () {
        debugger;
        window.location = "http://localhost:25005/";
        //return "UploadedFiles/3bd8e847-a7d3-4080-a91d-fa95db35fff7.txt"
    };
    
})
