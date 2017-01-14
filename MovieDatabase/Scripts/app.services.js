window.app = window.todoApp || {};

window.app.service = (function () {
    var baseUri = '/api/movies/';
    var serviceUrls = {
        movies: function () { return baseUri; },
        byId: function (id) { return baseUri + id; }
    }

    function ajaxRequest(type, url, data) {
        var options = {
            url: url,
            headers: {
                Accept: "application/json"
            },
            contentType: "application/json",
            cache: false,
            type: type,
            data: data ? ko.toJSON(data) : null
        };
        return $.ajax(options);
    }

    return {
        allMovies: function () {
            return ajaxRequest('get', serviceUrls.movies());
        },
        update: function (item) {
            return ajaxRequest('put', serviceUrls.byId(item.ID), item);
        }
    };
})();