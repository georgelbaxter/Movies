using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieDatabase.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Image { get; set; }
        public virtual List<Actor> Actors { get; set; }

        public Movie()
        {
            Actors = new List<Actor>();
        }

        public Movie(JSONMovie inputMovie)
        {
            Id = inputMovie.Id;
            Name = inputMovie.Name;
            Year = inputMovie.Year;
            Genre = inputMovie.Genre;
            Image = inputMovie.Image;
            foreach (string actor in inputMovie.Actors)
            {
                Actors.Add(new Actor() { Name = actor });
            }
        }
    }
}