using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace VIA_Cinema.WebService
{
    /// <summary>
    /// Summary description for VIACinemaService
    /// </summary>
    [WebService(Namespace = "http://localhost/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class VIACinemaService : System.Web.Services.WebService
    {

        [WebMethod]
        public Movie[] GetAllMovies()
        {
            return query(0, 0);
        }

        [WebMethod]
        public Movie[] GetMoviesOfDay(int day)
        {
            return query(-1, day);
        }

        [WebMethod]
        public Movie GetMovieInfo(int id)
        {
            if (id <= 0)
                return null;

            return query(id, 0)[0];
        }

        private Movie[] query(int id, int days)
        {
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            string query = @"SELECT     M.MovieID AS MovieID,
                                        M.Title AS Title,
                                        M.Description AS Description,
                                        M.Duration AS Duration,
                                        M.ReleaseDate AS ReleaseDate,
                                        S.ShowId AS ShowID,
                                        S.Date AS Date,
                                        S.RoomId AS RoomId
                                FROM    Movies AS M, Shows AS S
                                WHERE   M.MovieID=S.MovieID";

            if (id > 0)
            {
                query += " AND M.MovieId = " + id;
            }
            else
            {
                if (id < 0)
                    query += " AND CONVERT(date, S.Date) = CONVERT(date, GETDATE( )+"+days+")";
                else
                    query += " AND S.Date >= GETDATE( )";
                query += " ORDER BY M.MovieId, S.ShowId ASC";
            }

            cmd.CommandText = query;
            List<Movie> t = new List<Movie>();
            using (var rd = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
            {
                if (rd.Read())
                {
                    bool more = true;
                    int curId = Int32.Parse(rd["MovieID"].ToString());
                    while (more)
                    {
                        int preId = curId;

                        List<Show> shows = new List<Show>();
                        
                        string title = rd["Title"].ToString();
                        string description = rd["Description"].ToString();
                        int duration = Int32.Parse(rd["Duration"].ToString());
                        string releaseDate = rd["ReleaseDate"].ToString();

                        do
                        {
                            int showId = Int32.Parse(rd["ShowID"].ToString());
                            DateTime date = DateTime.ParseExact(rd["Date"].ToString(), "dd/MM/yyyy HH:mm:ss",
                                System.Globalization.CultureInfo.InvariantCulture);
                            shows.Add(new Show( showId,
                                                date,
                                                Int32.Parse(rd["RoomId"].ToString())
                                              ));
                        } while ((more = rd.Read()) && (curId = Int32.Parse(rd["MovieID"].ToString())) == preId);

                        t.Add(new Movie(preId, title, description, duration, releaseDate, shows.ToArray()));
                    }
                }
            }

            return t.OrderBy(o=>o.ReleaseDate).ToArray();

        }
    }
}
