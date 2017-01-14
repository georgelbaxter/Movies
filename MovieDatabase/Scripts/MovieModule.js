﻿var MovieApp = angular.module('MovieApp', [])

MovieApp.controller('MovieController', function ($scope, MovieService) {
    
    getMovies();
    function getMovies() {
        MovieService.getMovies()
            .then(function (movs) {
                $scope.movies = movs;
                console.log($scope.movies);
            })
    }
    $scope.createMovie = function createMovie() {
        MovieService.createMovie($scope.movieToCreate)
        .then(function () {
            getMovies();
            console.log(movieToCreate + "added");
        })
    }
    $scope.deleteMovie = function deleteMovie(movie) {
        var index = $scope.movies.data.indexOf(movie);
        MovieService.deleteMovie(movie.Id)
            .then(function () {
            getMovies();
            console.log(movie + "deleted");
        })
    }
});

MovieApp.factory('MovieService', ['$http', function ($http) {
    
    var MovieService = {};

    MovieService.getMovies = function () {
        return $http.get('/Home/GetMovies');
    };

    MovieService.createMovie = function (movieToCreate) {
        return $http.post('/Home/CreateMovie', movieToCreate);
    };

    MovieService.deleteMovie = function (movieToDeleteId) {
        return $http.delete('/Home/DeleteMovie', { params: { id: movieToDeleteId } });
    };

    return MovieService;

}]);