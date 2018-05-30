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

        CheckBox[][] seatsCheck;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["showId"]==null)
                Response.Redirect("../index.aspx");

            int showId = Convert.ToInt32(Request.QueryString["showId"]), maxX=0, maxY=0;

            SqlConnection conn = new SqlConnection(
              ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT MAX(Se.LocationY) AS maxY, MAX(Se.LocationX) AS maxX
                                FROM Shows AS S, Seats AS Se
                                WHERE S.ShowId=@showId
                                    AND S.RoomId = Se.RoomId
                                GROUP BY Se.RoomId";
            cmd.Parameters.Add("@showId", SqlDbType.Int);
            cmd.Parameters["@showId"].Value = showId;

            using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
            {
                if (rd.Read())
                {
                    maxY = Convert.ToInt32(rd["maxY"]);
                    maxX = Convert.ToInt32(rd["maxX"]);
                }
            }


            seatsCheck = new CheckBox[maxY+1][];
            for (int i = 0; i <= maxY; i++)
            {
                seats.Rows.Add(new TableRow());
                seatsCheck[i] = new CheckBox[maxX+1];

                for (int j = 0; j <= maxX; j++)
                {
                    seats.Rows[i].Cells.Add(new TableCell());
                   /* Button b = new Button();
                    int y = i, x = j;
                    b.Click += (s, ev) => { CheckSeat(y, x); };*/
                    seatsCheck[i][j] = new CheckBox();
                    seats.Rows[i].Cells[j].Controls.Add(seatsCheck[i][j]);
                }
            }
        }
    }
}