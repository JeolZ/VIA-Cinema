using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace VIA_Cinema
{
    public partial class WhatsOn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Web service
            localhost.VIACinemaService via = new localhost.VIACinemaService();
            
            //setting the days names
            d2.InnerText = System.DateTime.Now.AddDays(2).DayOfWeek.ToString().Substring(0, 3);
            d3.InnerText = System.DateTime.Now.AddDays(3).DayOfWeek.ToString().Substring(0, 3);
            d4.InnerText = System.DateTime.Now.AddDays(4).DayOfWeek.ToString().Substring(0, 3);
            d5.InnerText = System.DateTime.Now.AddDays(5).DayOfWeek.ToString().Substring(0, 3);
            d6.InnerText = System.DateTime.Now.AddDays(6).DayOfWeek.ToString().Substring(0, 3);
            
            //save the cells to an indexed array
            HtmlGenericControl[] days = { day0, day1, day2, day3, day4, day5, day6 };
            for (int i = 0; i < 7; i++)
            {
                //get all the movies for the current day (today+i)
                localhost.Movie[] movies = via.GetMoviesOfDay(i);

                //if there are no movies for today, show this message
                if (movies.Length == 0)
                {
                    days[i].InnerHtml = "<i>No films today.</i>";
                    continue;
                }

                //else, setup the div to show them
                days[i].InnerHtml = "";
                foreach (var m in movies)
                {
                    days[i].InnerHtml += "<div class=\"media\" style=\"margin: 10px;\">";
                    days[i].InnerHtml += "<a href=\"Movie.aspx?movieId="+m.Id+"\">";
                    days[i].InnerHtml += "<img class=\"media-left mr-3 mt-3\" src=\""+m.Cover+"\" style=\"width:150px;\" /></a>";
                    days[i].InnerHtml += "<div class=\"media-body\">";
                    days[i].InnerHtml += "<div class=\"card\" style=\"min-height: 250px\">";
                    days[i].InnerHtml += "<div class=\"card-body\">";
                    days[i].InnerHtml += "<a href=\"Movie.aspx?movieId=" + m.Id + "\">";
                    days[i].InnerHtml += "<h4 class=\"card-title\">" + m.Title + "</h4></a>";
                    days[i].InnerHtml += "<p class=\"card-text\">" + m.Description.Substring(0, 250) + "...</p>";
                    days[i].InnerHtml += "<p style=\"font-size: 12px\">Duration: " + m.Duration + "'</p>";
                    foreach (var s in m.Shows)
                        days[i].InnerHtml += "<a data-toggle=\"tooltip\" class=\"btn btn-primary\" href=\"ChooseSeats.aspx?showId=" + s.Id
                                                + "\" style=\"font-size: 12px; margin: 2px;\" title=\"Room "+s.Room+"\">" +
                                                s.Date.Hour + ":" + s.Date.Minute.ToString("00")+"</a>";
                    days[i].InnerHtml += "</div></div></div></div>";
                }
            }
        }
    }
}