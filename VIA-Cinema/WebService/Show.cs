using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VIA_Cinema.WebService
{
    [Serializable]
    public class Show
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Room { get; set; }
        public int AvailableSeats { get; set; }

        public Show()
        {
        }

        public Show(int id, DateTime d, int r, int ts)
        {
            Id = id;
            Date = d;
            Room = r;
            AvailableSeats = ts;
        }
    }
}