(function () {
    var injectParams = ['$scope', '$location', '$routeParams', 'LoginService'];
    var LoginController = function ($scope, $location, $routeParams,
    LoginService) {
        $scope.listaProductos = {};
        $scope.MostrarMensaje = function () {
            alert("gugo");
        };

        //UsuarioController.$inject = ['$scope'];

        //function UsuarioController($scope) {
        //    $scope.title = 'UsuarioController';

        //    activate();

        //    function activate() { }
        //}
    };
    LoginController.$inject = injectParams;
    angular.module('awApp').controller('LoginController', LoginController);
}());