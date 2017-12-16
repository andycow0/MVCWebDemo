

app.controller('customersController', function LoadData($scope, $http, customerService) {
    //$scope.searchCompanyName = "Anna";

    $scope.pageSize = 10;
    $scope.totalCount = 0;
    $scope.currentPage = 1;

    $scope.pageChanged = function () {
        $scope.IsLoad = true;
        GetData();
    };

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
});

