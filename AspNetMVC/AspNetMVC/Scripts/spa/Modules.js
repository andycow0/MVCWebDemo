var app = angular.module("spa", ['ui.bootstrap']);

app.filter('ceil', function () {
    return function (input) {
        return Math.ceil(input);
    };
});