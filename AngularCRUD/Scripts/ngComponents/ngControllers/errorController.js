var app = angular.module('myContactBook');
app.controller('errorController', ['$scope', function ($scope) {
    $scope.ErrorMessage = "Error! Please try again.";
}])