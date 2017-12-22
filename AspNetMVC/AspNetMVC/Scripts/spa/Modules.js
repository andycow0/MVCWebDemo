var app = angular.module("spa", ['ngRoute', 'ui.bootstrap']);

app.filter('ceil', function () {
    return function (input) {
        return Math.ceil(input);
    };
});

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    var viewBase = '/App/CustomerApp/';

    $routeProvider.when('/Customers/Index', {
        controller: 'customersController',
        templateUrl: viewBase + 'List.html'
    });

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);