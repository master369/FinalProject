App.controller('HomeCtrl', function ($http) {
    var vm = this;
    vm.go = function () {
        $http.post('Views/Home/Home.cshtml', {
            Login: 'Admin'
        }).then(function (res) {
            console.log(res.data);
        });
    };

    vm.private = function () {
        $http.post('Views/Account/Users.cshtml').then(function (res) {
            console.log(res.data);
        }, function () {
            console.log('Unautorised');
        });
    };

    vm.users = [{
        name: 'Anton',
        rating: 1595854562,
    },
 {
     name: 'Vlad',
     rating: 1,
 },
    ];

    vm.users = _.sortBy(vm.users, 'rating').reverse();

});