GlobalStateManager = {
    states: [],
    defaultState: "",
    registerState: function (stateName, stateTemplateUrl) {
        GlobalStateManager.states.push({
            name: stateName,
            url: '/' + stateName,
            templateUrl: stateTemplateUrl
        });
        if (GlobalStateManager.defaultState == "") {
            GlobalStateManager.defaultState = stateName;
        }
    }
};

// ApplicationStates = [];
// ApplicationDefaultState = "";
// function AddApplicationState(stateName, stateTemplateUrl,defaultState = false) {
//     ApplicationStates.push();

//     if (defaultState) {
//         ApplicationDefaultState = stateName;
//     }
// }

(function () {
    var app = angular.module("app", ['datatables', 'ui.router', 'angular-jquery-locationpicker', 'ngMap']);
    app.run(['$rootScope', function ($rootScope) {
        $rootScope.isLoggedIn = false;

        $rootScope.logout = function () {
            $rootScope.isLoggedIn = false;
        }

        // When the app loads the body's style is set to 'display:none'
        // this was done because ng-show takes some time to have an effect.
        // So we remove this attribute once angular has initialized.
        $('#main-body').removeAttr("style");
    }]);


    var states = [];

    app.config(['$stateProvider','$urlRouterProvider', function ($stateProvider,$urlRouterProvider) {


        for (var i = 0; i < GlobalStateManager.states.length; i++) {
            $stateProvider.state(GlobalStateManager.states[i]);
        }


        // List is the default route
        $urlRouterProvider.otherwise(GlobalStateManager.defaultState);

    }]);

    // Authentication controller:
    // This is kind of like the main controller, it is responsible for showing the body only When
    // the user is logged in, else it will hide the body.
    app.controller('AuthenticationController', ['$rootScope','$scope','$state','$http', function ($rootScope,$scope,$state,$http) {
        $scope.user = {
            userName: "",
            password: ""
        };

        $('#invalidCredentials').hide();

        function login() {
            $state.go(GlobalStateManager.defaultState);
            $rootScope.isLoggedIn = true;
        };

        $scope.submitForm = function () {

            $http.post('/api/adminlogin/validate', $scope.user).then(function (res) {
                res = res.data;

                if (res.status == 1) {
                    $('#invalidCredentials').hide();
                    $scope.user.userName = "";
                    $scope.user.password = "";
                    login();
                } else {
                    $('#invalidCredentials').show();
                }
            }, function (error) {

            });
        };
    }]);
    
})();