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
using VIA_Cinema.com.ftipgw.secure;

namespace VIA_Cinema.Account
{
    public partial class MyAccount : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //initialize the personal data fields
            email.Value = Session["Email"].ToString();
            name.Value = Session["name"].ToString();
            surname.Value = Session["surname"].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if it's not logged in, redirect to the login
            if(Session["userId"]==null)
                Response.Redirect("Login.aspx");

            //hide the error divs
            formError1.Visible = false;
            formError2.Visible = false;
            newCardError.Visible = false;
            
            //set up the hi message
            hi.Text = "Hi, " + Session["name"];

            //set log out and (maybe) admin button
            log.InnerHtml = "<a href = \"LogOut.aspx\" class=\"btn btn-primary\">Log out</a>";
            if (Session["admin"] != null && Convert.ToBoolean(Session["admin"]))
                log.InnerHtml += "<br /><a href = \"../Administration/admin.aspx\" class=\"btn btn-secondary\">Administration</a>";

            //retrieve active bookings
            //db connection
            SqlConnection conn = new SqlConnection(
                   ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            //command
            SqlCommand cmd = conn.CreateCommand();
            //set the query
            cmd.CommandText = @"SELECT S.ShowId AS ShowId, COUNT(*) AS seat, S.Date AS Date, S.RoomId AS Room, M.Title AS Title 
                                FROM Reservations AS R, Shows AS S, Movies AS M 
                                WHERE R.ShowId = S.ShowId AND S.MovieId = M.MovieId 
                                    AND UserId = @id AND S.Date > GETDATE( )
                                GROUP BY S.ShowId, S.Date, S.RoomId, M.Title";
            //set the parameters
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = Session["userId"];

            //read the result
            using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
            {
                int i = 0;
                while (rd.Read())
                {
                    //get showId, seat count, date, room and title
                    int showId = Convert.ToInt32(rd["ShowId"].ToString());
                    int seats = Convert.ToInt32(rd["seat"].ToString());
                    string date = rd["Date"].ToString().Substring(0, 16);
                    string room = rd["Room"].ToString();
                    string title = rd["Title"].ToString();
                    
                    //add a new row to the table
                    bookingsList.Rows.Add(new HtmlTableRow());
                    
                    //add a cell with a summary of the reservation
                    HtmlTableCell cell = new HtmlTableCell();
                    cell.InnerHtml = seats + " seats reserved for <b>" + title + "</b> on " + date + " in Room " + room + ".";
                    bookingsList.Rows[i].Cells.Add(cell);

                    //add a cell with a "delete" button to delete the reservation
                    cell = new HtmlTableCell();
                    cell.InnerHtml = "<a class=\"btn btn-primary\" href=\"DeleteBooking.aspx?showId=" + showId + "\">Delete</a>";
                    bookingsList.Rows[i].Cells.Add(cell);
                    i++;
                }
                if (i == 0) //if there are no records, print this message
                    bookingsListWrapper.InnerHtml = "<i>No bookings found</i>";
            }

            //retrieve credit cards
            //set the query
            cmd.CommandText = @"SELECT CreditCardN, ExpirationDate, Owner FROM CreditCards WHERE UserId=@id";

            //read the result
            using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
            {
                int i = 1;
                while (rd.Read())
                {
                    //take the credit card number and the expiration date
                    string cardN = rd["CreditCardN"].ToString();
                    string expDate = rd["ExpirationDate"].ToString();
                    string owner = rd["Owner"].ToString();

                    //add a new row to the table
                    creditCards.Rows.Add(new HtmlTableRow());

                    //add a cell with a the credit card number
                    HtmlTableCell cell = new HtmlTableCell();
                    cell.InnerHtml = "****-****-****-" + cardN.Substring(12);
                    creditCards.Rows[i].Cells.Add(cell);

                    //add a cell with a the expiration date
                    cell = new HtmlTableCell();
                    cell.InnerHtml = expDate.Substring(0, 2) + "/" + expDate.Substring(2);
                    creditCards.Rows[i].Cells.Add(cell);

                    //add a cell with a the owner name
                    cell = new HtmlTableCell();
                    cell.InnerHtml = owner;
                    creditCards.Rows[i].Cells.Add(cell);

                    //add a cell with a "delete" button to delete the credit card
                    cell = new HtmlTableCell();
                    cell.InnerHtml = "<a class=\"btn btn-primary\" href=\"DeleteCard.aspx?cardN=" + cardN + "\">Delete</a>";
                    creditCards.Rows[i].Cells.Add(cell);
                    i++;
                }
                if (i == 1) //if there are no records, print this message
                    creditCardsNotFound.InnerText = "No saved credit cards found.";
            }

            //close the db connection
            conn.Close();
        }

