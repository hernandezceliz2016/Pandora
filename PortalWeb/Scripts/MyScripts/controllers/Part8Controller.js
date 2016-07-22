angular.module('MyApp', [])
.controller('Part8Controller', function ($scope, FileUploadService) {
    $scope.Message = "";
    $scope.FileInvalidMessage = "";
    $scope.SelectedFileForUpload = null;
    $scope.FileDescription = "";
    $scope.IsFormSubmitted = false;
    $scope.IsFileValid = false;
    $scope.IsFormValid = false;

    $scope.$watch("f1.$valid", function (isValid) {
        $scope.IsFormValid = isValid;
    });

    $scope.ChechFileValid = function (file) {
        var isValid = false;
        debugger;
        if ($scope.SelectedFileForUpload !== null) {
            if ((file.type === 'text/plain') && file.size <= (512 * 1024)) {
                $scope.FileInvalidMessage = "";
                isValid = true;
            }
            else {
                $scope.FileInvalidMessage = "Selecionar archivos planos (solo archivos txt y 512 kb tamaño máximo)";
            }
        } else {
            $scope.FileInvalidMessage = "Archivo requerido";
        }
        $scope.IsFileValid = isValid;
    }

    $scope.SelectedFileForUpload = function (file) {
        $scope.SelectedFileForUpload = file[0];

    }

    $scope.SaveFile = function () {
        $scope.IsFormSubmitted = true;
        $scope.Message = "";
        $scope.ChechFileValid($scope.SelectedFileForUpload);
        if ($scope.IsFormValid && $scope.IsFileValid) {
            FileUploadService.UploadFile($scope.SelectedFileForUpload, $scope.FileDescription).then(function (d) {
                alert(d.Message);               
                ClearForm();                
            }, function (e) {
                alert(e);
            });
        } else {
            $scope.Message = "Adjunta archivo y mensaje ya que son requeridas !";
        }
    };

    function ClearForm() {
        $scope.FileDescription = "";
        angular.forEach(angular.element("input[type='file']"), function (inputElem) {
            angular.element(inputElem).val(null);
        });
        $scope.f1.$setPristine();
        $scope.IsFormSubmitted = false;
    }
})
.factory('FileUploadService', function ($http, $q) {

    var fac = {};
    fac.UploadFile = function (file, description) {
        var formData = new FormData();
        formData.append("file", file);
        formData.append("description", description);

        var defer = $q.defer();
        $http.post('/Data/SaveFiles', formData,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
        .success(function (d) {
            defer.resolve(d);
        })
        .error(function () {
            defer.reject("File Upload Failed!!");
        });
        return defer.promise;
    }
    return fac;
});