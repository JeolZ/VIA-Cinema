using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VIA_Cinema
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //destroy the session
            Session["UserID"] = null;
            Session["name"] = null;
            Session["Email"] = null;
            //redirect to the main page
            Response.Redirect("../index.aspx");
        }
    }
}