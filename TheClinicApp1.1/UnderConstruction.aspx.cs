using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheClinicApp1._1.Login
{
    public partial class UnderConstruction : System.Web.UI.Page
    {
        UIClasses.Const Const = new UIClasses.Const();
        protected void Page_Load(object sender, EventArgs e)
        {
           // GetUser_Info();
        }


        protected void GetUser_Info()
        {
            string IPAddress_Client = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                IPAddress_Client = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                IPAddress_Client = HttpContext.Current.Request.UserHostAddress;
            }

            string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
            String MachineName = System.Environment.MachineName;

            //Label1.Text = computer_name[0];
            //Label2.Text = MachineName;


        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            Response.Redirect(Const.LoginPageURL);
        }

        


    }
}