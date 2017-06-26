(function () {
    'use strict';

    angular
        .module('weatherApp')
        .controller('homeController', homeController)
        .factory("Cities", function ($http) {
          console.log('here...');
            return {
              getCities: function(val){
                return $http.get(`api/cities/search?byName=${val}`)
                .then(function(response){
                    return response.data;
                });
              },
              getCoord : function(id) {
                return $http.get('api/cities/'+ id)
              }
            }
        })

        .factory("Weather", function ($http) {
          return {
               getWeather: function(lng, lat, src){
                 return $http.get('api/forecast',{
                   params: {
                     longitude:lng,
                     latitude:lat,
                     source: src
                   }
                 })
               }
             };
        })
        .directive('weather', function() {
          return { // scope: {
            //   date: "=",
            //   temperature: "=",
            //   temperatureMax: "=",
            //   temperatureMin: "=",
            //   apparentTemperature: "=",
            //   apparentTemperatureMax: "=",
            //   apparentTemperatureMin: "=",
            //   humidity: "=",
            //   pressure: "=",
            //   cloudCover: "=",
            // },
            scope: {},
            templateUrl: './home-template.html' };
        });


    function homeController($scope, Cities, Weather) {

      $scope.selected = undefined;
      $scope.cities = Cities;
      $scope.weather = Weather;
      //

      const vm = this;

      vm.showData = function (city) {
        Cities.getCoord(city.placeId)
          .then(function(data) {
            vm.cityData = data.data;
            vm.weatherData = [];
            return Weather.getWeather(vm.cityData.latitude, vm.cityData.longitude,"WORLD_WEATHER")
             .then(function(response){
               vm.weatherData.push(response.data.currently)
                for (var i = 0; i < response.data.futureForecasts.length; i++) {
                  vm.weatherData.push(response.data.futureForecasts[i])
                }
               console.log(response.data);
               console.log(vm.weatherData)
               return response.data;
             })
          })
      };

      vm.buttDisable = function (val){       
        return (typeof val) === 'object';
      };
    }
    
})();
