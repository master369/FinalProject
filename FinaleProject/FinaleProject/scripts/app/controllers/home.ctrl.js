App.controller('HomeCtrl', function ($http) {
	var vm = this;
	vm.go = function () {
		$http.post('Views/Home/Home.cshtml').then(function () {
			console.dir(arguments);
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