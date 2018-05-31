using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VIA_Cinema.Administration
{
    public partial class AddMovie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null || Session["admin"] == null)
                Response.Redirect("../index.aspx");

            //if isset GET parameter, fill the fields
        }

        protected void CreateMovie(object sender, EventArgs e)
        {
            //retrieve all values from fields

            //if isset GET parameter, update

            //else insert a new one
        }
    }
}