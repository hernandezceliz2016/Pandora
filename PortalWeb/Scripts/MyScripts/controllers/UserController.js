    var userController = function ($scope, $http, $location, $window, UserService) {
    var user = {};
    debugger;
    $scope.UserLogin = "";
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
                $scope.UserLogin = response.data.User;
            } else if (response.data.Estado === 0) {
                $scope.inputMensaje = response.data.strMensaje;
            }
            debugger;

        }, function myError(response) {
            debugger;
            $scope.myWelcome = response.statusText;
        });
    };

    var init = function () {
        var url = "ObtenerUserLogin";
        $http.get(url).then(function (results) {
            debugger;
            var data = results.data;
            if (data.Estado === 1) {
                $scope.UserLogin = data.User.Usua;
            } else if (data.Estado === 0) {
                $scope.UserLogin = "USUARIO DESCONOCIDO";
            }
        });
    };
    init();
}
userController.$inject = ['$scope', '$http', '$location', '$window', 'UserService'];




var userModificarController = function ($scope, $http, $location, $window, UserService) {
    var user = {};
    $scope.documentoBusqueda = "45019503";
    $scope.strMensajeGuardarUser = "";
    $scope.FnModificarUser = function () {
        debugger;
        user.Apellido = $scope.apellido;
        user.Nombre = $scope.nombre;
        user.Nombres = $scope.apellido + ' ' + $scope.nombre;
        user.Dni = $scope.dni;
        user.Usua = $scope.user;
        user.Pass = $scope.pass;
        user.Email = $scope.FrmModUsuario.email.$modelValue;
        $http({
            method: "POST",
            url: "/User/Modificar",
            headers: { 'Content-Type': 'application/json' },
            data: user
        }).then(function succes(response) {
            debugger;
            var data = response.data;
            if (data.Estado === 1) {
                $scope.strMensajeGuardarUser = data.strMensaje;
            } else if (data.Estado === 0) {
                $scope.strMensajeGuardarUser = data.strMensaje;
            }

        }, function failured(response) {

        });

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
        $scope.strMensajeGuardarUser = "";
    };

    $scope.FnBusarUserPorDni = function () {
        $scope.FnLimpiarControles();
        var url = "BuscarPorDni/?strDni=" + $scope.documentoBusqueda;
        return $http.get(url).then(function (results) {
            debugger;
            var estado = results.data.Estado;
            var data = results.data.Data;
            if (estado === 1) {
                this.usuarioMod = data;
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

userModificarController.$inject = ['$scope', '$http', '$location', '$window', 'UserService'];


//var data = results.data.Data;
//$scope.apellido = data.Apellido;
//$scope.nombre = data.Nombre;
//$scope.dni = data.Dni;
//$scope.user = data.Usua;
//$scope.pass = data.Pass;
//$scope.email = data.Email;
//$scope.passConfir = data.Pass;

//$scope.usuarioMod.dni = $scope.dni;
//$scope.usuarioMod.apellido = $scope.apellido;
//$scope.usuarioMod.nombre = $scope.nombre;
//$scope.usuarioMod.email = $scope.email;
//$scope.usuarioMod.user = $scope.user;
//$scope.usuarioMod.pass = $scope.pass;
//$scope.usuarioMod.passConfir = $scope.passConfir;