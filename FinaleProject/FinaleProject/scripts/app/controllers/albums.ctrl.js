App.controller('AlbumsCtrl', function ($http, $scope) {
    var vm = this,
        myName = $scope.main.user.name;
    $scope.$watch('main.user.name', function (newVal, oldVal) {
        if (newVal) {
            myName = $scope.main.user.name;
        }
    });
    vm.albums = [1,2,3,4,5,5,6,67,4,3];
    vm.addAlbum = function () {
        if (!myName) {
            return;
        }
        $http.post('Views/Album/Albums.cshtml', {
            typeOfChange: 'post',
            albumName: vm.newAlbumName,
            username: myName
        }).then(function (res) {
            console.dir(res.data);
        });
    };
});