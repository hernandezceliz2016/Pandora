var userController = function ($scope, $http, UserService) {
    debugger;
    var user = {};
    $scope.RegistrarUsuario = function () {

        user.apellidos = $scope.usuario.apellidos;
        user.tipodoc = $scope.usuario.tipodoc;
        user.nombres = $scope.usuario.nombres;
        debugger;
        //UserService.servicioUser.RegistrarUsuario($user).then(function (data) {
        //});
        //$http.post('/api/Guardar', user)
        //    .success(function(data) {
        //        debugger;
        //        $scope.ServerResponse = data;
        //    });

        //$http.post('/api/Guardar', { foo: 'bar' }).success(function (response) {
        //    $scope.response = response;
        //    $scope.loading = false;
        //});
        //debugger;

        $http({
            method: "POST",
            url: "/api/Guardar",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: user
        }).then(function mySucces(response) {
            $scope.myWelcome = response.data;
        }, function myError(response) {
            $scope.myWelcome = response.statusText;
        });

    };
}
userController.$inject = ['$scope', '$http', 'UserService'];

