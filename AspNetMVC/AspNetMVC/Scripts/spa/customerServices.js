//app.factory("customerService", function ($http) {
//    return {
//        //getData: function (currentPage, pageSize) {
//        //    var deferred = $q.deferred;
//        //    $http.get('/api/customer', { params: { CurrentPage: $scope.currentPage, PageSize: $scope.pageSize } })
//        //        //.then(function (response) {

//        //        //}, function (response) {
//        //        //    // called asynchronously if an error occurs
//        //        //    // or server returns response with an error status.
//        //        //    $scope.IsLoad = false;

//        //    //});
//        //    .success(deferred.resolve)
//        //    .error(deferred.reject);

//        //    return deferred;
//        //}
//        getData: function () {
//            return "test123456789";
//        }
//    }
//});

app.factory('customerService', customerService);

function customerService($http, $q) {
    return {
        getData: function (currentPage, pageSize) {
            var deferred = $q.defer();
            $http.get('/api/customer', { params: { CurrentPage: currentPage, PageSize: pageSize } })
                .then(function (response) {
                    deferred.resolve(response);
                }, function (response) {
                    deferred.reject('There was an error: ' + response);
                });

            return deferred.promise;
        },
        getDataWithID: function (id) {
            var deferred = $q.defer();
            $http.get('/api/customer', { params: { Id: id } })
                .then(function (response) {
                    deferred.resolve(response);
                }, function (response) {
                    deferred.reject('There was an error: ' + response);
                });

            return deferred.promise;
        }
    }
}