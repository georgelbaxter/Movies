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
            List<Movie> movieList = db.Movies.ToList();

            List<JSONMovie> resultList = new List<JSONMovie>();
            foreach (Movie movie in movieList)
                resultList.Add(new JSONMovie(movie));
            resultList = resultList.OrderBy(m => m.Name).ToList();
            return JsonConvert.SerializeObject(resultList);
        }

        [System.Web.Http.HttpPost]
        public string CreateMovie(JSONMovie newMovie)
        {
            Movie movie = new Movie(newMovie);

            //add movie to database
            db.Movies.Add(movie);

            //add actors to the database and save
            foreach (Actor actor in movie.Actors)
            {
                actor.MovieId = movie.Id;
                db.Actors.Add(actor);
            }
            db.SaveChanges();

            //return the updated database to redraw
            return GetMovie();
        }

        [System.Web.Http.HttpDelete]
        public string DeleteMovie(int id)
        {
            //find the movie to delete
            Movie movieToDelete = db.Movies.Where(m => m.Id == id).ToList().First();

            //delete the movie and the actors associated with it
            db.Actors.RemoveRange(movieToDelete.Actors);
            db.Movies.Remove(movieToDelete);
            

            //save the changes
            db.SaveChanges();

            return GetMovie();
        }

        [System.Web.Http.HttpPut]
        public string EditMovie(JSONMovie movieToEdit)
        {
            Movie movieBeingEdited = new Movie(movieToEdit);

            //get the actors in the movie
            List<string> actorsInMovie = new List<string>();
            foreach (Actor actor in movieBeingEdited.Actors)
                actorsInMovie.Add(actor.Name);
            List<Actor> actorList = db.Actors.ToList();
            int minId = actorList.OrderBy(a => a.Id).ToList().Last().Id;

            //remove stranded actors
            foreach (Actor actor in actorList)
            {
                if (actor.MovieId == movieBeingEdited.Id && !actorsInMovie.Contains(actor.Name))
                {
                    db.Actors.Remove(db.Actors.Where(a => a.Id == actor.Id).ToList().First());
                }
            }

            //set Id movieId on actors in movie
            for (int i = 0; i < movieBeingEdited.Actors.Count(); i++)
            {
                Actor actor = movieBeingEdited.Actors[i];
                actor.MovieId = movieBeingEdited.Id;

                if (actorList.Where(a => a.Name == actor.Name && a.MovieId == movieBeingEdited.Id).ToList().Count() > 0)
                {
                    actor = actorList.Where(a => a.Name == actor.Name && a.MovieId == movieBeingEdited.Id).ToList().First();
                }
                else
                {
                    actor.Id = minId + 1;
                    db.Actors.Add(actor);
                    db.SaveChanges();
                    minId++;
                }
                movieBeingEdited.Actors[i] = actor;
                db.Actors.Attach(actor);
            }

            db.Entry(movieBeingEdited).State = EntityState.Modified;
            db.SaveChanges();
            return GetMovie();
        }
    }
}