using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheClinicApp1._1.Masters
{
    public partial class Main : System.Web.UI.MasterPage
    {

        ClinicDAL.UserAuthendication UA;
        UIClasses.Const Const = new UIClasses.Const();

        protected void Page_Init(object sender, EventArgs e)
        {

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            if (UA == null)
            {
                Response.Redirect(Const.LoginPageURL);
            }

            AccessCheck();
           
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
            lblClinic.Text = UA.Clinic;           
            lblUser.Text = "👤 " + Login + " ";
            dt = UA.GetRoleName1(Login);
            foreach (DataRow dr in dt.Rows)
            {
                RoleName.Add(dr["RoleName"].ToString());
            }
            //*Check Roles Assigned and Giving Visibility For Admin Tab
            if (RoleName.Contains(Const.RoleAdministrator))
            {
                admin.Visible = true;
                master.Visible = true;               
            }
        }



        public void AccessCheck() {

            try
            {
               
                List<string> currRole = new List<string>();
                DataTable dt = null;
                dt=UA.GetRoleName1(UA.userName);
                foreach (DataRow dr in dt.Rows)
                {

                    currRole.Add(dr["RoleName"].ToString());
                
                }
                 
                string currPage = Const.GetCurrentPageName(Request);
                string From = "?From=";
                string redirectURL = "";

                

                if (currRole.Count==0) { Response.Redirect(Const.AccessDeniedURL); }

                if (currPage != Const.AccessDenied)
                {
                    if (currPage == Const.PatientPage) { }
                    if (currPage == Const.TokenPage) { }
                    if (currPage == Const.DoctorPage) { 
                        if (!currRole.Contains(Const.RoleDoctor)) {
                            From = From + Const.Doctor;
                            redirectURL = Const.AccessDeniedURL + From;
                        } 
                    }
                    if (currPage == Const.PharmacyPage) { }
                    if (currPage == Const.StockPage) { }
                    if (currPage == Const.AdminPage) {
                        if (!(currRole.Contains( Const.RoleDoctor) | currRole.Contains(Const.RoleAdministrator)))
                        {
                            From = From + Const.Admin;
                            redirectURL = Const.AccessDeniedURL + From;
                        } 
                    }
                    if (currPage == Const.MasterPage)
                    {
                        if (!(currRole.Contains(Const.RoleAdministrator)))
                        {
                            From = From + Const.Admin;
                            redirectURL = Const.AccessDeniedURL + From;
                        }
                    }



                    if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                   


                }

            }
            catch (Exception)
            {

             //   Response.Redirect(Const.AccessDeniedURL);
            }
        }
    }
}