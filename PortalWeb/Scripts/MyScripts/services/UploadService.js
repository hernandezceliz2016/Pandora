(function () {
    var injectParams = ['$http', '$q'];
    var factory = function ($http, $q) {
        var service = {};

        service.uploadFile = function (name, file) {
            debugger;
            var deferred = $q.defer();
            var formData = new FormData();
            formData.append("name", name);
            formData.append("file", file);
            return $http.post("./api/Files", {
                headers: {
                    "Content-type": undefined
                },
                transformRequest: formData
            })
            .success(function (res) {
                deferred.resolve(res);
            })
            .error(function (msg, code) {
                deferred.reject(msg);
            })
            return deferred.promise;
        }
        return service;
    };
    factory.$inject = injectParams;
    angular.module('awApp').factory('UploadService', factory);
}());