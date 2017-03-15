(function () { 
    var app = angular.module('app');


    GlobalStateManager.registerState(
        'listUsers',
        '/angular-app/Users/ListUsers/Templates/ListUsers.html');
    
    GlobalStateManager.defaultState = "listUsers";
    
    
    app.controller('ListUsersController', ['$scope', '$http', '$state', 'DTOptionsBuilder', function ($scope, $http, $state, DTOptionsBuilder) {
        


        $scope.users = [];

        $scope.dtOptions = DTOptionsBuilder.newOptions().withOption('aaSorting', [[1, 'asc']]);


        function loadUsers() {
            $http.post('/api/users/getAllUsers', {}).then(function (res) { 
                $scope.users = res.data.users;
            }, function (error) {
                
            });
        }

        $scope.init = function () {
            loadUsers();
        };

        $scope.edit = function (user) {
            console.log("Edit");
            console.log(user);
        }

        // Displays a confirmation modal for event delete.
        $scope.delete = function (user) {
            
            $scope.toDelete = user;

            $('#deleteModal').modal('show');
        }

        // Is called when the user confirms for the event to be deleted.
        $scope.confirmDeleteEvent = function () {

            // console.log("Delete user");

            var id = $scope.toDelete.id;

            console.log("remove user");

            $http.post('/api/users/deleteUser', {
                userId: id
            }).then(
            function (res) {

                loadUsers();

            },
            function (error) {
                console.log(error);
            });
        };
    }]);
})();