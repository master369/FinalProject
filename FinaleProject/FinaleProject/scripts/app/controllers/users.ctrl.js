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
    }

});