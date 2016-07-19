var app = angular.module('awApp', ['ngRoute']);

app.config(['$routeProvider',
    function ($routeProvider) {
        $routeProvider.when('/logeo',
            {
                controller: 'LoginController',                
                templateUrl: 'spa/views/login/login.html'
            }).when('/registro',
            {
                controller: 'LoginController',
                templateUrl: 'spa/views/login/registro.html'
            }).when('/subida',
            {
                controller: 'UploadController',               
                templateUrl: 'spa/views/upload/upload.html'
            }).
            when('/Layaout',
            {
                controller: 'UserController',               
                templateUrl: 'Layaout/Index.html'
            }).
            otherwise({ redirectTo: "/" })
    
    }]);
