using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VIA_Cinema
{
    public partial class Movies : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //Web service
            localhost.VIACinemaService via = new localhost.VIACinemaService();


            //get all the movies shown or coming soon
            localhost.Movie[] movies = via.GetAllMovies();


            //setup the div to show them
            movieList.InnerHtml = "";
            foreach (var m in movies)
            {
                movieList.InnerHtml += "<div class=\"media\" style=\"margin: 10px;\">";
                movieList.InnerHtml += "<a href=\"Movie.aspx?movieId=" + m.Id + "\">";
                movieList.InnerHtml += "<img class=\"media-left mr-3 mt-3\" src=\"" + m.Cover + "\" style=\"width:150px;\" /></a>";
                movieList.InnerHtml += "<div class=\"media-body\">";
                movieList.InnerHtml += "<div class=\"card\" style=\"min-height: 250px\">";
                movieList.InnerHtml += "<div class=\"card-body\">";
                movieList.InnerHtml += "<a href=\"Movie.aspx?movieId=" + m.Id + "\">";
                movieList.InnerHtml += "<h4 class=\"card-title\">" + m.Title + "</h4></a>";
                if(m.Description.Length > 250)
                    movieList.InnerHtml += "<p class=\"card-text\">" + m.Description.Substring(0, 250) + "...</p>";
                else
                    movieList.InnerHtml += "<p class=\"card-text\">" + m.Description + "</p>";
                movieList.InnerHtml += "<p style=\"font-size: 12px\">Duration: " + m.Duration + "'</p>";
                movieList.InnerHtml += "<p style=\"font-size: 12px\">Release Date: " + m.ReleaseDate + "'</p>";
                movieList.InnerHtml += "</div></div></div></div>";
            }
        }
    }
}