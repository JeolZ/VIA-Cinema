using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace VIA_Cinema.Account
{
    public partial class MyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["userId"]==null)
                Response.Redirect("Login.aspx");

            formError1.Visible = false;
            formError2.Visible = false;

            hi.Text = "Hi, " + Session["name"];

            email.Value = Session["Email"].ToString();
            name.Value = Session["name"].ToString();
            surname.Value = Session["surname"].ToString();

            //retrieve active bookings
            SqlConnection conn = new SqlConnection(
                   ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT S.ShowId AS ShowId, COUNT(*) AS seat, S.Date AS Date, S.RoomId AS Room, M.Title AS Title 
                                FROM Reservations AS R, Shows AS S, Movies AS M 
                                WHERE R.ShowId = S.ShowId AND S.MovieId = M.MovieId 
                                    AND UserId = @id AND S.Date > GETDATE( )
                                GROUP BY S.ShowId, S.Date, S.RoomId, M.Title";

            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = Session["userId"];

            using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
            {
                int i = 0;
                while (rd.Read())
                {
                    int showId = Convert.ToInt32(rd["ShowId"].ToString());
                    int seats = Convert.ToInt32(rd["seat"].ToString());
                    string date = rd["Date"].ToString().Substring(0, 16);
                    string room = rd["Room"].ToString();
                    string title = rd["Title"].ToString();

                    bookingsList.Rows.Add(new HtmlTableRow());

                    HtmlTableCell cell = new HtmlTableCell();
                    cell.InnerHtml = seats + "seats reserved for <b>" + title + "</b> on " + date + " in Room " + room + ".";
                    bookingsList.Rows[i].Cells.Add(cell);
                    cell = new HtmlTableCell();
                    cell.InnerHtml = "<a class=\"btn btn-primary\" href=\"DeleteBooking.aspx?showId=" + showId + "\">Delete</a>";
                    bookingsList.Rows[i].Cells.Add(cell);
                    i++;
                }
                if (i == 0)
                    bookingsList.InnerHtml = "<i>No bookings found</i>";
            }
            conn.Close();
        }

        protected void EditData(object sender, EventArgs e)
        {
            formError1.Visible = false;
            SqlConnection conn = new SqlConnection(
                   ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            string err = "";

            Regex name_valid = new Regex(@"^[a-zA-Z\s]*$");

            if (!name_valid.Match(name.Value).Success)
                err += "Insert a valid name.<br />";

            if (!name_valid.Match(surname.Value).Success)
                err += "Insert a valid surname.<br />";

            if (err.Equals(""))
            {
                cmd.CommandText = @"UPDATE Registries SET Name=@name, Surname=@surname WHERE UserId=@id";

                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = Convert.ToInt32(Session["userId"]);
                cmd.Parameters.Add("@name", SqlDbType.VarChar);
                cmd.Parameters["@name"].Value = name.Value;
                cmd.Parameters.Add("@surname", SqlDbType.VarChar);
                cmd.Parameters["@surname"].Value = surname.Value;

                cmd.ExecuteNonQuery();
            }
            else
            {
                formError1.InnerHtml = "<p>" + err + "</p>";
                formError1.Visible = true;
            }
            conn.Close();
        }

        protected void ResetPassword(object sender, EventArgs e)
        {
            formError2.Visible = false;
            SqlConnection conn = new SqlConnection(
                   ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            string err = "";

            if(!pswOld.Equals("") && !password.Equals(""))
            {
                cmd.CommandText = @"SELECT UserId FROM Users WHERE UserId=@id
                                     AND Password=HASHBYTES('SHA2_256',@password);";

                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = Convert.ToInt32(Session["userId"]);
                cmd.Parameters.Add("@password", SqlDbType.VarBinary);
                cmd.Parameters["@password"].Value = Encoding.ASCII.GetBytes(pswOld.Value);
                
                var userId = cmd.ExecuteScalar();

                if (userId == null)
                    err += "The old password is incorrect. Try again.<br />";
                else
                {
                    if (!password.Value.Equals(password2.Value))
                        err += "The two password are not the same.<br />";
                    else
                    {
                        cmd.CommandText = @"UPDATE Users SET Password=HASHBYTES('SHA2_256',@newPassword) WHERE UserId=@id;";
                        cmd.Parameters.Add("@newPassword", SqlDbType.VarBinary);
                        cmd.Parameters["@newPassword"].Value = Encoding.ASCII.GetBytes(password.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            if(!err.Equals(""))
            {
                formError2.InnerHtml = "<p>" + err + "</p>";
                formError2.Visible = true;
            }
            conn.Close();
        }
    }
}