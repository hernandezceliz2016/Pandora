var userController = function ($scope, $http, $location, $window, UserService) {
    var user = {};
    $scope.RegistrarUsuario = function () {

        user.Apellido = $scope.usuario.apellidos;
        user.Nombre = $scope.usuario.nombres;
        user.Nombres = $scope.usuario.apellidos + ' ' + $scope.usuario.nombres;
        user.Dni = $scope.usuario.documento;
        user.Usua = $scope.usuario.usuario;
        user.Pass = $scope.usuario.pass;
        user.Email = $scope.usuario.emails;
        user.FechExpiracion = $scope.usuario.fecha;
        $http({
            method: "POST",
            url: "/User/Guardar",
            headers: { 'Content-Type': 'application/json' },
            data: user
        }).then(function mySucces(response) {
            debugger;
            $window.location.href = response.data.Url;


        }, function myError(response) {
            debugger;
            $scope.myWelcome = response.statusText;
            $scope.usuario.mensaje = response.data;
        });
    };

    $scope.FnIngresar = function () {
        user.Usua = $scope.inputUsuario;
        user.Pass = $scope.inputPassword;
        $http({
            method: "POST",
            url: "/User/Login",
            headers: { 'Content-Type': 'application/json' },
            data: user
        }).then(function mySucces(response) {
            $window.location.href = response.data.Url;
        }, function myError(response) {
            $scope.myWelcome = response.statusText;
        });
    };
}
userController.$inject = ['$scope', '$http', '$location', '$window', 'UserService'];
