app.controller('customersController', function LoadData($scope, $http, customerService) {
    var vm = this;
    //$scope.searchCompanyName.CompanyName = null;
    $scope.showPaging = true;
    $scope.pageSize = 10;
    $scope.totalCount = 0;
    $scope.currentPage = 1;

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
                CompanyName: response.data["CompanyName"],
                Region: response.data["Region"],
                ContactName: response.data["ContactName"],

            };
            //alert('Data loading success!');
        }, function (response) {
            
        });
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

