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
        //global attributes
        Dictionary<string, CheckBox> seatsCheck = new Dictionary<string, CheckBox>();
        float price;
        string title, date;
        int room;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if no showId is passed, redirect to the main page
            if (Request.QueryString["showId"] == null)
                Response.Redirect("index.aspx");

            //hide error div
            formError.Visible = false;

            //take the showId
            int showId = Convert.ToInt32(Request.QueryString["showId"]), maxX = 0, maxY = 0;

            //db connection
            SqlConnection conn = new SqlConnection(
              ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();
            //command
            SqlCommand cmd = conn.CreateCommand();
            //set the query to see how big is the room, the price of the show, the title, the room and the date
            cmd.CommandText = @"SELECT  MAX(Se.LocationY) AS maxY,
                                        MAX(Se.LocationX) AS maxX,
                                        S.Price AS Price,
                                        M.Title AS Title,
                                        S.RoomId AS Room,
                                        S.Date AS Date
                                FROM Shows AS S, Seats AS Se, Movies AS M
                                WHERE S.ShowId=@showId
                                    AND S.RoomId = Se.RoomId
                                    AND S.MovieId = M.MovieId
                                GROUP BY Se.RoomId, S.Price, M.Title, S.RoomId, S.Date";
            //set the parameters
            cmd.Parameters.Add("@showId", SqlDbType.Int);
            cmd.Parameters["@showId"].Value = showId;
            //read the result
            using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
            {
                if (rd.Read())
                {
                    //take the values
                    maxY = Convert.ToInt32(rd["maxY"]);
                    maxX = Convert.ToInt32(rd["maxX"]);
                    price = float.Parse(rd["Price"].ToString());
                    title = rd["Title"].ToString();
                    room = Convert.ToInt32(rd["Room"].ToString());
                    date = rd["Date"].ToString().Substring(0, 16);
                }
            }

            //set the info Label
            info.Text = "<h1>" + title + "</h1>";
            info.Text += "<p><b>Room:</b> " + room + "<br />";
            info.Text += "<b>Date and Time:</b> " + date + "</p>";

            //init the table to the right dimensions
            for (int i = 0; i <= maxY; i++)
            {
                //adding rows
                seats.Rows.Add(new TableRow());

                for (int j = 0; j <= maxX; j++)
                    seats.Rows[i].Cells.Add(new TableCell()); //and adding cells to each row
            }

            //set the query to select all the already taken seats
            cmd.CommandText = @"SELECT  Se.LocationY AS i,
                                        Se.LocationX AS j
                                FROM Shows AS S, Seats AS Se, Reservations AS R
                                WHERE S.ShowId = @showId
                                    AND R.ShowId = S.ShowId
                                    AND S.RoomId = Se.RoomId
                                    AND Se.SeatN = R.SeatN";

            //red the result
            using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
            {
                while (rd.Read())
                {
                    //set the respective cell in the table as "taken"
                    int i = Convert.ToInt32(rd["i"]), j = Convert.ToInt32(rd["j"]);
                    seats.Rows[i].Cells[j].CssClass = "TakenSeat";
                }
            }

            //set the query to select all the seats of the room
            cmd.CommandText = @"SELECT  Se.LocationY AS i,
                                        Se.LocationX AS j,
                                        Se.SeatN AS id
                                FROM Shows AS S, Seats AS Se
                                WHERE S.ShowId=@showId
                                    AND S.RoomId = Se.RoomId";

            //read the results
            using (var rd = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
            {
                while (rd.Read())
                {
                    //take the coordinates and the id
                    int i = Convert.ToInt32(rd["i"]), j = Convert.ToInt32(rd["j"]);
                    string id = rd["id"].ToString();

                    //if the cell is already taken, skip to the next seat
                    if (seats.Rows[i].Cells[j].CssClass.Equals("TakenSeat"))
                        continue;

                    //else, put a checkbox inside it, saving the id
                    CheckBox cb = new CheckBox() { ID = id };
                    seats.Rows[i].Cells[j].Controls.Add(cb);
                    seats.Rows[i].Cells[j].CssClass = "AvailableSeat";
                    seatsCheck.Add(id, cb);
                }
            }

            //if there are still in session the seats relative to this show
            if (Session["seats"] != null && Session["showId"]!=null
                && Convert.ToInt32(Session["showId"])==showId)
            {
                //check them as selected
                foreach (string id in (List<string>)Session["seats"])
                    seatsCheck[id].Checked = true;
            }
        }

        //when confirmed
        protected void ConfirmSeats(object sender, EventArgs e)
        {
            //retrieve all the checked seats
            List<string> chosenSeats = new List<string>();
            foreach (var s in seatsCheck.Values)
            {
                if (s.Checked) //if checked
                    chosenSeats.Add(s.ID); //save the ID
            }

            //if no seat was checked
            if (chosenSeats.Count == 0)
            {
                //show an error
                formError.InnerHtml = "<p>Please choose at least one seat to continue.</p>";
                formError.Visible = true;
                return;
            }

            //save in the session the wanted seats and the show info
            Session["showId"] = Request.QueryString["showId"];
            Session["seats"] = chosenSeats;
            Session["price"] = price;
            Session["title"] = title;
            Session["room"] = room;
            Session["date"] = date;
            //then redirect to the payment page
            Response.Redirect("Payment.aspx");
        }

    }
}