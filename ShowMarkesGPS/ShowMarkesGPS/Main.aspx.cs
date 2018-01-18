using Subgurim.Controles;
using Subgurim.Controles.GoogleChartIconMaker;
using System;
using System.Configuration;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace ShowMarkesGPS
{
    public partial class Main : System.Web.UI.Page
    {
        MySqlDataReader datareader;
        MySqlCommand cmd;
        String str;
        List<GetCoordonates> coordList;
        MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        private void IsUserLogedIn()
        {
            if (Session["IsUserLogin"] == null || !Convert.ToBoolean(Session["IsUserLogin"]))
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            IsUserLogedIn();
            foreach (var i in getDeviceID())
            {
                DropDownList1.Items.Add(i);
            }
            foreach (var j in Datee())
            {
                DropDownList2.Items.Add(j);
                DropDownList3.Items.Add(j);

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            coordList = createList();
            updateMap();
        }
        private void updateMap()
        {
            if (coordList.Count > 0)
            {
                GLatLng location = new GLatLng(46.749063, 23.558166);
                GMap1.setCenter(location, 13);
                XPinLetter xpinLetter = new XPinLetter(PinShapes.pin_star, "H", Color.Red, Color.White, Color.Chocolate);
                GMap1.Add(new GMarker(location, new GMarkerOptions(new GIcon(xpinLetter.ToString(), xpinLetter.Shadow()))));

                PinIcon p;
                GMarker gm;
                GInfoWindow win;

                foreach (var i in coordList)
                {
                    p = new PinIcon(PinIcons.home, Color.Cyan);
                    gm = new GMarker(new GLatLng(Convert.ToDouble(i.getLatitude()), (Convert.ToDouble(i.getLongitude()))), new GMarkerOptions(new GIcon(p.ToString(), p.Shadow())));
                    win = new GInfoWindow(gm,"Device Location");
                    GMap1.Add(win);
                }
            }
            else
            {
                String myStringVariable = "No registration for this period";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
            }

        }
        private List<GetCoordonates> createList()
        {
            List<GetCoordonates> coordonates = new List<GetCoordonates>();

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            conn.Open();

            str = "";
            str = "SELECT * FROM gpstrack.track where Date>='" + DropDownList2.SelectedItem + "' && Date<='" + DropDownList3.SelectedItem + "' && DeviceID='" + DropDownList1.SelectedItem + "';";
            cmd = new MySqlCommand(str, conn);
            datareader = cmd.ExecuteReader();

            while (datareader.HasRows && datareader.Read())
            {
                coordonates.Add(new GetCoordonates(double.Parse(datareader.GetString(datareader.GetOrdinal("latitude"))), double.Parse(datareader.GetString(datareader.GetOrdinal("longitude")))));
            }

            datareader.Close();
            conn.Close();
            return coordonates;
        }

        private List<String> getDeviceID()
        {
            List<String> deviceIdList = new List<String>();

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            conn.Open();

            str = "";
            str = "SELECT DISTINCT DeviceID FROM gpstrack.track;";

            cmd = new MySqlCommand(str, conn);
            datareader = cmd.ExecuteReader();

            while (datareader.HasRows && datareader.Read())
            {
                deviceIdList.Add(datareader.GetString("DeviceID"));
            }

            datareader.Close();
            conn.Close();

            return deviceIdList;
        }
        private List<String> Datee()
        {
            List<String> datee = new List<String>();

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            conn.Open();

            str = "";
            str = "SELECT DISTINCT Date FROM gpstrack.track;";

            cmd = new MySqlCommand(str, conn);
            datareader = cmd.ExecuteReader();

            while (datareader.HasRows && datareader.Read())
            {
                datee.Add(datareader.GetString("Date"));
            }

            datareader.Close();
            conn.Close();

            return datee;
        }
    }
}