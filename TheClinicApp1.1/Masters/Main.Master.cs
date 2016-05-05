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

                

                if (currRole==null) { Response.Redirect(Const.AccessDeniedURL); }

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