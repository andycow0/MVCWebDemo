app.controller('customersController', function LoadData($scope, $http, customerService) {
    //var vm = this;
    //$scope.searchCompanyName.CompanyName = null;
    $scope.showPaging = true;
    $scope.pageSize = 10;
    $scope.totalCount = 0;
    $scope.currentPage = 1;
    $scope.enableSave = false;
    $scope.Editable = false;
    $scope.EditText = 'Edit';

    $scope.pageChanged = function () {
        $scope.IsLoad = true;
        GetData();
    };

    $scope.$watch('searchCompanyName.CompanyName', function () {
        if ($scope.searchCompanyName.CompanyName != '') {
            $scope.showPaging = false;
        }
        else {
            $scope.showPaging = true;
        }
    });

    var GetData = function () {
        customerService.getData($scope.currentPage, $scope.pageSize).then(function (response) {
            $scope.Customers = response.data.Data;
            $scope.totalCount = response.data.Total;
            $scope.IsLoad = false;
            //alert('Data loading success!');
        }, function (response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
            $scope.IsLoad = false;
            alert('Data loading error!');
        });
    };

    GetData();

    $scope.GetDataWithID = function (id) {
        customerService.getCustomer(id).then(function (response) {
            $scope.customer = {
                CustomerID: response.data["CustomerID"],
                CompanyName: response.data["CompanyName"],
                Region: response.data["Region"],
                ContactName: response.data["ContactName"],
                ContactName: response.data["ContactName"],
                ContactTitle: response.data["ContactTitle"],
                Address: response.data["Address"],
                Country: response.data["Country"],
                Phone: response.data["Phone"],
            };
            //alert('Data loading success!');
        }, function (response) {

        });
    };

    $scope.Editor = function () {
        $scope.Editable = !$scope.Editable;
        $scope.enableSave = !$scope.enableSave;
        if ($scope.Editable) {
            $scope.EditText = 'Cancel Edit';

            $scope.OldCustomer = {
                CustomerID: $scope.customer.CustomerID,
                CompanyName: $scope.customer.CompanyName,
                ContactName: $scope.customer.ContactName,
                ContactTitle: $scope.customer.ContactTitle,
                Address: $scope.customer.Address,
                Country: $scope.customer.Country,
                Phone: $scope.customer.Phone,
                Region: $scope.customer.Region,

            };

        }
        else {
            $scope.OldCustomer = {};
            $scope.EditText = 'Edit';
        }
    };

    $scope.SaveChange = function () {        
        if (!angular.equals($scope.OldCustomer, $scope.customer)) {
            customerService.updateCustomer($scope.customer).then(function (response) {
                alert('Update success!');
                GetData();
            }, function (response) {
                alert('Update failed!');
            });
        }
    };

    $scope.RemoveCustomer = function (id) {
        customerService.deleteCustomer(id).then(function (response) {
            alert('Delete success!');
            GetData();
        }, function (response) {
            alert('Delete failed!');
        });
    };
});

