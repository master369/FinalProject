App.controller('LoginCtrl', function ($http, $scope, $state) {
    var vm = this,
        user;
    user = vm.user = {
        name: '',
        password: '',
    };
    vm.login = function () {
        $http({
            method: 'POST',
            url: 'Views/Account/LogIn.cshtml',
            data: {
                Login: user.name,
                Password: user.password
            },
        })
            .then(function (res) {
                var data = res.data;
                angular.copy({
                    isAuthorized: true,
                    name: data.Name,
                    roles: data.Roles
                }, $scope.main.user);
                $state.go('home');
            }, function (res) {
                console.dir(arguments);
                var data = res.data;
                user.password = "";

            });
    };
});