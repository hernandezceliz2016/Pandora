angular.module('MyApp', [])

.directive('ngFile', ['$parse', function ($parse) {
    
    function fn_link(scope, element, attrs) {
        var onChange = $parse(attrs.ngFile);

        element.on('change', function (event) {           
            onChange(scope, { $files: event.target.files });
        })
    }
    return {
        link: fn_link
    }
}])

.controller('Part8Controller', function ($scope, $http, FileUploadService) {
    $scope.Message = "";
    $scope.FileInvalidMessage = "";
    $scope.SelectedFileForUpload = null;
    $scope.FileDescription = "";
    $scope.IsFormSubmitted = false;
    $scope.IsFileValid = false;
    $scope.IsFormValid = false;
    $scope.gugo = "";

    $scope.$watch("f1.$valid", function (isValid) {
        $scope.IsFormValid = isValid;
    });


    $scope.getThefile = function ($files) {
        debugger;
        $scope.imagesrc = [];

        //imagesrc.nombreG = $files[0].name;
        //imagesrc.sizeG = $files[0].size;
        //imagesrc.typeG = $files[0].type;

        var reader = new FileReader();
        $scope.gugo = $files[0].name;

        reader.onload = function (event) {
            var image = {};
            image.Name = event.target.fileName
            image.Size = (event.total / 1024).toFixed(2);
            image.Src = event.target.result;
            $scope.imagesrc.push(image);
            $scope.$apply();
        }
        reader.readAsDataURL($files[0]);
    };

    
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