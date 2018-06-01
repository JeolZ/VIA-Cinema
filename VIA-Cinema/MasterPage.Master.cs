using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VIA_Cinema
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        int count = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            //to change the last item to "Log in"
            Menu1.MenuItemDataBound += CheckLogIn;
        }

        protected void CheckLogIn(object sender, EventArgs e)
        {
            //if the 5th item has been added and the user is not logged in...
            if (count >= 4 && Session["userId"] == null)
            {
                //...then show "Log In" instead of "My Account"
                Menu1.Items[0].ChildItems[3].Text = "Log in";
                Menu1.Items[0].ChildItems[3].NavigateUrl = "~/Account/LogIn.aspx";
            }
            else
                ++count;
        }
    }
}