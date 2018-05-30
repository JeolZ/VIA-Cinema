using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;

namespace VIA_Cinema
{
    public partial class ChooseSeats : System.Web.UI.Page
    {

        List<CheckBox> seatsCheck = new List<CheckBox>();
        float price;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["showId"] == null)
                Response.Redirect("index.aspx");

            int showId = Convert.ToInt32(Request.QueryString["showId"]), maxX = 0, maxY = 0;

            SqlConnection conn = new SqlConnection(
              ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT  MAX(Se.LocationY) AS maxY,
                                        MAX(Se.LocationX) AS maxX,
                                        S.Price AS Price
                                FROM Shows AS S, Seats AS Se
                                WHERE S.ShowId=@showId
                                    AND S.RoomId = Se.RoomId
                                GROUP BY Se.RoomId, S.Price";
            cmd.Parameters.Add("@showId", SqlDbType.Int);
            cmd.Parameters["@showId"].Value = showId;

            using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
            {
                if (rd.Read())
                {
                    maxY = Convert.ToInt32(rd["maxY"]);
                    maxX = Convert.ToInt32(rd["maxX"]);
                    price = float.Parse(rd["Price"].ToString());
                }
            }

            for (int i = 0; i <= maxY; i++)
            {
                seats.Rows.Add(new TableRow());

                for (int j = 0; j <= maxX; j++)
                    seats.Rows[i].Cells.Add(new TableCell());
            }

            cmd.CommandText = @"SELECT  Se.LocationY AS i,
                                        Se.LocationX AS j
                                FROM Shows AS S, Seats AS Se, Reservations AS R
                                WHERE S.ShowId = @showId
                                    AND R.ShowId = S.ShowId
                                    AND S.RoomId = Se.RoomId
                                    AND Se.SeatN = R.SeatN";

            using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
            {
                while (rd.Read())
                {
                    int i = Convert.ToInt32(rd["i"]), j = Convert.ToInt32(rd["j"]);
                    seats.Rows[i].Cells[j].CssClass = "TakenSeat";
                }
            }

            cmd.CommandText = @"SELECT  Se.LocationY AS i,
                                        Se.LocationX AS j,
                                        Se.SeatN AS id
                                FROM Shows AS S, Seats AS Se
                                WHERE S.ShowId=@showId
                                    AND S.RoomId = Se.RoomId";

            using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
            {
                while (rd.Read())
                {
                    int i = Convert.ToInt32(rd["i"]), j = Convert.ToInt32(rd["j"]);
                    string id = rd["id"].ToString();

                    if (seats.Rows[i].Cells[j].CssClass.Equals("TakenSeat"))
                        continue;

                    CheckBox cb = new CheckBox() { ID = id };

                    seats.Rows[i].Cells[j].Controls.Add(cb);
                    seats.Rows[i].Cells[j].CssClass = "AvailableSeat";
                    seatsCheck.Add(cb);
                }
            }
        }


        /*protected void Page_Load(object sender, EventArgs e)
        {
        }*/


        protected void ConfirmSeats(object sender, EventArgs e)
        {
            List<string> chosenSeats = new List<string>();
            foreach (var s in seatsCheck)
            {
                if (s.Checked)
                    chosenSeats.Add(s.ID);
            }

            if (chosenSeats.Count == 0)
            {
                formError.Text = "Please choose at least one seat to continue.<br />";
                return;
            }

            Session["showId"] = Request.QueryString["showId"];
            Session["seats"] = chosenSeats;
            Session["price"] = price;
            Response.Redirect("Payment.aspx");
        }

    }
}