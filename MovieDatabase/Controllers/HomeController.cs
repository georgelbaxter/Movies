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


namespace MovieDatabase.Controllers
{
    public class HomeController : Controller
    {
        private MovieDatabaseContext db = new MovieDatabaseContext();

        public ActionResult Index()
        {
            ViewData.Model = db.Movies.ToList();
            return View();
        }

    }
}