app.controller("customersAddController", function ($scope, $http, customerService) {

    $scope.CustomerID = '';
    $scope.CompanyName = '';
    $scope.Country = '';
    //$scope.CustomerID = '1234';

    $scope.Create = function () {

        var customer = {
            CustomerID: $scope.CustomerID,
            CompanyName: $scope.CompanyName,
            ContactName: $scope.ContactName,
            ContactTitle: $scope.ContactTitle,
            Address: $scope.Address,
            Country: $scope.Country,
            Phone: $scope.Phone,
            Region: $scope.Region,
            Fax: $scope.Fax,
        };

        customerService.createCustomer(customer)
            .then(function (response) {
                alert('Create success!');
            }, function (response) {
                alert('Create failed!');
            });
    };
});