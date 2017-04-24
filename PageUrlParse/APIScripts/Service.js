app.service("DataService", function ($http) {
    this.getUrls = function () {
        var url = 'api/Url';
        return $http.get(url).then(function (response) {
            return response.data;
        });        
    }

    this.saveUrl = function (urladdress) {
        return $http({
            method: 'post',
            data: urladdress,
            url: 'api/Url'
        });
    }

    this.deleteUrl = function (urlID) {
        var url = 'api/Url/' + urlID;
        return $http({
            method: 'delete',
            data: urlID,
            url: url
        });
    }
});