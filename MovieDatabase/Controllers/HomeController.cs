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

        [System.Web.Http.HttpGet]
        public string GetMovie()
        {
            var movieList = db.Movies.ToList();

            List<JSONMovie> resultList = new List<JSONMovie>();
            foreach (Movie movie in movieList)
                resultList.Add(new JSONMovie(movie));

            return JsonConvert.SerializeObject(resultList);
        }

        [System.Web.Http.HttpPost]
        public string CreateMovie(JSONMovie newMovie)
        {
            Movie movie = new Movie(newMovie);
            if (movie != null)
            {
                db.Movies.Add(movie);
                //if (movie.Actors != null && movie.Actors.Count > 0)
                //{
                //    foreach (Actor actor in movie.Actors)
                //    {
                //        if (!db.Actors.Contains(actor))
                //            db.Actors.Add(actor);
                //    }
                //}
                db.SaveChanges();
            }
            //return the updated database to redraw
            return GetMovie();
        }

        [System.Web.Http.HttpDelete]
        public string DeleteMovie(int id)
        {
            Movie movieToDelete = db.Movies.Where(m => m.Id == id).ToList().First();
            db.Movies.Remove(movieToDelete);
            db.SaveChanges();

            return GetMovie();
        }

        [System.Web.Http.HttpPut]
        public string EditMovie (JSONMovie movieToEdit)
        {
            Movie movieBeingEdited = new Movie(movieToEdit);
            db.Entry(movieBeingEdited).State = EntityState.Modified;
            db.SaveChanges();
            return GetMovie();
        }

    }
}