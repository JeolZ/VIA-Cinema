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
        public Movie[] GetMovies()
        {
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT  M.MovieID AS MovieID,
                                        M.Title AS Title,
                                        M.Description AS Description,
                                        M.Duration AS Duration,
                                        M.ReleaseDate AS ReleaseDate,
                                        S.Date AS Date,
                                        S.Time AS Time,
                                        S.RoomId AS RoomId
                                FROM    Movies AS M, Shows AS S
                                WHERE   M.MovieID=S.MovieID
                                    AND S.Date >= GETDATE( )";


            List<Movie> t = new List<Movie>();
            using (SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
            {
                if (rd.Read())
                {
                    bool more = true;
                    int curId = Int32.Parse(rd["MovieID"].ToString());
                    while (more)
                    {
                        int preId = curId;
                        //int id = rd.GetInt32(1); //MovieID

                        List<Show> shows = new List<Show>();

                        string title = rd["Title"].ToString();
                        //string title = rd.GetString(2); //Title
                        string description = rd["Description"].ToString();
                        //string description = rd.GetString(3); //Description
                        int duration = Int32.Parse(rd["Duration"].ToString());
                        //int duration = rd.GetInt32(4); //Duration
                        string releaseDate = rd["ReleaseDate"].ToString();
                        //string releaseDate = rd.GetString(5); //Release date

                        do
                        {
                            /*shows.Add(new Show( rd.GetString(6),    //Date
                                                rd.GetString(7),    //Time
                                                rd.GetInt32(8)));   //Room*/
                            shows.Add(new Show( rd["Date"].ToString(),
                                                rd["Time"].ToString(),
                                                Int32.Parse(rd["RoomId"].ToString())
                                              ));
                        } while ( (more=rd.Read()) && (curId = Int32.Parse(rd["MovieID"].ToString())) == preId);

                        t.Add( new Movie(title, description, duration, releaseDate, shows.ToArray()) );
                    }
                }
            }

            return t.ToArray();
        }
    }
}
