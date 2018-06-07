using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace VIA_Cinema
{
    public partial class Movie : System.Web.UI.Page
    {
        public string imgSrc;
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

            //if no movie was found with this id
            if(m==null) //redirect to index
                Response.Redirect("index.aspx");

            //show the image
            image.ImageUrl = m.Cover;
            image.CssClass = "movieImage";
            imgSrc = m.Cover;
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
                    days[i].InnerHtml += "<a data-toggle=\"tooltip\" class=\"" + cssClass + "\" href=\"ChooseSeats.aspx?showId=" + s.Id
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

            /*SETUP THE SLIDESHOW*/
            //set a counter
            int count = 0;
            selectors.InnerHtml = "";
            images.InnerHtml = "";
            if (!m.Trailer.Equals(""))
            {
                selectors.InnerHtml += "<li data-target=\"#carouselExampleIndicators\" data-slide-to=\"" + count + "\" class=\"active\"></ li>";
                count++;
                images.InnerHtml += "<div class=\"carousel-item active\">" +
                "<iframe src=\"//www.youtube.com/embed/"+m.Trailer+"\"></iframe></div>";
                carousel_title.InnerText = "Trailer";
            }


            //db connection
            SqlConnection conn = new SqlConnection(
              ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            //command
            SqlCommand cmd = conn.CreateCommand();
            //set the query to get all pictures
            cmd.CommandText = @"SELECT src FROM Gallery WHERE MovieId=@movie";
            //set the parameters
            cmd.Parameters.Add("@movie", SqlDbType.Int);
            cmd.Parameters["@movie"].Value = movieId;
            //read the result
            using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
            {
                while (rd.Read())
                {
                    selectors.InnerHtml += "<li data-target=\"#carouselExampleIndicators\" data-slide-to=\"" + count + "\"";
                    if (count == 0)
                        selectors.InnerHtml += " class=\"active\" ";
                    selectors.InnerHtml+="></ li>";
                    count++;
                    images.InnerHtml += "<div class=\"carousel-item\"><img class=\"d-block w-100";
                    if (count == 1)
                        images.InnerHtml += " active";
                    images.InnerHtml += "\" src=\"" + rd["src"] + "\" alt=\"\"></div>";
                }
            }
            //if there are no pictures neither trailer
            if (count == 0)
            {
                //hide the slideshow (carousel)
                carousel_title.Visible = false;
                carousel_wrapper.Visible = false;
            }else if (count > 1)
            {
                carousel_title.InnerText = "Gallery";
            }
            else
            {
                selectors.Visible = false;
                prev.Visible = false;
                next.Visible = false;
            }

            //print categories
            categories.Text = "";
            foreach(var c in m.Categories)
            {
                categories.Text += ", "+c;
            }
            categories.Text = categories.Text.Substring(2);
        }
    }
}