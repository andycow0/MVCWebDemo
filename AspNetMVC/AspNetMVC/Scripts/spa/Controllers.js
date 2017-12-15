

app.controller('customersController', ['$scope', '$http',
   LoadData
]);

function LoadData($scope, $http) {
    //$scope.searchCompanyName = "Anna";

    $scope.pageSize = 10;
    $scope.totalCount = 0;
    $scope.currentPage = 1;

    $scope.pageChanged = function () {
        $scope.IsLoad = true;
        GetData();
    };

    var GetData = function () {
        $http.get('/api/customer', { params: { CurrentPage: $scope.currentPage, PageSize: $scope.pageSize } })
        .then(function (response) {
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
        //.error(function (data) {
        //    alert('Data loading error!');
        //});
        //.complete(function (data) {
        //    //alert('查詢完成');
        //    //$("#datepicker").datepicker("refresh");
        //    alert('Data loading complete!');
        //});

    };

    GetData();
}