        protected void AddCard(object sender, EventArgs e)
        {
            //hide error div
            newCardError.Visible = false;
            //check credit card
            CreditCardValidator c = new CreditCardValidator();
            string expDate = "";
            //if the field has been set
            if (exp.Value.Length > 6)
                //convert it into the form mmyy
                expDate = exp.Value.Substring(5) + exp.Value.Substring(2, 2);

            //check the credit card validity
            int check = c.ValidCard(cardn.Value, expDate);
            if (check != 0)
            {
                //if it's not valid, show an error
                newCardError.InnerHtml = "<p>Error " + check + "! Please check your Credit Card data.</p>";
                newCardError.Visible = true;
                return;
            }

            //regex for the CVV code (3 digits)
            Regex code_valid = new Regex(@"^\d{3}$", RegexOptions.IgnoreCase);
            if (!code_valid.Match(code.Value).Success)
            {
                newCardError.InnerHtml = "Please insert a valid 3-digits code.<br />";
                newCardError.Visible = true;
                return;
            }

            //if it has not been inserted an owner name, show an error
            if (owner.Value.Equals(""))
            {
                newCardError.InnerHtml = "Please insert an owner.<br />";
                newCardError.Visible = true;
                return;
            }

            //db connection
            SqlConnection conn = new SqlConnection(
                   ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
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
            //close connection
            conn.Close();
            //refresh the page
            Response.Redirect(Request.RawUrl);
        }

        protected void EditData(object sender, EventArgs e)
        {
            //hide the error
            formError1.Visible = false;

            //db connection
            SqlConnection conn = new SqlConnection(
                   ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            //command
            SqlCommand cmd = conn.CreateCommand();

            string err = "";

            //regex for name and surname
            Regex name_valid = new Regex(@"^[a-zA-Z\s]*$");

            //if the name doesn't match, update the error
            if (!name_valid.Match(name.Value).Success)
                err += "Insert a valid name.<br />";
            //if the surname doesn't match, update the error
            if (!name_valid.Match(surname.Value).Success)
                err += "Insert a valid surname.<br />";

            //if no errors
            if (err.Equals(""))
            {
                //set the query
                cmd.CommandText = @"UPDATE Registries SET Name=@name, Surname=@surname WHERE UserId=@id";

                //set the parameters
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = Convert.ToInt32(Session["userId"]);
                cmd.Parameters.Add("@name", SqlDbType.VarChar);
                cmd.Parameters["@name"].Value = name.Value;
                cmd.Parameters.Add("@surname", SqlDbType.VarChar);
                cmd.Parameters["@surname"].Value = surname.Value;

                //update session name and surname
                Session["name"] = name.Value;
                Session["surname"] = surname.Value;

                //execute the query
                cmd.ExecuteNonQuery();

                //close connection
                conn.Close();

                //refresh the page
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                //set the error and show the div
                formError1.InnerHtml = "<p>" + err + "</p>";
                formError1.Visible = true;
            }
            //close connection
            conn.Close();
        }

        protected void ResetPassword(object sender, EventArgs e)
        {
            //hide the error
            formError2.Visible = false;

            //db connection
            SqlConnection conn = new SqlConnection(
                   ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            //command
            SqlCommand cmd = conn.CreateCommand();

            string err = "";
            //if an old and a new passwords have been typed
            if(!pswOld.Equals("") && !password.Equals(""))
            {
                //try to see if the password is correct
                //set the query
                cmd.CommandText = @"SELECT UserId FROM Users WHERE UserId=@id
                                     AND Password=HASHBYTES('SHA2_256',@password);";
                //set the parameters
                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = Convert.ToInt32(Session["userId"]);
                cmd.Parameters.Add("@password", SqlDbType.VarBinary);
                cmd.Parameters["@password"].Value = Encoding.ASCII.GetBytes(pswOld.Value);
                //get the result
                var userId = cmd.ExecuteScalar();
                //if it's null
                if (userId == null) //the password was not correct
                    err += "The old password is incorrect. Try again.<br />";
                else
                {
                    //else if the password is valid
                    //regex
                    Regex password_valid = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", RegexOptions.None);
                    //match it
                    if (!password_valid.Match(password.Value).Success)
                        err += "Please insert a valid password. At least 8 characters, at least one number and one letter.<br />";
                    else if (!password.Value.Equals(password2.Value)) //match with the confirmation password
                        err += "The two password are not the same.<br />";
                    else
                    {
                        //if everything is fine, set the query and the parameters
                        cmd.CommandText = @"UPDATE Users SET Password=HASHBYTES('SHA2_256',@newPassword) WHERE UserId=@id;";
                        cmd.Parameters.Add("@newPassword", SqlDbType.VarBinary);
                        cmd.Parameters["@newPassword"].Value = Encoding.ASCII.GetBytes(password.Value);
                        //execute the query to update the password
                        cmd.ExecuteNonQuery();

                        //close connection
                        conn.Close();
                        
                        //refresh the page
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }

            //if there are some errors, show them
            if(!err.Equals(""))
            {
                formError2.InnerHtml = "<p>" + err + "</p>";
                formError2.Visible = true;
            }
            //close connection
            conn.Close();
        }
    }
}