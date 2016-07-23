var userController = function ($scope, $http, $location, $window, UserService) {
    var user = {};

    $scope.documentoBusqueda = "45019503";


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
            if (response.data.Estado === 1) {
                $window.location.href = response.data.Url;
            } else if (response.data.Estado === 0) {
                $scope.inputMensaje = response.data.strMensaje;
            }
            debugger;

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

    $scope.FnLimpiarControles = function () {
        debugger;
        $scope.apellido = "";
        $scope.nombre = "";
        $scope.dni = "";
        $scope.user = "";
        $scope.pass = "";
        $scope.email = "";
        $scope.passConfir = "";
        $scope.mensajeBusqUser = "";
    };

    $scope.FnBusarUserPorDni = function () {
        $scope.FnLimpiarControles();
        var url = "BuscarPorDni/?strDni=" + $scope.documentoBusqueda;
        return $http.get(url).then(function (results) {
            debugger;
            var estado = results.data.Estado;
            if (estado === 1) {
                var data = results.data.Data;
                $scope.apellido = data.Apellido;
                $scope.nombre = data.Nombre;
                $scope.dni = data.Dni;
                $scope.user = data.Usua;
                $scope.pass = data.Pass;
                $scope.email = data.Email;
                $scope.passConfir = data.Pass;
            } else if (estado === 0) {
                $scope.mensajeBusqUser = results.data.strMensaje;
            }
        });
    };



}
userController.$inject = ['$scope', '$http', '$location', '$window', 'UserService'];
