var app = angular.module("App", ['urlsController', 'ngRoute', 'ui.bootstrap', 'chart.js']);

    app.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.
            when('/urls', {
               templateUrl: 'APIScripts/Views/Urls.html',
               controller: 'UrlController'
            })
           .when('/url/details/:id', {
               templateUrl: 'APIScripts/Views/Details.html',
               controller: 'DetailsController'
           })
           .otherwise({
               redirectTo: '/urls'
           });
    }]);