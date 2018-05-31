using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VIA_Cinema.Account
{
    public partial class MyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userId"]==null)
                Response.Redirect("Login.aspx");


            //retrieve active bookings
            SqlConnection conn = new SqlConnection(
                   ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT R.SeatN AS seat, S.Date AS Date, S.RoomId AS Room, M.Title AS Title 
                                FROM Reservations AS R, Shows AS S, Movies AS M 
                                WHERE R.ShowId = S.ShowId AND S.MovieId = M.MovieId 
                                    AND UserId = @id AND S.Date > GETDATE( )";

            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = Session["userId"];

            using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
            {
                while (rd.Read())
                {

                }
            }
        }
    }
}