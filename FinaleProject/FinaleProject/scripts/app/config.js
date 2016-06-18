App.config(function ($stateProvider, $urlRouterProvider) {
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
        templateUrl: "LogIn.html"
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
            templateUrl: "SignUp.html"
        })
        .state('users', {
            url: "/users",
            templateUrl: "Users.html"
        });
});