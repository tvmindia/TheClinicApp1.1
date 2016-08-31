using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheClinicApp1._1.Masters
{

    public partial class popup : System.Web.UI.MasterPage
    {
        ClinicDAL.UserAuthendication UA;
        UIClasses.Const Const = new UIClasses.Const();

        protected void Page_Init(object sender, EventArgs e)
        {

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            if (UA == null)
            {
                Response.Redirect("../Login/Login.aspx");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> RoleName = new List<string>();
            DataTable dt = new DataTable();
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string Login = UA.userName;
            Label lblClinic = (Label)ContentPlaceHolder1.FindControl("lblClinicName");
            Label lblUser = (Label)ContentPlaceHolder1.FindControl("lblUserName");
            System.Web.UI.HtmlControls.HtmlGenericControl admin = (System.Web.UI.HtmlControls.HtmlGenericControl)ContentPlaceHolder1.FindControl("admin");
            System.Web.UI.HtmlControls.HtmlGenericControl master = (System.Web.UI.HtmlControls.HtmlGenericControl)ContentPlaceHolder1.FindControl("master");
            System.Web.UI.HtmlControls.HtmlGenericControl logout = (System.Web.UI.HtmlControls.HtmlGenericControl)ContentPlaceHolder1.FindControl("log");
            System.Web.UI.HtmlControls.HtmlImage BigLogo = (System.Web.UI.HtmlControls.HtmlImage)ContentPlaceHolder1.FindControl("biglogo");
            System.Web.UI.HtmlControls.HtmlImage SmallLogo = (System.Web.UI.HtmlControls.HtmlImage)ContentPlaceHolder1.FindControl("smalllogo");
            BigLogo.Src = "../Handler/ImageHandler.ashx?ClinicLogoID=" + UA.ClinicID;
            SmallLogo.Src = "../Handler/ImageHandler.ashx?ClinicLogosmallID=" + UA.ClinicID;
            if (logout != null)
            {
                logout.Visible = false;
            }
            if (lblClinic != null)
            {
                lblClinic.Text = "ClinicLite";
            }
            if(lblUser!=null)
            { 
            lblUser.Text = "👤 " + Login + " ";
            }
            RoleName = UA.GetRoleName1(Login);
            
            //*Check Roles Assigned and Giving Visibility For Admin Tab
            if (RoleName.Contains(Const.RoleAdministrator))
            {
                if (admin !=null)
                admin.Visible = true;
                if(master!=null)
                master.Visible = true;
            }
            //*Check Roles Assigned and Giving Visibility For SAdmin Tab
            if (RoleName.Contains(Const.RoleSadmin))
            {
                string currentPag = HttpContext.Current.Request.Url.AbsolutePath;
                if ((currentPag == Const.AssRolePage) || (currentPag == Const.AdminPageUrl))
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl sadmin = (System.Web.UI.HtmlControls.HtmlGenericControl)ContentPlaceHolder1.FindControl("liSAdmin");
                    sadmin.Visible = true;
                }

            }
        }
    }
}