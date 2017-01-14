var MovieApp = angular.module('MovieApp', [])

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
});

MovieApp.factory('MovieService', ['$http', function ($http) {
    
    var MovieService = {};

    MovieService.getMovies = function () {
        return $http.get('/Home/GetMovies');
    };

    MovieService.createMovie = function (movieToCreate) {
        return $http.post('/Home/CreateMovie', movieToCreate);
    };

    return MovieService;

}]);