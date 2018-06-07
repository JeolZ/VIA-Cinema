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
        //global attributes
        private List<string> seats;
        private string showId;
        private float totPrice;

        protected void Page_Load(object sender, EventArgs e)
        {
            //hide the error div
            formError.Visible = false;
            
            //if it's not logged in, hide the dropdown list and the checkbox
            if (Session["userId"] == null)
            {
                saveCardWrapper.Visible = false;
                savedCardsWrapper.InnerHtml = "<p style=\"font-size: 12px\"><a href=\"Account/LogIn.aspx?redirect=../Payment.aspx\""+
                    "class=\"btn btn-primary\">Log In</a><br />to see your saved cards and to save the booking into your account</p>";
            }

            //take the seats chosen and the showId
            seats = (List<string>)Session["seats"];
            showId = (string)Session["showId"];

            //if something is null (we arrived here without passing through the booking page)
            //redirect to the main page
            if(seats==null || showId == null || Session["price"]==null)
                Response.Redirect("index.aspx");

            //set link
            back.NavigateUrl = "ChooseSeats.aspx?showId=" + showId;

            //save the tot price
            totPrice = (float)Session["price"];
            totPrice *= seats.Count;

            //setting the summary of the purchase
            string summ = "<p><b>Movie:</b> " + Session["title"] + "<br />";
            summ += "<b>Room:</b> " + Session["room"] + "<br />";
            summ += "<b>Date and Time:</b> " + Session["date"] + "<br />";
            summ += "<b>Chosen seats:</b> ";
            foreach (string seat in seats)
                summ += seat + " ";

            summ += "<br /><b>Total price:</b> €" + totPrice + "</p>";

            summary.Text = summ;

            //put the first item into the drop down list for the saved cards
            ListItem li = new ListItem();
            li.Value = "None";
            li.Text = "Select one of your saved cards";
            savedCards.Items.Add(li);
            //if the user is not logged in
            if (Session["userId"] == null)
            {
                //add a "message" at the end of the drop down text
                savedCards.Items[0].Text += " (you must be logged in)";
                //and disable it
                savedCards.Enabled = false;
            }

            //if it's logged in
            if (Session["userId"] != null)
            {
                //connect to the db
                SqlConnection conn = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();
                //create the command variable
                SqlCommand cmd = conn.CreateCommand();

                //set the query to retrieve all saved credit cards
                cmd.CommandText = @"SELECT CreditCardN, ExpirationDate, Owner FROM CreditCards WHERE UserId=@userId";
                //set the parameters
                cmd.Parameters.Add("@userId", SqlDbType.Int);
                cmd.Parameters["@userId"].Value = Convert.ToInt32(Session["userId"]);

                //read the results
                using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
                {
                    while (rd.Read())
                    {
                        //take the credit card number and the expiration date
                        string cardN = rd["CreditCardN"].ToString();
                        string expDate = rd["ExpirationDate"].ToString();
                        string owner = rd["Owner"].ToString();

                        //check if they're still valid
                        CreditCardValidator c = new CreditCardValidator();
                        int check = c.ValidCard(cardN, expDate);

                        //if they are not, skip it
                        if (check != 0)
                            continue;

                        //else, put the card inside the drop down list
                        ListItem l = new ListItem();
                        l.Value = cardN;
                        l.Text = "****-****-****-"+cardN.Substring(12)+" "+expDate.Substring(0, 2)+"/"+expDate.Substring(2)+" "+owner;
                        savedCards.Items.Add(l);
                    }
                }
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
                //we don't need to check it (we did when we retrieved it from the db)
                cardn.Value = savedCards.SelectedValue;
            }
            else
            {
                //check credit card
                CreditCardValidator c = new CreditCardValidator();
                string expDate = "";
                //if the field has been set
                if (exp.Value.Length>6)
                    //convert it into the form mmyy
                    expDate = exp.Value.Substring(5) + exp.Value.Substring(2, 2);

                //check the credit card validity
                int check = c.ValidCard(cardn.Value, expDate);
                if (check != 0)
                {
                    //if it's not valid, show an error
                    formError.InnerHtml = "<p>Error " + check + "! Please check your Credit Card data.</p>";
                    formError.Visible = true;
                    return;
                }

                //regex for the CVV code (3 digits)
                Regex code_valid = new Regex(@"^\d{3}$", RegexOptions.IgnoreCase);
                if (!code_valid.Match(code.Value).Success)
                {
                    formError.InnerHtml = "Please insert a valid 3-digits code.<br />";
                    formError.Visible = true;
                    return;
                }

                //if it has not been inserted an owner name, show an error
                if (owner.Value.Equals(""))
                {
                    formError.InnerHtml = "Please insert an owner.<br />";
                    formError.Visible = true;
                    return;
                }

                //if the user wants to save the card
                if (saveCard.Checked && Session["userId"]!=null)
                {
                    //create the command
                    SqlCommand comm = conn.CreateCommand();
                    //set the query
                    comm.CommandText = @"INSERT INTO CreditCards
                                        (UserId, CreditCardN, ExpirationDate, Owner, SecCode)
                                        VALUES(@userId, @card, @exp, @own, @code)";
                    //set the parameters
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
                    //execute the query to insert the credit card
                    comm.ExecuteNonQuery();
                }
            }

            cmd.CommandText = "SELECT MAX(ReservationId) FROM Reservations";
            string resId = cmd.ExecuteScalar().ToString();

            resId = ComputeNextID(resId);

            //insert into database the reservation (connected to the userId, if it's logged in)
            string query = @"INSERT INTO Reservations (ReservationId, SeatN, ShowId, CreditCardN";
            if (Session["userId"] != null)
                query += ", UserId";
            query += ") VALUES(@resId, @seat, @showId, @card";
            if (Session["userId"] != null)
                query += ", @userId";
            query += ");";

            //set the query and the parameters
            cmd.CommandText = query;
            cmd.Parameters.Add("@resId", SqlDbType.Char);
            cmd.Parameters.Add("@seat", SqlDbType.VarChar);
            cmd.Parameters.Add("@showId", SqlDbType.Int);
            cmd.Parameters.Add("@card", SqlDbType.Char);
            cmd.Parameters["@resId"].Value = resId;
            cmd.Parameters["@showId"].Value = showId;
            cmd.Parameters["@card"].Value = cardn.Value;
            if (Session["userId"] != null)
            {
                cmd.Parameters.Add("@userId", SqlDbType.Int);
                cmd.Parameters["@userId"].Value = (int)Session["userId"];
            }

            //execute the queries changing the seat number
            foreach(var seat in seats)
            {
                cmd.Parameters["@seat"].Value = seat;
                cmd.ExecuteNonQuery();
            }

            //reset the session variables
            Session["seats"] = null;
            Session["showId"] = null;
            Session["price"] = null;
            Session["title"] = null;
            Session["room"] = null;
            Session["date"] = null;

            //redirect to the ThankYou page
            Response.Redirect("ThankYou.aspx?id="+resId);
        }

        private string ComputeNextID(string prevId)
        {
            char[] dig = new char[36];
            for (char c = '0'; c <= '9'; c++)
                dig[c - '0'] = c;
            for (char c = 'A'; c <= 'Z'; c++)
                dig[c - 'A' + 10] = c;

            char[] curId = prevId.ToCharArray();
            for(int i=prevId.Length-1; i>=0; i--)
            {
                if (prevId[i] < dig[9]) {
                    curId[i] = (char)(prevId[i] + 1);
                    break;
                } else if (prevId[i] == dig[9]) {
                    curId[i] = 'A';
                    break;
                } else if (prevId[i] < dig[35])
                {
                    curId[i] = (char)(prevId[i] + 1);
                    break;
                }
                curId[i] = '0';
            }

            return new string(curId);
        }
    }
}