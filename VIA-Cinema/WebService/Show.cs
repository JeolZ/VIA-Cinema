using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VIA_Cinema.WebService
{
    [Serializable]
    public class Show
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public int Room { get; set; }

        public Show()
        {
        }

        public Show(string d, string t, int r)
        {
            Date = d;
            Time = t;
            Room = r;
        }
    }
}