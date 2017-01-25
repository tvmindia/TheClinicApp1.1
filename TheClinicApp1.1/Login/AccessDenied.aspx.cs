using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheClinicApp1._1.Login
{
    
    public partial class AccessDenied : System.Web.UI.Page
    {
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        public string RoleName = null;
        public string From = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> RoleName = new List<string>();
            
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
           
            string Login = UA.userName;

            RoleName = UA.GetRoleName1(Login);
            
            
            if (Request.QueryString["From"] != null) {
                From = Request.QueryString["From"].ToString();
                //module.InnerText = From.ToUpper();
            }
        }

        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }
    }
}