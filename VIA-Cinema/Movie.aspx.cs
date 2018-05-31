using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VIA_Cinema
{
    public partial class Movie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["movieId"] == null)
                Response.Redirect("index.aspx");

            int movieId = Convert.ToInt32(Request.QueryString["movieId"]);


            localhost.VIACinemaService via = new localhost.VIACinemaService();

            localhost.Movie m = via.GetMovieInfo(movieId);

            /*
             * print all movie info
             * in a nice way
             * 
             * */



            localhost.Show[] shows;
            for (int i = 0; i < 7; i++)
                 shows = via.GetShowsOfDay(movieId, 0);

            /*
             * print all the shows of the week
             * 
             * */
        }
    }
}