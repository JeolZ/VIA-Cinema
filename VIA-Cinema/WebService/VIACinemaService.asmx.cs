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
    /// This WebService will send the data of the available movies
    /// </summary>
    [WebService(Namespace = "http://localhost/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class VIACinemaService : System.Web.Services.WebService
    {

        [WebMethod(MessageName = "Get all movies from today, without shows info")]
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
        public Show[] GetShowsOfDay(int id, int day)
        {
            if (id <= 0)
                return null;

            Movie[] m = query(id, day);
            if(m.Length > 0)
                return m[0].Shows;
            return null;
        }

        [WebMethod(MessageName = "Get just movie info, without shows")]
        public Movie GetMovieInfo(int id)
        {
            if (id <= 0)
                return null;

            return query(id, -1)[0];
        }

        //the main method, called from every WebMethod
        private Movie[] query(int id, int days)
        {
            //connect to the database
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            //create the command
            SqlCommand cmd = conn.CreateCommand();
            //set up the query
            string query = @"SELECT M.MovieID AS MovieID,
                                    M.Title AS Title,
                                    M.Description AS Description,
                                    M.Duration AS Duration,
                                    M.ReleaseDate AS ReleaseDate,
                                    M.Cover AS Cover";

            if(id<=0 && days < 0) // if we want just all the movies (without the shows)
            {
                query += @" FROM Movies AS M 
                            WHERE M.MovieId IN (
                                SELECT DISTINCT M.MovieID AS MovieId
                                FROM Movies AS M
                                LEFT JOIN Shows AS S ON M.MovieId=S.MovieId 
                                WHERE M.ReleaseDate > GETDATE( )
                                    OR S.Date >= GETDATE( )
                                )
                            ORDER BY M.ReleaseDate ASC";
            }else if (id > 0 && days < 0) //if we want just the movie info
            {
                //get the fields just from the table Movies
                query += " FROM Movies AS M WHERE M.MovieId = " + id;
            }
            else
            {
                //else, take all the shows info
                query   +=  @", S.ShowId AS ShowID,
                                S.Date AS Date,
                                S.RoomId AS RoomId
                                FROM Movies AS M, Shows AS S
                                WHERE M.MovieID=S.MovieID";
                if (days >= 0) //if we are searching for a specific day, select the date we want
                    query += " AND CONVERT(date, S.Date) = CONVERT(date, GETDATE( )+"+days+")";
                else //else, just take all future movies
                    query += " AND S.Date >= GETDATE( )";

                //if we passed an id, take just the shows of the movie we want
                if (id > 0)
                    query += " AND M.MovieId = " + id;
                
                //ordering
                query += " ORDER BY M.MovieId, S.Date, S.ShowId ASC";
            }

            //save the query
            cmd.CommandText = query;

            //read the results
            List<Movie> t = new List<Movie>();
            using (var rd = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
            {
                if (rd.Read())
                {
                    bool more = true;
                    //get curId
                    int curId = Int32.Parse(rd["MovieID"].ToString());
                    while (more) //until we can read (more is updated below)
                    {
                        //save the current Id, we'll need it later
                        int preId = curId;

                        //create the Movie object
                        Movie m = new Movie(curId,
                                            rd["Title"].ToString(),
                                            rd["Description"].ToString(),
                                            Int32.Parse(rd["Duration"].ToString()),
                                            rd["ReleaseDate"].ToString().Substring(0, 10),
                                            rd["Cover"].ToString(),
                                            null);

                        //if we want the shows
                        if (days >= 0)
                        {
                            //read until the curId is the same (now we need preId)
                            List<Show> shows = new List<Show>();
                            do
                            {
                                //read show data
                                int showId = Int32.Parse(rd["ShowID"].ToString());
                                DateTime date = DateTime.ParseExact(rd["Date"].ToString(), "dd/MM/yyyy HH:mm:ss",
                                    System.Globalization.CultureInfo.InvariantCulture);
                                //add to the list the Show Object
                                shows.Add(new Show(showId,
                                                    date,
                                                    Int32.Parse(rd["RoomId"].ToString()),
                                                    -1
                                                  ));
                            } while ((more = rd.Read()) && (curId = Int32.Parse(rd["MovieID"].ToString())) == preId);
                            //save the show list to the movie object
                            m.Shows = shows.ToArray();
                        }
                        else if(more = rd.Read())
                                curId = Int32.Parse(rd["MovieID"].ToString());
                        t.Add(m); //add the movie object to the list
                    }
                }
            }

            //return the array of movies
            return t.ToArray();

        }
    }
}
