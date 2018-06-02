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
            //set invisible the error div
            formError.Visible = false;
        }

        protected void UserLogin(object sender, EventArgs e)
        {
            //db connection
            SqlConnection conn = new SqlConnection(
              ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            //command
            SqlCommand cmd = conn.CreateCommand();

            String err = "";
            //regex for the email
            Regex email_valid = new Regex(@"\b[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}\b$", RegexOptions.IgnoreCase);
            //if it's not a valid email, update the error
            if (!email_valid.Match(email.Value).Success)
                err += "Please insert a valid email.<br />";

            //if there are no errors
            if (err.Equals(""))
            {
                //set the query
                cmd.CommandText = @"SELECT UserId FROM Users WHERE Email=@email
                                     AND Password=HASHBYTES('SHA2_256',@password);";
                //set the parameters
                cmd.Parameters.Add("@email", SqlDbType.VarChar);
                cmd.Parameters["@email"].Value = email.Value;
                cmd.Parameters.Add("@password", SqlDbType.VarBinary);
                cmd.Parameters["@password"].Value = Encoding.ASCII.GetBytes(password.Value);

                //execute the query
                var userId = cmd.ExecuteScalar();
                //if it's null
                if (userId == null) //the credentials were incorrect
                    err += "Incorrect email or password. Try again.<br />";
                else { //else
                    //select name and surname
                    cmd.CommandText = @"SELECT Name FROM Registries WHERE UserId=@ID";
                    cmd.Parameters.Add("@ID", SqlDbType.Int);
                    cmd.Parameters["@ID"].Value = Convert.ToInt32(userId);
                    
                    //Log in, setting up the session
                    Session["UserID"] = Convert.ToInt32(userId);
                    Session["name"] = cmd.ExecuteScalar().ToString();
                    Session["Email"] = email.Value;
                    cmd.CommandText = @"SELECT Surname FROM Registries WHERE UserId=@ID";
                    Session["surname"] = cmd.ExecuteScalar().ToString();

                    //check if it's an admin
                    cmd.CommandText = @"SELECT Admin FROM Users WHERE UserId=@ID";
                    if (Convert.ToBoolean(cmd.ExecuteScalar()))
                        Session["admin"] = true;

                    //redirect to the "redirect" value as get
                    if(Request.QueryString["redirect"] != null)
                        Response.Redirect(Request.QueryString["redirect"].ToString());
                    else //or to MyAccount if "redirect" is not set
                        Response.Redirect("MyAccount.aspx");
                }
            }
            //if we're still here, then there are some errors. Then show them
            formError.InnerHtml = "<p>"+err+"</p>";
            formError.Visible = true;
        }
    }
}