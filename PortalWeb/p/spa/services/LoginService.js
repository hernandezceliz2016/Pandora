(function () {
    var injectParams = ['$http', '$q'];
    var factory = function ($http, $q) {
        var service = {};
        
        //service.GetProductos = function (nombre) {
        //    var url = "Product/BusquedaService";
        //    return $http.get(url).then(function (results) {
        //        var data = results.data;
        //        return data;
        //    });
        //};
        return service;
    };
    factory.$inject = injectParams;
    angular.module('awApp').factory('LoginService', factory);
}());