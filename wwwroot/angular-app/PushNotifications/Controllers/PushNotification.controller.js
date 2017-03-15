(function () { 
    myAppModule = angular.module('app');

    GlobalStateManager.registerState(
        'pushNotification',
        '/angular-app/PushNotifications/Templates/PushNotification.html');

    myAppModule.controller("PushNotificationController", ['$scope', '$http', '$state', function ($scope, $http, $state) {

        
        $scope.pushRequest = {
            userId: 0,
            Message: ""
        };

        $scope.init = function () {
            (function initChosen() {
                $('#select-user').ajaxChosen({
                    dataType: 'json',
                    type: 'POST',
                    url: '/api/notification/getUsersForDropDown'
                }, {
                        loadingImg: '/css/chosen/loading.gif'
                    });
            })();
        };
        

        $scope.submitForm = function () {
            
            $scope.pushRequest.userId = parseInt($('#select-user').val());
            


            $http.post('api/notification/pushnotification', $scope.pushRequest).then(function (res) {
                alert('Notification sent!');
            }, function (req) {
                alert('Could not send notification! Make sure you\'re connected to the internet!');
            });
        };

    }]);
})();