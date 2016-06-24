App.controller('SignupCtrl', function ($http, $scope, $httpParamSerializerJQLike, $state) {
    var vm = this,
        user;
    user = vm.user = {
        name: '',
        password: '',
    };  
    vm.signup = function () {
        $http({
            method: 'POST',
            url: 'Views/Account/SignUp.cshtml',
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
            }, function () {
                console.dir(arguments);
                //todo show error message
            });
    };
});