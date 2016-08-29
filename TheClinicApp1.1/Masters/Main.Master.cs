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
            if(lblClinic!=null)
            {
                lblClinic.Text = UA.Clinic;
            }
                       
            lblUser.Text = "👤 " + Login + " ";
            RoleName= UA.GetRoleName1(Login);           
            //*Check Roles Assigned and Giving Visibility For SAdmin Tab
            if (RoleName.Contains(Const.RoleSadmin))
            {
                string currentPag=HttpContext.Current.Request.Url.AbsolutePath;
                if ((currentPag == Const.AssRolePage) || (currentPag == Const.AdminPageUrl))
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl sadmin = (System.Web.UI.HtmlControls.HtmlGenericControl)ContentPlaceHolder1.FindControl("liSAdmin");
                    sadmin.Visible = true;
                    System.Web.UI.HtmlControls.HtmlGenericControl ClinicDiv = (System.Web.UI.HtmlControls.HtmlGenericControl)ContentPlaceHolder1.FindControl("dropdivclinic");
                    ClinicDiv.Visible = true;
                }
                              
            }
        }



        public void AccessCheck() {

            try
            {
               
                List<string> currRole = new List<string>();               
                currRole = UA.GetRoleName1(UA.userName);                               
                string currPage = Const.GetCurrentPageName(Request);
                string From = "?From=";
                string redirectURL = "";
                if(!currRole.Contains(Const.RoleSadmin))
                {
                    if (currRole.Count == 0) { Response.Redirect(Const.AccessDeniedURL); }

                    if (currPage != Const.AccessDenied)
                    {
                        if (currPage == Const.PatientPage) { }
                        if (currPage == Const.TokenPage) { }
                        if (currPage == Const.DoctorPage)
                        {
                            if (!currRole.Contains(Const.RoleDoctor))
                            {
                                From = From + Const.Doctor;
                                redirectURL = Const.AccessDeniedURL + From;
                            }
                        }
                        if (currPage == Const.PharmacyPage) { }
                        if (currPage == Const.StockPage) { }

                        if (currPage == Const.AdminPage)
                        {
                            if (!currRole.Contains(Const.RoleAdministrator))
                            {
                                From = From + Const.Admin;
                                redirectURL = Const.AccessDeniedURL + From;
                            }
                        }

                        //-----------Checking access of report tab ,user with only pharmacist role is not allowed to get report ------------
                        if (currPage == Const.ReportPageURL)
                        {
                            if (currRole.Count == 1 && currRole.Contains(Const.RolePharmacist))
                            {
                                From = From + Const.RolePharmacist;
                                redirectURL = Const.AccessDeniedURL + From;
                            }



                            //if (currRole.Contains(Const.Patient) || currRole.Contains(Const.Token) || currRole.Contains(Const.Token) || currRole.Contains(Const.Doctor) || currRole.Contains(Const.Stock) || currRole.Contains(Const.Admin) || currRole.Contains(Const.RoleAdministrator)) 
                            //{

                            //}
                            //else
                            //{
                            //    From = From + Const.RolePharmacist;
                            //    redirectURL = Const.AccessDeniedURL + From;
                            //}
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
                



                    

            }
            catch (Exception)
            {

             //   Response.Redirect(Const.AccessDeniedURL);
            }
        }
    }
}