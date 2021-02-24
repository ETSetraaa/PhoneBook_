var app = angular.module('myContactBook');
app.factory('contactService', ['$http', '$q', function ($http, $q) {
    var fac = {};
    fac.GetContacts = function () {
        var defer = $q.defer();
        $http.get('/home/getcontacts')
        .success(function (data) {
            defer.resolve(data);
        });
        return defer.promise;
    }

    fac.GetContact = function (contactID) {
        var defer = $q.defer();
        $http.get('/home/getcontact', {
            params: {
                'contactID' : contactID
            }
        })
        .success(function (data) {
            defer.resolve(data);
        });
        return defer.promise;
    }

    fac.SaveContact = function (contact) {
        var defer = $q.defer();
        $http({
            method: "post",
            url: '/home/savecontact',
            data : contact
        }).then(function (response) {
            defer.resolve(response.data);
        })
        return defer.promise;
    }

    fac.DeleteContact = function (contactID) {
        var defer = $q.defer();
        $http({
            method: 'POST',
            url: '/home/DeleteContact',
            data : {'contactID' : contactID}
        }).then(function (response) {
            defer.resolve(response.data);
        })

        return defer.promise;
    }

    return fac;

}]);