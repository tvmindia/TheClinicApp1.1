using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheClinicApp1._1
{
    public partial class Default : System.Web.UI.Page
    {
        ClinicDAL.UserAuthendication UA;
        UIClasses.Const Const = new UIClasses.Const();
        protected void Page_Load(object sender, EventArgs e)
        {
            string log=Request.QueryString["sessionclear"];
            if(log== Const.LogoutSession)
            {
                Session.Remove(Const.LoginSession);
                Session.Clear();             
            }
            Response.Redirect("~/Login/Login.aspx");



        }
    }
}