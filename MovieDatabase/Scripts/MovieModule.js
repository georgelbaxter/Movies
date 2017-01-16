var MovieApp = angular.module('MovieApp', [])

MovieApp.controller('MovieController', function ($scope, MovieService) {
    getMovie();

    function getMovie() {
        MovieService.getMovie()
            .then(function (movs) {
                $scope.movies = movs;
                console.log($scope.movies);
            })
    }

    $scope.createMovie = function createMovie(movie) {
        MovieService.createMovie(movie)
        .then(function () {
            getMovie();
            console.log(movie + "added");
            $scope.addMovieForm.$setPristine();
            $scope.movieToCreate = {};
        })
    }

    $scope.deleteMovie = function deleteMovie(movie) {
        MovieService.deleteMovie(movie.Id)
            .then(function () {
                getMovie();
                console.log(movie + "deleted");
            })
    }

    $scope.editMovie = function editMovie(movie) {
        MovieService.editMovie(movie)
        .then(function () {
            movie.editEnabled = false;
            console.log(movie + "edited");
            getMovie();
        })
    }

    //button show/hide functions
    $scope.enableDelete = function (movie) {
        movie.deleteEnabled = true;
    }

    $scope.disableDelete = function (movie) {
        movie.deleteEnabled = false;
    }

    $scope.enableEdit = function (movie) {
        movie.editEnabled = true;
    }

    $scope.disableEdit = function (movie) {
        getMovie();
        movie.editEnabled = false;
    }


});

MovieApp.factory('MovieService', ['$http', function ($http) {

    var MovieService = {};

    MovieService.getMovie = function () {
        return $http.get('/Home/GetMovie');
    };

    MovieService.createMovie = function (movieToCreate) {
        return $http.post('/Home/CreateMovie', angular.toJson(movieToCreate));
    };

    MovieService.deleteMovie = function (movieToDeleteId) {
        return $http.delete('/Home/DeleteMovie', { params: { id: movieToDeleteId } });
    };

    MovieService.editMovie = function (movieToEdit) {
        return $http.put('Home/EditMovie', angular.toJson(movieToEdit));
    };

    return MovieService;

}]);