App.controller('MainCtrl', function ($http, alert, $scope, MainUser) {
    var vm = this;
    vm.user = {
        isAuthorized: false
    };

    vm.logout = function () {
        $http.post('Views/Account/Logout.cshtml')
            .then(function (res) {
                angular.copy({ isAuthorized: false }, vm.user);
                angular.copy({ isAuthorized: false }, MainUser);
            });
    };

    init();
    function init() {

        $scope.$on('$responseError', function (event, errorMessage) {
            errorMessage = errorMessage.split('\n');
            alert(errorMessage, 7000);
        });

        $http.post('Views/Account/isAuthorized.cshtml')
            .then(function (res) {
                var data = res.data,
                    user = {
                        isAuthorized: true,
                        name: data.Name,
                        roles: data.Roles
                    };
                angular.copy(user, vm.user);
                angular.copy(user, MainUser);
            }, function () {
                angular.copy({ isAuthorized: false }, vm.user);
                angular.copy({ isAuthorized: false }, MainUser);
            });

        vm.isAdmin = function () {
            return _.includes(vm.user.roles, 'Admin');
        };
    }
});