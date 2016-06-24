App.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded; charset=UTF-8';
    //$httpProvider.defaults.paramSerializer = '$httpParamSerializerJQLike';
    $httpProvider.interceptors.push(function ($q) {
        return {
            'responseError': function (response) {
                if (response.status === 401) {
                    //Auth.logOut();//todo fix
                }
                return $q.reject(response);
            },
            'request': function (config) {
                if (_.isObject(config.data)) config.data = $.param(config.data);
                return config;
            },

        };
    });



    $urlRouterProvider.otherwise("/home");
    $stateProvider
      .state('home', {
          url: "/home",
          templateUrl: "Home.html",
          controller: "HomeCtrl",
          controllerAs: "home",
      })
      .state('albums', {
          url: "/albums",
          templateUrl: "Albums.html",
          controller: "AlbumsCtrl",
          controllerAs: "albums",
      })
    .state('login', {
        url: "/login",
        templateUrl: "LogIn.html",
        controller: "LoginCtrl",
        controllerAs: "login",
    })
        .state('photos', {
            url: "/photos",
            templateUrl: "Photos.html"
        })
        .state('search', {
            url: "/search",
            templateUrl: "Search.html"
        })
        .state('signup', {
            url: "/signup",
            templateUrl: "SignUp.html",
            controller: "SignupCtrl",
            controllerAs: "signup",
        })
        .state('users', {
            url: "/users",
            templateUrl: "Users.html"
        });
});