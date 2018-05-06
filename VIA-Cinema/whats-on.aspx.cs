using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VIA_Cinema
{
    public partial class whats_on : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Web service
            localhost.VIACinemaService via = new localhost.VIACinemaService();

            string moviesHtml = "";

            for (int i = 0; i < 7; i++)
            {
                localhost.Movie[] movies = via.GetMoviesOfDay(i);

                moviesHtml += "<h4>Day " + i + ":</h4>";
                foreach (var m in movies)
                {
                    moviesHtml += "<div>";
                    moviesHtml += "<b>" + m.Title + "</b><br />";
                    moviesHtml += "<p>" + m.Description + "</p><br />";
                    moviesHtml += "<span>" + m.Duration + "mins</span><br />";
                    moviesHtml += "<span>";
                    foreach (var s in m.Shows)
                        moviesHtml += " " + s.Date.Hour + ":" + s.Date.Minute;
                    moviesHtml += "</span>";
                    moviesHtml += "</div><hr />";
                }
                moviesHtml += "<br />";
            }

            weekMovies.InnerHtml = moviesHtml;
        }
    }
}