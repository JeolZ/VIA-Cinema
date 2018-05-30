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

        }

        protected void UserRegistration(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(
              ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            String query = "";

            String err = "";
            Regex name_valid = new Regex(@"^[a-zA-Z\s]*$");

            if (!name_valid.Match(name.Text).Success)
                err += "Insert a valid name.<br />";

            if (!name_valid.Match(surname.Text).Success)
                err += "Insert a valid surname.<br />";

            Regex email_valid = new Regex(@"\b[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}\b$", RegexOptions.IgnoreCase);
            

            if (!email_valid.Match(email.Text).Success)
                err += "Please insert a valid email.<br />";
            else
            {
                query = "SELECT Email FROM Users WHERE Email='" + email.Text + "'";
                cmd.CommandText = query;

                using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
                {
                    if (rd.Read())
                        err += "Email already in use.<br />";
                }
            }
            Regex password_valid = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", RegexOptions.None);
            
            if (!password_valid.Match(password.Text).Success)
                err += "Please insert a valid password. At least 8 characters, at least one number and one letter.<br />";
            else if (!password.Text.Equals(password2.Text))
                err += "Two password does not match.<br />";

            formError.Text = err;

            if (err.Equals(""))
            {
                query = @"INSERT INTO Users (Email, Password)
                                VALUES (@email, HASHBYTES('SHA2_256',@password));";
                
                cmd.CommandText = query;
                
                cmd.Parameters.Add("@email", SqlDbType.VarChar);
                cmd.Parameters.Add("@password", SqlDbType.VarBinary);
                cmd.Parameters["@email"].Value = email.Text;
                cmd.Parameters["@password"].Value = Encoding.ASCII.GetBytes(password.Text);

                cmd.ExecuteNonQuery();

                cmd.CommandText = @"SELECT UserId FROM Users WHERE Email=@email
                                     AND Password=HASHBYTES('SHA2_256',@password);";

                int userId = Convert.ToInt32(cmd.ExecuteScalar());

                //Log in
                Session["UserID"] = userId;
                Session["name"] = name.Text;
                Session["Email"] = email.Text;

                //New row in registries
                cmd.CommandText = @"INSERT INTO Registries (UserId, Name, Surname) VALUES(@ID, @name, @surname);";
                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters["@ID"].Value = userId;
                cmd.Parameters.Add("@name", SqlDbType.VarChar);
                cmd.Parameters["@name"].Value = name.Text;
                cmd.Parameters.Add("@surname", SqlDbType.VarChar);
                cmd.Parameters["@surname"].Value = surname.Text;

                cmd.ExecuteNonQuery();

                Response.Redirect("../index.aspx");
            }
        }
    }
}