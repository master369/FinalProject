App.controller('LoginCtrl', function ($http, $scope, $httpParamSerializerJQLike, $state) {
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
            data: $httpParamSerializerJQLike({
                Login: user.name,
                Password: user.password
            }),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
            }
        })
            .then(function (res) {
                $scope.main.user.isAuthorized = true;
                $state.go('home');
            }, function () {
                console.dir(arguments);
                //todo show error message
            });
    };
});