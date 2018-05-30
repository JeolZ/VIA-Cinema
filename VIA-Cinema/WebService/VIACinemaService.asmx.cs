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

        [WebMethod(MessageName = "Get all movies from today")]
        public Movie[] GetAllMovies()
        {
            return query(0, -1);
        }

        [WebMethod(MessageName = "Get shows of all movies of \"day\" days from today")]
        public Movie[] GetMoviesOfDay(int day)
        {
            return query(-1, day);
        }

        [WebMethod(MessageName = "Get shows of \"day\" days from today for the movie with MovieID = id")]
        public Movie[] GetShowsOfDay(int id, int day)
        {
            return query(id, day);
        }

        [WebMethod(MessageName = "Get just movie info, without shows")]
        public Movie GetMovieInfo(int id)
        {
            if (id <= 0)
                return null;

            return query(id, -1)[0];
        }

        private Movie[] query(int id, int days)
        {
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            string query = @"SELECT M.MovieID AS MovieID,
                                    M.Title AS Title,
                                    M.Description AS Description,
                                    M.Duration AS Duration,
                                    M.ReleaseDate AS ReleaseDate";

            if (id > 0 && days < 0)
            {
                query += " FROM Movies AS M WHERE M.MovieId = " + id;
            }
            else
            {
                query   +=  @", S.ShowId AS ShowID,
                                S.Date AS Date,
                                S.RoomId AS RoomId,
                                COUNT(*) AS TotalSeats
                                FROM Movies AS M, Shows AS S, Seats AS Se
                                WHERE M.MovieID=S.MovieID, Se.RoomID=S.RoomID";
                if (days >= 0)
                    query += " AND CONVERT(date, S.Date) = CONVERT(date, GETDATE( )+"+days+")";
                else
                    query += " AND S.Date >= GETDATE( )";

                if (id > 0)
                    query += " AND M.MovieId = " + id;

                query += " GROUP BY S.ShowID, S.Date, S.RoomId";
                query += " ORDER BY M.MovieId, S.Date, S.ShowId ASC";
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

                        Movie m = new Movie(curId,
                                            rd["Title"].ToString(),
                                            rd["Description"].ToString(),
                                            Int32.Parse(rd["Duration"].ToString()),
                                            rd["ReleaseDate"].ToString(),
                                            null);

                        List<Show> shows = new List<Show>();
                        do
                        {
                            int showId = Int32.Parse(rd["ShowID"].ToString());
                            DateTime date = DateTime.ParseExact(rd["Date"].ToString(), "dd/MM/yyyy HH:mm:ss",
                                System.Globalization.CultureInfo.InvariantCulture);
                            shows.Add(new Show( showId,
                                                date,
                                                Int32.Parse(rd["RoomId"].ToString()),
                                                Int32.Parse(rd["TotalSeats"].ToString())
                                              ));
                        } while ((more = rd.Read()) && (curId = Int32.Parse(rd["MovieID"].ToString())) == preId);

                        m.Shows = shows.ToArray();

                        t.Add(m);
                    }
                }
            }

            return t.ToArray();

        }
    }
}
