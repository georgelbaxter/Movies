using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieDatabase.Models
{
    public class JSONMovie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Image { get; set; }
        public virtual List<String> Actors { get; set; }

        public JSONMovie (Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            Year = movie.Year;
            Genre = movie.Genre ?? string.Empty;
            Image = movie.Image ?? string.Empty;
            Actors = new List<string>();
            foreach (Actor actor in movie.Actors)
                Actors.Add(actor.Name);
        }
    }
}