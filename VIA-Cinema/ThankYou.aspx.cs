using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VIA_Cinema
{
    public partial class ThankYou : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
                id.Text = Request.QueryString["id"];
            else
                //redirect to the index after 2 seconds
                Response.AddHeader("REFRESH", "2;URL=index.aspx");
        }
    }
}