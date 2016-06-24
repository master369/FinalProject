App.controller('MainCtrl', function ($http) {
    var vm = this;
    vm.user = {
        isAuthorized: false
    };

    vm.logout = function () {
        $http.post('Views/Account/Logout.cshtml')
            .then(function (res) {
                vm.user.isAuthorized = false;
            });
    };

    init();

    function init() {
        $http.post('Views/Account/isAuthorized.cshtml')
            .then(function (res) {
                vm.user.isAuthorized = true;
            }, function () {
                vm.user.isAuthorized = false;
            });
    }
});