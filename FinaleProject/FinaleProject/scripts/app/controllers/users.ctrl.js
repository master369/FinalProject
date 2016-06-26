App.controller('UsersCtrl', function ($http, $scope, $state, $stateParams) {
    var vm = this,
        user,
        pageFromUrl = parseInt($stateParams.page, 10),
        pageSizeFromUrl = parseInt($stateParams.pageSize, 10);


    init();

    function init() {
        vm.currentPage = !_.isNaN(pageFromUrl) ? pageFromUrl : 1;
        vm.pageSize = !_.isNaN(pageSizeFromUrl) ? pageSizeFromUrl : 10;
        initPages();
        initData();
        vm.pageSizeList = [3, 5, 10, 25, 50];
    }

    function initData() {
        $http({
            method: 'POST',
            url: 'Views/Account/Users.cshtml',
            data: {
                pageNumber: vm.currentPage,
                pageSize: vm.pageSize
            },
        })
           .then(function (res) {
               var data = res.data;

               vm.userList = data.items;
               vm.totalCount = data.totalCount;
           }, function (res) {
               console.dir(arguments);
           });

    }

    function initPages() {
        $scope.$watch('users.currentPage', function () {
            initData();
        });
        $scope.$watch('users.pageSize', function () {
            initData();
        });
    }

    vm.isModer = function (index) {
        return _.includes(vm.userList[index].Roles, 'Moderator');
    };

    vm.toggleModer = function (flag, index) {
        var username = vm.userList[index].Name;
        $http({
            method: 'POST',
            url: 'Views/Account/ChangeRole.cshtml',
            data: {
                addFlag: flag,
                username: username,
                userrole: "Moderator",
            },
        })
          .then(function (res) {
              var data = res.data;
              vm.userList[index].Roles = data.Roles;
          }, function (res) {
              console.dir(arguments);
          });

    };

    vm.updatePageSize = function () {
        $state.go('.', { pageSize: vm.pageSize }, { notify: false });
    };

});