App.controller('MainCtrl', function ($http) {
    var vm = this;
    vm.user = {
        isAuthorized: false
    };

    vm.logout = function () {
        $http.post('Views/Account/Logout.cshtml')
            .then(function (res) {
                angular.copy({ isAuthorized: false }, vm.user);
            });
    };

    init();

    function init() {
        $http.post('Views/Account/isAuthorized.cshtml')
            .then(function (res) {
                var data = res.data;
                angular.copy({
                    isAuthorized: true,
                    name: data.Name,
                    roles: data.Roles
                }, vm.user);
            }, function () {
                angular.copy({ isAuthorized: false }, vm.user);
            });
    }
});