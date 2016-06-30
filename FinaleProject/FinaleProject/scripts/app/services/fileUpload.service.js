'use strict';

App.service('fileUpload', ['$http', function ($http) {
    this.uploadFileToUrl = function (uploadUrl, file, otherParamsObj) {
        var fd = new FormData(),
            i;


        for (i = 0; i < file.length; i++) {
            fd.append('uploaded', file[i]);
        }

        for (i in otherParamsObj) {
            if (otherParamsObj.hasOwnProperty(i)) {
                fd.append(i, otherParamsObj[i]);
            }
        }
        return $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        });
    };
}]);