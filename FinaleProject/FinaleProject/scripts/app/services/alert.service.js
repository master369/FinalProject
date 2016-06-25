'use strict';

App.factory('alert', ['$uibModal', function ($uibModal) {
	return function (errors, timeout) {
	//configurating of modal instance
	    var modalInstance = $uibModal.open({
		    templateUrl: "Alert.html",
			controller: 'AlertCtrl',
			size: '',
			backdrop: false,
			windowClass: 'modal-alert',
			resolve: {
				errors: function () {
					return errors;
				},
				timeout: function () {
					return timeout;
				}
			}
		});

		return modalInstance.result;
	};
}])
.controller('AlertCtrl', ['$scope', '$uibModalInstance', '$uibModalStack', '$timeout', 'errors', 'timeout', function ($scope, $uibModalInstance, $uibModalStack, $timeout, errors, timeout) {
	$scope.errors = errors;//array of errors

	$scope.ok = function () {//close modal
	    $uibModalInstance.close();
	};

	$timeout($scope.ok, timeout);//close this modal after 'timeout' time
	$uibModalStack.dismissAll();//close all other opened modals
}]);