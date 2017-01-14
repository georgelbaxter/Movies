using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MovieDatabase.Models;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace MovieDatabase.Controllers
{
    public class HomeController : Controller
    {
        private MovieDatabaseContext db = new MovieDatabaseContext();

        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public string GetMovies()
        {
            var movieList = db.Movies.ToList();

            List<JSONMovie> JSONMovieList = new List<JSONMovie>();
            foreach (Movie movie in movieList)
                JSONMovieList.Add(new JSONMovie(movie));

            return JsonConvert.SerializeObject(JSONMovieList);
        }
        
        [System.Web.Http.HttpPost]
        public string CreateMovie(JSONMovie newMovie)
        {
            Movie movie = new Movie(newMovie);
            if (movie != null)
            {
                db.Movies.Add(movie);
                if (movie.Actors != null && movie.Actors.Count > 0)
                {
                    foreach (Actor actor in movie.Actors)
                    {
                        if (!db.Actors.Contains(actor))
                            db.Actors.Add(actor);
                    }
                }
                db.SaveChanges();
            }
            //return the updated database to redraw
            return GetMovies();
        }

    }
}