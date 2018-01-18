using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ShowMarkesGPS
{

    public partial class Login : System.Web.UI.Page
    {
        MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                // Set IsUserLogin Session as False on Page laod
                Session["IsUserLogin"] = false;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string checkuser = "select count(*) from login where username='" + TextBoxUserName.Text + "'";
            MySqlCommand com = new MySqlCommand(checkuser, conn);
            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            conn.Close();
            if (temp == 1)
            {
                conn.Open();
                string checkPasswordQuery = "select password from login where username='" + TextBoxUserName.Text + "'";
                MySqlCommand passCom = new MySqlCommand(checkPasswordQuery, conn);
                string password = passCom.ExecuteScalar().ToString().Replace(" ",""); 
                if (password == TextBoxPassword.Text)
                {
                    Session["IsUserLogin"] = true;
                    Session["New"] = TextBoxUserName.Text;
                    Response.Write("Password is correct");
                    Response.Redirect("Main.aspx");
                }
                else
                {
                    Response.Write("Password is not correct");
                }
            }
            else
            {
                Response.Write("Username is not correct");
            }

        }
    }
}