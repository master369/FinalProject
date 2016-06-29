App.controller('AlbumsCtrl', function ($http, $scope, $stateParams) {
    var vm = this,
        myName = $scope.main.user.name;
    vm.username = $stateParams.username;
    $scope.$watch('main.user.name', function (newVal, oldVal) {
        if (newVal) {
            myName = $scope.main.user.name;

        }
    });
    vm.albums = [];
    vm.addAlbum = function () {
        if (!myName) {
            return;
        }
        $http.post('Views/Album/Albums.cshtml', {
            typeOfChange: 'post',
            albumName: vm.newAlbumName,
            username: myName
        }).then(function (res) {
            var data = res.data;
            vm.albums = data.AlbumList;
        });
    };
    init();
    function init() {
        $http.post('Views/Album/Albums.cshtml', {
            typeOfChange: 'getAll',
            username: $stateParams.username,
        }).then(function (res) {
            var data = res.data;
            vm.albums = data.AlbumList;
        });
    };
    vm.removeAlbum = function (albumId) {
        $http.post('Views/Album/Albums.cshtml', {
            typeOfChange: 'delete',
            albumid: albumId,
            username: $stateParams.username,
        }).then(function (res) {
            var data = res.data;
            vm.albums = data.AlbumList;
        });
    };
});