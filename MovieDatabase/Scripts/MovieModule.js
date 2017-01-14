var MovieApp = angular.module('MovieApp', [])

MovieApp.controller('MovieController', function ($scope, MovieService) {
    
    getMovies();
    function getMovies() {
        MovieService.getMovies()
            .then(function (movs) {
                $scope.movies = movs;
                console.log($scope.movies);
                console.log("Made it to line 11");
            })
    }
});

MovieApp.factory('MovieService', ['$http', function ($http) {
    
    var MovieService = {};

    MovieService.getMovies = function () {
        return $http.get('/Home/GetMovies');
    };

    return MovieService;

}]);