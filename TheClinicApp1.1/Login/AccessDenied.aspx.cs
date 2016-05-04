using System;
using System.Collections.Generic;
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
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            lblClinicName.Text = UA.Clinic;
            string Login = UA.userName;
            RoleName = UA.GetRoleName(Login);
            if (RoleName == Const.RoleAdministrator)
            {
                //this.hide.style.Add("display", "none");
                this.admin.Style.Add("Visibility", "Visible");
                this.master.Style.Add("Visibility", "Visible");
            }
            if (Request.QueryString["From"] != null) {
                From = Request.QueryString["From"].ToString();
                module.InnerText = From.ToUpper();
            }
        }
    }
}