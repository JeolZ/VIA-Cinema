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
            formError.Visible = false;
            if (Session["userId"] == null)
            {
                saveCardWrapper.Visible = false;
                savedCardsWrapper.Visible = false;
            }

            seats = (List<string>)Session["seats"];
            showId = (string)Session["showId"];

            if(seats==null || showId == null || Session["price"]==null)
                Response.Redirect("index.aspx");

            totPrice = (float)Session["price"];
            totPrice *= seats.Count;

            string summ = "<p><b>Movie:</b> " + Session["title"] + "<br />";
            summ += "<b>Room:</b> " + Session["room"] + "<br />";
            summ += "<b>Date and Time:</b> " + Session["date"] + "<br />";
            summ += "<b>Chosen seats:</b> ";
            foreach (string seat in seats)
                summ += seat + " ";

            summ += "<br /><b>Total price:</b> €" + totPrice + "</p>";

            summary.Text = summ;

            if (Session["userId"] != null)
            {
                SqlConnection conn = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = @"SELECT CreditCardN, ExpirationDate FROM CreditCards WHERE UserId=@userId";
                cmd.Parameters.Add("@userId", SqlDbType.Int);
                cmd.Parameters["@userId"].Value = Convert.ToInt32(Session["userId"]);

                ListItem li = new ListItem();
                li.Value = "None";
                li.Text = "None";
                savedCards.Items.Add(li);

                using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
                {
                    while (rd.Read())
                    {
                        string cardN = rd["CreditCardN"].ToString();
                        string expDate = rd["ExpirationDate"].ToString();

                        CreditCardValidator c = new CreditCardValidator();
                        int check = c.ValidCard(cardN, expDate);

                        if (check != 0)
                            continue;

                        ListItem l = new ListItem();
                        l.Value = cardN;
                        l.Text = "************"+l.Value.Substring(12);
                        savedCards.Items.Add(l);
                    }
                }
            }
            else
            {

            }
        }

        protected void CheckOrder(object sender, EventArgs e)
        {
            //clear error
            formError.Visible = false;
            formError.InnerHtml = "";
            //database connection
            SqlConnection conn = new SqlConnection(
              ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();

            //If we selected a saved card
            if (Session["userId"]!=null & !savedCards.SelectedValue.Equals("None"))
            {
                //we don't need to check it
                cardn.Value = savedCards.SelectedValue;
            }
            else
            {
                //check credit card
                CreditCardValidator c = new CreditCardValidator();
                string expDate = "";
                if (exp.Value.Length>6)
                    expDate = exp.Value.Substring(5) + exp.Value.Substring(2, 2);
                int check = c.ValidCard(cardn.Value, expDate);
                if (check != 0)
                {
                    formError.InnerHtml = "<p>Error " + check + "! Please check your Credit Card data.</p>";
                    formError.Visible = true;
                    return;
                }


                Regex code_valid = new Regex(@"^\d{3}$", RegexOptions.IgnoreCase);
                if (!code_valid.Match(code.Value).Success)
                {
                    formError.InnerHtml = "Please insert a valid 3-digits code.<br />";
                    formError.Visible = true;
                    return;
                }

                if (owner.Value.Equals(""))
                {
                    formError.InnerHtml = "Please insert an owner.<br />";
                    formError.Visible = true;
                    return;
                }

                if (saveCard.Checked && Session["userId"]!=null)
                {
                    SqlCommand comm = conn.CreateCommand();
                    comm.CommandText = @"INSERT INTO CreditCards
                                        (UserId, CreditCardN, ExpirationDate, Owner, SecCode)
                                        VALUES(@userId, @card, @exp, @own, @code)";
                    comm.Parameters.Add("@userId", SqlDbType.Int);
                    comm.Parameters.Add("@card", SqlDbType.Char);
                    comm.Parameters.Add("@exp", SqlDbType.Char);
                    comm.Parameters.Add("@own", SqlDbType.VarChar);
                    comm.Parameters.Add("@code", SqlDbType.Char);
                    comm.Parameters["@userId"].Value = Session["userId"];
                    comm.Parameters["@card"].Value = cardn.Value;
                    comm.Parameters["@exp"].Value = expDate;
                    comm.Parameters["@own"].Value = owner.Value;
                    comm.Parameters["@code"].Value = code.Value;

                    comm.ExecuteNonQuery();
                }
            }

            //insert into database
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
            cmd.Parameters["@card"].Value = cardn.Value;

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

            Session["seats"] = null;
            Session["showId"] = null;
            Session["price"] = null;

            Response.Redirect("ThankYou.aspx");
        }
    }
}