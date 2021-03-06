﻿App.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded; charset=UTF-8';
    $httpProvider.interceptors.push(function ($q) {
        return {
            'responseError': function (response) {
                if (response.status === 401) {
                    //Auth.logOut();//todo fix
                }
                return $q.reject(response);
            },
            'request': function (config) {
                if (_.isObject(config.data) && !(config.data instanceof FormData)) config.data = $.param(config.data);
                return config;
            },
        };
    });
    $httpProvider.interceptors.push(['$rootScope', '$q', function ($rootScope, $q) {
        return {
            'responseError': function (rejection) {
                if (angular.isDefined(rejection.data)) {
                    //broadcasting of event for opening of the error modal
                    $rootScope.$broadcast('$responseError', rejection.data);
                }
                return $q.reject(rejection);
            }
        };
    }]);


    $urlRouterProvider.otherwise("/users");
    $stateProvider

      .state('albums', {
          url: "/albums?username",
          templateUrl: "Albums.html",
          controller: "AlbumsCtrl",
          controllerAs: "albums",
          params: {
              username: {
                  value: '',
              }
          },
      })
    .state('login', {
        url: "/login",
        templateUrl: "LogIn.html",
        controller: "LoginCtrl",
        controllerAs: "login",
    })
        .state('photos', {
            url: "/photos?username&albumId",
            templateUrl: "Photos.html",
            controller: "PhotoCtrl",
            controllerAs: "photos",
            params: {
                username: {
                    value: '',
                },
                albumId: {
                    value: "",
                }
            },

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
            url: "/users?page&pageSize",
            templateUrl: "Users.html",
            controller: "UsersCtrl",
            controllerAs: "users",
            params: {
                page: {
                    value: '1',
                    squash: false
                },
                pageSize: {
                    value: '10',
                    squash: false
                }
            },
        });
});