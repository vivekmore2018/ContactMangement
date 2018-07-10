var app = angular.module('MyApp', []);

app.controller("Contact", function ($scope, ContactApiServices) {
    var promiseGet = ContactApiServices.getAllContacts();
    promiseGet.then(function (response) {
        $scope.items = response.data;
    },
    function (errorResponse) {
        alert('Some Error in Getting Records.', errorResponse.message);
    });

    $scope.submitForm = function (isValid, item) {
        if (isValid) {
            var promiseGet = ContactApiServices.CreateContact(item);
            promiseGet.then(function (response) {
                $scope.items.push(response.data);
                $scope.item = {};
            },
            function (errorResponse) {
                alert('Some Error in Getting Records.', errorResponse.message);
            });
        }
    },
    $scope.removeItem = function (index) {
        console.log(index);
        $scope.items.splice(index, 1)
    },
    $scope.editItem = function (index) {
        $scope.editing = $scope.items.indexOf(index);
    }
    $scope.updateItem = function (item) {

        var promiseGet = ContactApiServices.UpdateContact(item);
        promiseGet.then(function (response) {
            //$scope.items.push(response.data);
            //$scope.item = {};
        },
        function (errorResponse) {
            alert('Some Error in Getting Records.', errorResponse.message);
        });
        item.editMode = false;
    }
});