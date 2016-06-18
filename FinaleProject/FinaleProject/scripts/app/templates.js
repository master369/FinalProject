angular.module("templates", []).run(["$templateCache", function($templateCache) {$templateCache.put("Albums.html","<h1>Albums:</h1>\r\n\r\n<div class=\"container\">\r\n    <img src=\"Views/1.jpg\" class=\"img-rounded\" ng-repeat=\"album in albums.albums track by $index\" style=\"width:250px; height:250px; margin: 20px;\">\r\n</div>\r\n\r\n");
$templateCache.put("Home.html","<h1>Топ 10 пользователей</h1>\r\n<button class=\"btn btn-primary\" ng-click=\"home.go()\">Go</button>\r\n<button class=\"btn btn-danger\" ng-click=\"home.private()\">Private</button>\r\n<table class=\"table table-condensed\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                №\r\n            </th>\r\n            <th>\r\n                Имя\r\n            </th>\r\n            <th>Рейтинг</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n        <tr ng-repeat=\"user in home.users track by $index\">\r\n            <td>\r\n                {{$index+1}}\r\n            </td>\r\n            <td>\r\n                {{user.name}}\r\n            </td>\r\n            <td>\r\n                {{user.rating}}\r\n            </td>\r\n        </tr>\r\n    </tbody>\r\n</table>");
$templateCache.put("LogIn.html","");
$templateCache.put("Photos.html","<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <title></title>\r\n	<meta charset=\"utf-8\" />\r\n</head>\r\n<body>\r\n\r\n</body>\r\n</html>\r\n");
$templateCache.put("Search.html","<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <title></title>\r\n	<meta charset=\"utf-8\" />\r\n</head>\r\n<body>\r\n\r\n</body>\r\n</html>\r\n");
$templateCache.put("SignUp.html","<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <title></title>\r\n	<meta charset=\"utf-8\" />\r\n</head>\r\n<body>\r\n\r\n</body>\r\n</html>\r\n");
$templateCache.put("Users.html","<h1>Page 1</h1>");}]);