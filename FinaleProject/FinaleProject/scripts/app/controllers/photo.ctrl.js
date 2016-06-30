App.controller('PhotoCtrl', function ($scope, $mdDialog, $mdMedia, $http, $stateParams, $timeout, fileUpload) {
    $scope.status = '  ';
    var vm = this,
       myName = $scope.main.user.name,
       albumId = $stateParams.albumId;

    vm.username = $stateParams.username;
    $scope.$watch('main.user.name', function (newVal, oldVal) {
        if (newVal) {
            myName = $scope.main.user.name;
        }
    });
    $scope.customFullscreen = $mdMedia('xs') || $mdMedia('sm');
    $scope.showPhoto = function (ev) {
        var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
        $mdDialog.show({
            controller: DialogController,
            templateUrl: 'ZoomPhoto.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: useFullScreen
        })
        $scope.$watch(function () {
            return $mdMedia('xs') || $mdMedia('sm');
        }, function (wantsFullScreen) {
            $scope.customFullscreen = (wantsFullScreen === true);
        });
    };
    function DialogController($scope, $mdDialog) {
        $scope.hide = function () {
            $mdDialog.hide();
        };
        $scope.cancel = function () {
            $mdDialog.cancel();
        };
        $scope.answer = function (answer) {
            $mdDialog.hide(answer);
        };
    };
    vm.photos = [];
    vm.addPhoto = function () {
        if (!myName) {
            return;
        }
        $http.post('Views/Photos/Photos.cshtml', {
            typeOfChange: 'post',
            photoName: vm.newPhotoName,
            username: myName
        }).then(function (res) {
            var data = res.data;
            vm.photos = data.PhotosList;
        });
    };

    init();

    function init() {
        $http.post('Views/Photos/Photos.cshtml', {
            typeOfChange: 'getAll',
            username: $stateParams.username,
        }).then(function (res) {
            var data = res.data;
            vm.photos = data.PhotosList;
        });
    };

    $scope.$watch('myFile', function (newVal) {
        if (angular.isDefined(newVal)) {
            $timeout(function () {
                uploadFile(newVal);
            });
        }
    });

    function uploadFile(file) {
        var uploadUrl = 'Views/Photos/Photos.cshtml';
        fileUpload.uploadFileToUrl(uploadUrl, file, {
            typeOfChange: 'post',
            albumId: albumId,
            username: vm.username
        }).then(function (res) {
            var data = res.data;
            document.querySelectorAll('[type="file"]')[0].value = "";
        });
    };
});