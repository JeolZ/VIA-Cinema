using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VIA_Cinema.WebService
{
    [Serializable]
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string ReleaseDate { get; set; }
        public Show[] Shows { get; set; }
        public string Cover { get; set; }
        public string Trailer { get; set; }
        public string[] Categories { get; set; }

        public Movie()
        {
        }

        public Movie(int id, string t, string d, int dur, string rd, string c, string tr)
        {
            Id = id;
            Title = t;
            Description = d;
            Duration = dur;
            ReleaseDate = rd;
            Cover = c;
            Trailer = tr;
            Shows = null;
            Categories = null;
        }
    }
}