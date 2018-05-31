using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;

namespace VIA_Cinema.Account
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //hide the error div
            formError.Visible = false;
        }

        protected void UserRegistration(object sender, EventArgs e)
        {
            //hide the error div
            formError.Visible = false;
            //db connection
            SqlConnection conn = new SqlConnection(
              ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            //command
            SqlCommand cmd = conn.CreateCommand();

            String query = "";

            String err = "";

            //regex for name and surname
            Regex name_valid = new Regex(@"^[a-zA-Z\s]*$");
            //match the name
            if (!name_valid.Match(name.Value).Success)
                err += "Insert a valid name.<br />";
            //match the surname
            if (!name_valid.Match(surname.Value).Success)
                err += "Insert a valid surname.<br />";

            //regex for email
            Regex email_valid = new Regex(@"\b[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}\b$", RegexOptions.IgnoreCase);
            //match the email
            if (!email_valid.Match(email.Value).Success)
                err += "Please insert a valid email.<br />";
            else
            {
                //if it's in the correct form, try to check if it already exists

                //set the query
                query = "SELECT Email FROM Users WHERE Email='" + email.Value + "'";
                cmd.CommandText = query;

                //read the result
                using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
                {
                    if (rd.Read()) //if there is at least one result
                        err += "Email already in use.<br />"; //show the error
                }
            }

            //password regex
            Regex password_valid = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", RegexOptions.None);
            //match the password with the regex
            if (!password_valid.Match(password.Value).Success)
                err += "Please insert a valid password. At least 8 characters, at least one number and one letter.<br />";
            else if (!password.Value.Equals(password2.Value)) //and with the confirmation password
                err += "Two password does not match.<br />";


            //if no errors
            if (err.Equals(""))
            {
                //set the query
                query = @"INSERT INTO Users (Email, Password)
                                VALUES (@email, HASHBYTES('SHA2_256',@password));";
                
                cmd.CommandText = query;
                //set the parameters
                cmd.Parameters.Add("@email", SqlDbType.VarChar);
                cmd.Parameters.Add("@password", SqlDbType.VarBinary);
                cmd.Parameters["@email"].Value = email.Value;
                cmd.Parameters["@password"].Value = Encoding.ASCII.GetBytes(password.Value);
                //execute the query to insert the new user
                cmd.ExecuteNonQuery();

                //set up the query to select the userId, we'll need it later
                cmd.CommandText = @"SELECT UserId FROM Users WHERE Email=@email
                                     AND Password=HASHBYTES('SHA2_256',@password);";
                //save the userId
                int userId = Convert.ToInt32(cmd.ExecuteScalar());

                //Log in
                Session["UserID"] = userId;
                Session["name"] = name.Value;
                Session["Email"] = email.Value;

                //New row in "registries", that is the table containing name and surname. (now we need UserId)
                cmd.CommandText = @"INSERT INTO Registries (UserId, Name, Surname) VALUES(@ID, @name, @surname);";
                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters["@ID"].Value = userId;
                cmd.Parameters.Add("@name", SqlDbType.VarChar);
                cmd.Parameters["@name"].Value = name.Value;
                cmd.Parameters.Add("@surname", SqlDbType.VarChar);
                cmd.Parameters["@surname"].Value = surname.Value;
                //execute the query
                cmd.ExecuteNonQuery();
                //redirect to index
                Response.Redirect("../index.aspx");
            }

            //set the error
            formError.InnerHtml = "<p>" + err + "</p>";
            formError.Visible = true;
        }
    }
}