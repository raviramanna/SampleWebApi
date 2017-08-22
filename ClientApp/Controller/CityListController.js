/// <reference path="C:\Users\Ravi\documents\visual studio 2015\Projects\SampleApi\ClientApp\scripts/angular.js" />

angular.module("WeatherApp", ['ui.bootstrap']).controller("WeatherController",
    function ($scope, $http) {
        $scope.changeCountry = function(countryName) {
            console.log(countryName);
            var countrySelector = document.getElementById("countrySelector");
            $scope.country = countrySelector.options[countrySelector.selectedIndex].text;
        };
        $scope.country = "China";
        $scope.getCities = function (country) {
            $scope.showWeatherDetails = false;
            $scope.showDropDown = true;
            console.log(country);
            var buildUrl = "http://localhost:35030/api/weather/country/" + encodeURI(country);
            console.log(buildUrl);
            $http.get(buildUrl).then(function (response) {
                $scope.cities = JSON.parse(response.data);
                console.log($scope.cities);
            });
        };

        $scope.getWeatherDetails = function(cityName) {
            $scope.showWeatherDetails = true;
            console.log(cityName);
            var buildUrl = "http://localhost:35030/api/weather/city/" + encodeURI(cityName);
            console.log(buildUrl);
            $http.get(buildUrl).then(function (response) {
                console.log(response.data);
                $scope.cityWeather = response.data;
                $scope.weatherImage = "http://openweathermap.org/img/w/" + response.data.weather[0].icon + ".png";
                console.log($scope.cityWeather);
            });
        }
    });
