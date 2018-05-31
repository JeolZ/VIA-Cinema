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
            //if it's not logged in, redirect to login
            if (Session["userId"] == null)
                Response.Redirect("Login.aspx");

            //if there is the passed showId
            if (Request.QueryString["showId"] != null)
            {
                //delete the reservation

                //db connection
                SqlConnection conn = new SqlConnection(
                       ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();
                //command
                SqlCommand cmd = conn.CreateCommand();
                //set the query
                cmd.CommandText = @"DELETE FROM Reservations
                                    WHERE showId=@show AND userId=@user";
                //set the parameters in the query
                cmd.Parameters.Add("@user", SqlDbType.Int);
                cmd.Parameters["@user"].Value = Session["userId"];
                cmd.Parameters.Add("@show", SqlDbType.Int);
                cmd.Parameters["@show"].Value = Convert.ToInt32(Request.QueryString["showId"]);
                //execute the query
                cmd.ExecuteNonQuery();
            }
            //redirect back to the MyAccount page
            Response.Redirect("MyAccount.aspx");
        }
    }
}