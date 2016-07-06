App.controller('SignupCtrl', function ($http, $scope, $state) {
    var vm = this,
        user;
    user = vm.user = {
        name: '',
        password: '',
        confirmPassword: ''
    };
    vm.signup = function () {
        $http({
            method: 'POST',
            url: 'Views/Account/SignUp.cshtml',
            data: {
                Login: user.name,
                Password: user.password,
                ConfirmPassword: user.confirmPassword
            },
        })
            .then(function (res) {
                var data = res.data;
                angular.copy({
                    isAuthorized: true,
                    name: data.Name,
                    roles: data.Roles
                }, $scope.main.user);
                $state.go('users');
            }, function (res) {
                var data = res.data;
                user.password = '';
                user.confirmPassword = '';
                if (data !== 'Password does not match the confirm password!') {
                    user.name = '';

                }

                console.dir(arguments);
                //todo show error message
            });
    };
});