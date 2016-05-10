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
            if (logout != null)
            {
                logout.Visible = false;
            }
            lblClinic.Text = UA.Clinic;
            lblUser.Text = "👤 " + Login + " ";
            RoleName = UA.GetRoleName1(Login);
            
            //*Check Roles Assigned and Giving Visibility For Admin Tab
            if (RoleName.Contains(Const.RoleAdministrator))
            {
                admin.Visible = true;
                master.Visible = true;
            }
        }
    }
}