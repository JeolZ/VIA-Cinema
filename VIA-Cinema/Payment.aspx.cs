using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VIA_Cinema.com.ftipgw.secure;

namespace VIA_Cinema
{
    public partial class Payment : System.Web.UI.Page
    {
        private List<string> seats;
        private string showId;
        private float totPrice;

        protected void Page_Load(object sender, EventArgs e)
        {
            seats = (List<string>)Session["seats"];
            showId = (string)Session["showId"];

            if(seats==null || showId == null || Session["price"]==null)
                Response.Redirect("index.aspx");

            totPrice = (float)Session["price"];
            totPrice *= seats.Count;

            string summ = @"Summarize your order<br />Chosen seats: ";

            foreach (string seat in seats)
                summ += seat + " ";

            summ += "<br />Total price: €" + totPrice + "<br /><br />";

            summarize.Text = summ;
        }

        protected void CheckOrder(object sender, EventArgs e)
        {
            formError.Text = "";
            //check credit card
            CreditCardValidator c = new CreditCardValidator();
            int check = c.ValidCard(cardn.Text, exp.Text.Remove(2,1));
            if (check!=0)
            {
                formError.Text = "Error " + check + "! Please check your Credit Card data.<br />";
                return;
            }


            Regex code_valid = new Regex(@"^\d{3}$", RegexOptions.IgnoreCase);
            if (!code_valid.Match(code.Text).Success)
            {
                formError.Text = "Please insert a valid 3-digits code.<br />";
                return;
            }

            if (owner.Text.Equals(""))
            {
                formError.Text = "Please insert an owner.<br />";
                return;
            }

            //insert into database
            SqlConnection conn = new SqlConnection(
              ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            string query = @"INSERT INTO Reservations (SeatN, ShowId, CreditCardN";
            if (Session["userId"] != null)
                query += ", UserId";
            query += ") VALUES(@seat, @showId, @card";
            if (Session["userId"] != null)
                query += ", @userId";
            query += ");";

            cmd.CommandText = query;
            cmd.Parameters.Add("@seat", SqlDbType.VarChar);
            cmd.Parameters.Add("@showId", SqlDbType.Int);
            cmd.Parameters.Add("@card", SqlDbType.Char);
            cmd.Parameters["@showId"].Value = showId;
            cmd.Parameters["@card"].Value = cardn.Text;

            if (Session["userId"] != null)
            {
                cmd.Parameters.Add("@userId", SqlDbType.Int);
                cmd.Parameters["@userId"].Value = (int)Session["userId"];
            }

            foreach(var seat in seats)
            {
                cmd.Parameters["@seat"].Value = seat;
                cmd.ExecuteNonQuery();
            }
        }
    }
}