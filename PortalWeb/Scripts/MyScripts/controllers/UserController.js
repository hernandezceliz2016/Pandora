var userController = function ($scope, $http, $location, $window, UserService) {
    var user = {};
    $scope.documento = "45019503";
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
            $scope.usuario.mensaje = response.data.Data;
            $scope.mensaje.alert();
            alert("hola");
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
            debugger;
            $window.location.href = response.data.Url;
        }, function myError(response) {
            debugger;
            $scope.myWelcome = response.statusText;
        });
    };

    $scope.FnModificarUser = function () {
        debugger;
        user.Apellido = $scope.usuario.apellido;
        user.Nombre = $scope.usuario.nombre;
        user.Nombres = $scope.usuario.apellido + ' ' + $scope.usuario.nombre;
        user.Dni = $scope.usuario.dni;
        user.Usua = $scope.usuario.user;
        user.Pass = $scope.usuario.pass;
        user.Email = $scope.usuario.email;
    };

    $scope.FnBusarUserPorDni = function () {
        var url = "BuscarPorDni/?strDni=" + $scope.documentoBusqueda;
        return $http.get(url).then(function (results) {
            debugger;
            var data = results.data.data;
            if (data.Dni !== "") {
                $scope.apellido = data.Apellido;
                $scope.usuario.nombre = data.Nombre;
                $scope.usuario.dni = data.Dni;
                $scope.usuario.user = data.Usua;
                $scope.usuario.pass = data.Pass;
                $scope.usuario.email = data.Email;
            } else {
                alert("NO SE ENCONTRO EL USUARIO");
            }
        });
    };

}
userController.$inject = ['$scope', '$http', '$location', '$window', 'UserService'];
