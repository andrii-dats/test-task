## Recruitment Test Task

We would like you to build a Weather Dashboard as a small web application.

This repository contains an implementation of the backend part. The Forecast API is built over Forecast.io and WorldWeather services and allow the client to get information about the current and 7-day weather forecast from different API sources. The Cities API is built on top of the Google Places API.

The Front-End framework to use is AngularJS. You have complete creative control over how the dashboard looks and behaves. At a minimum, please implement the following:
- The user can search for and select a city;
- The user can check the forecast for the selected city;


# Starting project

From Application folder run:
```
bower i
```
If bower is not installed:
```
npm i bower -g
```
After that from /Application/src/WeatherApi run:
```
dotnet restore
```
and after:
```
dotnet run
```
Access project with link localhost:5000
