using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace VIA_Cinema.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            formError.Visible = false;
        }

        protected void UserLogin(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(
              ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            String err = "";

            Regex email_valid = new Regex(@"\b[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}\b$", RegexOptions.IgnoreCase);


            if (!email_valid.Match(email.Value).Success)
                err += "Please insert a valid email.<br />";

            if (err.Equals(""))
            {
                cmd.CommandText = @"SELECT UserId FROM Users WHERE Email=@email
                                     AND Password=HASHBYTES('SHA2_256',@password);";

                cmd.Parameters.Add("@email", SqlDbType.VarChar);
                cmd.Parameters["@email"].Value = email.Value;
                cmd.Parameters.Add("@password", SqlDbType.VarBinary);
                cmd.Parameters["@password"].Value = Encoding.ASCII.GetBytes(password.Value);


                var userId = cmd.ExecuteScalar();

                if (userId == null)
                    err += "Incorrect email or password. Try again.<br />";
                else {
                    cmd.CommandText = @"SELECT Name FROM Registries WHERE UserId=@ID";
                    cmd.Parameters.Add("@ID", SqlDbType.Int);
                    cmd.Parameters["@ID"].Value = Convert.ToInt32(userId);
                    
                    //Log in
                    Session["UserID"] = Convert.ToInt32(userId);
                    Session["name"] = cmd.ExecuteScalar().ToString();
                    Session["Email"] = email.Value;
                    cmd.CommandText = @"SELECT Surname FROM Registries WHERE UserId=@ID";
                    Session["surname"] = cmd.ExecuteScalar().ToString();

                    Response.Redirect("MyAccount.aspx");
                }
            }
            
            formError.InnerHtml = "<p>"+err+"</p>";
            formError.Visible = true;
        }
    }
}