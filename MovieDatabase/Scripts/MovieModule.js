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
    $scope.deleteMovie = function deleteMovie(movie) {
        MovieService.deleteMovie(movie.Id)
            .then(function () {
            getMovies();
            console.log(movie + "deleted");
        })
    }
    $scope.enableEdit = function () {
        $scope.editEnabled = true;
    }
    $scope.disableEdit = function () {
        $scope.editEnabled = false;
        getMovies();
    }
    $scope.editMovie = function editMovie(movie) {
        MovieService.editMovie(movie)
        .then(function () {
            getMovies();
            console.log(movie + "edited");
            $scope.diableEdit();
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

    MovieService.editMovie = function (movieToEdit) {
        return $http.put('Home/EditMovie', movieToEdit);
    };

    return MovieService;

}]);