using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieDatabase.Models
{
    public class MovieActors
    {
        [Column(Order = 0) Key]
        public int ActorId { get; set; }
        [Column(Order = 1) Key]
        public int MovieId { get; set; }
    }
}