using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services.Protocols;

namespace VIA_Cinema
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Web service
            localhost.VIACinemaService via = new localhost.VIACinemaService();

            localhost.Movie[] m = via.GetMovies();
            
            Label1.Text = m[0].ToString();
            Label2.Text = m[1].ToString();
            Label3.Text = m[2].ToString();
        }
    }
}