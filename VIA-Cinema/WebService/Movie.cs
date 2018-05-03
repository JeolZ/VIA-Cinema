using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VIA_Cinema.WebService
{
    [Serializable]
    public class Movie
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string ReleaseDate { get; set; }
        public Show[] Shows { get; set; }

        public Movie()
        {
        }

        public Movie(string t, string d, int dur, string rd, Show[] s)
        {
            Title = t;
            Description = d;
            Duration = dur;
            ReleaseDate = rd;
            Shows = s;
        }
    }
}