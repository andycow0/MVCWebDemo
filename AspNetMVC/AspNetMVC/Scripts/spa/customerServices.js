﻿app.factory('customerService', customerService);

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
        getCustomer: function (id) {
            var deferred = $q.defer();
            $http.get('/api/customer', { params: { Id: id } })
                .then(function (response) {
                    deferred.resolve(response);
                }, function (response) {
                    deferred.reject('There was an error: ' + response);
                });

            return deferred.promise;
        },

        createCustomer: function (customer) {
            var deferred = $q.defer();
            //$http.post('/api/customer', { params: { Customer: customer } })
            $http.post('/api/customer', customer)
                .then(function (response) {
                    deferred.resolve(response);
                }, function (response) {
                    deferred.reject('There was an error: ' + response);
                });

            return deferred.promise;
        },

        updateCustomer: function (customer) {
            var deferred = $q.defer();
            $http.put('/api/customer', customer)
                .then(function (response) {
                    deferred.resolve(response);
                }, function (response) {
                    deferred.reject('There was an error: ' + response);
                });

            return deferred.promise;
        },

        deleteCustomer: function (id) {
            var deferred = $q.defer();
            $http.delete('/api/customer', { params: { Id: id } })
                .then(function (response) {
                    deferred.resolve(response);
                }, function (response) {
                    deferred.reject('There was an error: ' + response);
                });

            return deferred.promise;
        }
    }
}