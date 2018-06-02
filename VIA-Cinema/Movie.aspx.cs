using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace VIA_Cinema
{
    public partial class Movie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if there is no movieId passed, redirect to the main page
            if (Request.QueryString["movieId"] == null)
                Response.Redirect("index.aspx");

            //get the movieId
            int movieId = Convert.ToInt32(Request.QueryString["movieId"]);

            //reference to the web service
            localhost.VIACinemaService via = new localhost.VIACinemaService();

            //get the movie info
            localhost.Movie m = via.GetMovieInfo(movieId);

            //show the image
            image.ImageUrl = m.Cover;
            image.CssClass = "movieImage";
            //set the title, description and other info
            title.Text = m.Title;
            description.Text = m.Description;
            relDate.Text = m.ReleaseDate;
            duration.Text = m.Duration.ToString();

            //put the day name (week name, like "mon", "tus", "wed", etc)
            day2.InnerText = System.DateTime.Now.AddDays(2).DayOfWeek.ToString().Substring(0, 3);
            day3.InnerText = System.DateTime.Now.AddDays(3).DayOfWeek.ToString().Substring(0, 3);
            day4.InnerText = System.DateTime.Now.AddDays(4).DayOfWeek.ToString().Substring(0, 3);
            day5.InnerText = System.DateTime.Now.AddDays(5).DayOfWeek.ToString().Substring(0, 3);
            day6.InnerText = System.DateTime.Now.AddDays(6).DayOfWeek.ToString().Substring(0, 3);

            //save to an indexed array the cells where to put the shows times
            HtmlTableCell[] days = { day0_content, day1_content, day2_content, day3_content, day4_content, day5_content, day6_content };

            //variable to check if there is at least one show this week
            bool check = false;

            //for the 7 days (a week)
            for (int i = 0; i < 7; i++)
            {
                //get all the shows for this movie and this day
                localhost.Show[] shows;
                shows = via.GetShowsOfDay(movieId, i);

                //if there are no shows
                if (shows == null)
                {
                    //set this message
                    days[i].InnerHtml = "<i>No shows this day.</i>";
                    continue;
                }

                //check if there is at least one show this week
                check = true;
                days[i].InnerHtml = "";
                foreach (var s in shows)
                {
                    string cssClass = "btn ";
                    if (s.AvailableSeats > 0)
                        cssClass += "btn-primary";
                    else
                        cssClass += "btn-secondary active";
                    //add a button with the time, which is linked to the booking page
                    days[i].InnerHtml += "<a data-toggle=\"tooltip\" class=\""+cssClass+"\" href=\"ChooseSeats.aspx?showId=" + s.Id
                                            + "\" style=\"font-size: 12px; margin: 2px;\" title=\"Room " + s.Room + ": " + s.AvailableSeats + " seats left\">" +
                                            s.Date.Hour + ":" + s.Date.Minute.ToString("00") + "</a>";
                }
            }

            DateTime rDate = DateTime.ParseExact(m.ReleaseDate, "dd/MM/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            if (!check)
            {
                if (rDate > DateTime.Now)
                    showsWrapper.InnerHtml = "<h4><small>Coming soon!</small></h4>";
                else
                    showsWrapper.InnerHtml = "<h4><small>No shows planned this week.</small></h4>";
            }
        }
    }
}