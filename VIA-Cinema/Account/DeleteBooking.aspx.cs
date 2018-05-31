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
    public partial class DeleteBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
                Response.Redirect("Login.aspx");

            if (Request.QueryString["showId"] != null)
            {
                //delete it (checking for userId for security reasons)
                SqlConnection conn = new SqlConnection(
                       ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = @"DELETE FROM Reservations
                                    WHERE showId=@show AND userId=@user";

                cmd.Parameters.Add("@user", SqlDbType.Int);
                cmd.Parameters["@user"].Value = Session["userId"];
                cmd.Parameters.Add("@show", SqlDbType.Int);
                cmd.Parameters["@show"].Value = Convert.ToInt32(Request.QueryString["showId"]);

                cmd.ExecuteNonQuery();
            }
            Response.Redirect("MyAccount.aspx");
        }
    }
}