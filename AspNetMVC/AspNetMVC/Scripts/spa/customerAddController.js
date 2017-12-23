app.controller("customersAddController", function ($scope, $http, customerService) {

    $scope.CustomerID = '';
    $scope.CompanyName = '';
    $scope.Country = '';
    //$scope.CustomerID = '1234';

    $scope.Create = function () {

        var customer = {
            CustomerID: $scope.CustomerID,
            CompanyName: $scope.CompanyName,
            Country: $scope.Country,
        };

        customerService.createCustomer(customer).then(function (response) {
            alert('Create success!');
        }, function (response) {
            alert('Create failed!');
        });
    };
});