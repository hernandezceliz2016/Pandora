var injectParameter = ['$http'];
var factory = function ($http) {
    var servicioUser = {};
    var buscarUser = {};
    servicioUser.RegistrarUsuario = function (args) {

    };
    return {
        servicioUser: servicioUser
    }

    

}
factory.$inject = injectParameter;