var injectParameter = ['$http'];
var factory = function ($http) {
    var servicioUser = {};
    servicioUser.RegistrarUsuario = function (args) {

    };
    return {
        servicioUser: servicioUser
    }
}
factory.$inject = injectParameter;