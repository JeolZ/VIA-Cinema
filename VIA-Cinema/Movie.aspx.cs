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
            if (Request.QueryString["movieId"] == null)
                Response.Redirect("index.aspx");

            int movieId = Convert.ToInt32(Request.QueryString["movieId"]);


            localhost.VIACinemaService via = new localhost.VIACinemaService();

            localhost.Movie m = via.GetMovieInfo(movieId);

            image.ImageUrl = m.Cover;
            image.CssClass = "movieImage";
            title.Text = m.Title;
            description.Text = m.Description;
            relDate.Text = m.ReleaseDate.ToString();
            duration.Text = m.Duration.ToString();


            day2.InnerText = System.DateTime.Now.AddDays(2).DayOfWeek.ToString().Substring(0, 3);
            day3.InnerText = System.DateTime.Now.AddDays(3).DayOfWeek.ToString().Substring(0, 3);
            day4.InnerText = System.DateTime.Now.AddDays(4).DayOfWeek.ToString().Substring(0, 3);
            day5.InnerText = System.DateTime.Now.AddDays(5).DayOfWeek.ToString().Substring(0, 3);
            day6.InnerText = System.DateTime.Now.AddDays(6).DayOfWeek.ToString().Substring(0, 3);

            HtmlTableCell[] days = { day0_content, day1_content, day2_content, day3_content, day4_content, day5_content, day6_content };
            
            for (int i = 0; i < 7; i++)
            {
                localhost.Show[] shows;
                shows = via.GetShowsOfDay(movieId, i);
                if (shows == null)
                {
                    days[i].InnerHtml = "<i>No shows this day.</i>";
                    continue;
                }

                days[i].InnerHtml = "";
                foreach (var s in shows)
                {
                    days[i].InnerHtml += "<a data-toggle=\"tooltip\" class=\"btn btn-primary\" href=\"ChooseSeats.aspx?showId=" + s.Id
                                            + "\" style=\"font-size: 12px; margin: 2px;\" title=\"Room " + s.Room + "\">" +
                                            s.Date.Hour + ":" + s.Date.Minute.ToString("00") + "</a>";
                }
            }
        }
    }
}