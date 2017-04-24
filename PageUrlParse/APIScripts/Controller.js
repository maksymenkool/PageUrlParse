var urlController = angular.module('urlsController', []);

urlController.controller('UrlController', function ($scope, DataService) {
    getAll();
    function getAll() {

        var minspeed = new Array();
        var maxspeed = new Array();
        var lab = new Array();

        var servCall = DataService.getUrls();
        servCall.then(function (d) {
            $scope.urls = d;

            $.map(d, function (item) {
                minspeed.push(item.MinSpeed);
                maxspeed.push(item.MaxSpeed);
            });
            $scope.labels = [];
            $scope.data = [];
            for (var i = 0; i < d.length; i++) {
                lab.push(i + 1);
            }
            $scope.labels = lab;
            $scope.series = ['Max Speed', 'Min Speed'];
            $scope.data.push(maxspeed);
            $scope.data.push(minspeed);
            
        }, function (error) {
            console.log('Oops! Something went wrong while fetching the data.')
        });
    }

    $scope.regex = '^((https?)://)?([A-Za-z]+\\.)?[A-Za-z0-9-]+(\\.[a-zA-Z]{1,4}){1,2}(/.*\\?.*)?$';

    $scope.saveUrls = function () {
        var u = {
            NameUrl: $scope.nameUrl
        };
        if (!$scope.nameUrl) {
            alert("This URL format is invalid!\n Please enter valid URL!");
            $scope.nameUrl = null;
            return;
        }
        var saveUrls = DataService.saveUrl(u);
        saveUrls.then(function (d) {
            $scope.nameUrl = null;
            getAll();
        }, function (error) {
            console.log('Oops! Something went wrong while saving the data.')
        })
    };

    $scope.dltUrl = function (urlID) {
        var dlt = DataService.deleteUrl(urlID);
        dlt.then(function (d) {
            getAll();
        }, function (error) {
            console.log('Oops! Something went wrong while deleting the data.')
        })
    };
});

urlController.controller('DetailsController', function ($scope, $http, $routeParams) {

    function GetData() {
        $http({
            method: 'get',
            url: '/api/Url/' + $routeParams.id
        })
        .success(function (data) {
            $scope.page = data;
        });
    }

    GetData();
});
