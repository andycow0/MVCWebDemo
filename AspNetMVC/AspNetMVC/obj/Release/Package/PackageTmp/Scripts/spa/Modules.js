var app = angular.module("spa", ['ngRoute', 'ui.bootstrap']);

app.filter('ceil', function () {
    return function (input) {
        return Math.ceil(input);
    };
});

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    var viewBase = '/App/CustomerApp/';

    $routeProvider.when('/Customers/', {
        controller: 'customersController',
        templateUrl: '/Customers/List'
    });

    $routeProvider.when('/Customers/Index', {
        controller: 'customersController',
        //    templateUrl: viewBase + 'List.html'
        templateUrl: '/Customers/List'
    });

    $routeProvider.when('/Customers/Add', {
        controller: 'customersAddController',
        templateUrl: viewBase + 'Create.html'
    });

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);