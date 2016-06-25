App.controller('UsersCtrl', function ($http, $scope, $state) {
    var vm = this,
        user;

    init();

    function init() {
        $http({
            method: 'POST',
            url: 'Views/Account/Users.cshtml',
            data: {
                pageNumber: vm.pageNumber,
                pageSize: vm.pageSize
            },
        })
            .then(function (res) {
                console.dir(res);
            }, function (res) {
                console.dir(arguments);
            });
    }

});