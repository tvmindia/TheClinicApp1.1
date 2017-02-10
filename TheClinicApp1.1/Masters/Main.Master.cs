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
        //string dbError = "";
        ClinicDAL.UserAuthendication UA;
        UIClasses.Const Const = new UIClasses.Const();
       public string masterRole
        {
            get;
            set;
        }
        protected void Page_Init(object sender, EventArgs e)
        {

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            //if(Session["dbError"]!=null)
            //{
            //  dbError = Session["dbError"].ToString();
            //}
            string currentPage = HttpContext.Current.Request.Url.AbsolutePath;
            if (currentPage == "/underConstruction.aspx" || currentPage == "/underConstruction.aspx?cause=dbDown")
            {
              //  Response.Redirect("~/underConstruction.aspx");
            }
            else
            {
                if (UA == null)
                {
                    Response.Redirect(Const.LoginPageURL);

                }
                else if(UA!=null)
                {
                    if(hdfPage.Value != currentPage)
                    {
                        AccessCheck();
                    }
                    
                }
                else
                {

                }
            }
            
           
           
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (UA != null)
            {
                List<string> RoleName = new List<string>();
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
                    lblClinic.Text = "";
                }

                lblUser.Text = "👤 " + Login + " ";
                RoleName = UA.GetRoleName1(Login);

                //*Check Roles Assigned and Giving Visibility For SAdmin Tab
                if (RoleName.Contains(Const.RoleSadmin))
                {
                    string currentPag = HttpContext.Current.Request.Url.AbsolutePath;
                    if ((currentPag == Const.AssRolePage) || (currentPag == Const.AdminPageUrl))
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl sadmin = (System.Web.UI.HtmlControls.HtmlGenericControl)ContentPlaceHolder1.FindControl("liSAdmin");
                        sadmin.Visible = true;
                        //System.Web.UI.HtmlControls.HtmlGenericControl ClinicDiv = (System.Web.UI.HtmlControls.HtmlGenericControl)ContentPlaceHolder1.FindControl("dropdivclinic");
                        //ClinicDiv.Visible = true;
                    }

                }
            }
        }



        public void AccessCheck() {

            try
            {

                List<string> currRole = new List<string>();
                currRole = UA.GetRoleName1(UA.userName);
                string currPage = Const.GetCurrentPageName(Request);
                if(currPage!=string.Empty)
                {
                    UA.currentPage = currPage.Split('.')[0];
                }
                
                string From = "?From=";
                string redirectURL = "";
                
               if((currRole.Contains(Const.RoleDoctor))&&(currRole.Contains(Const.Receptionist)))
                {
                    masterRole = "1";
                    UA.userInRole = masterRole;
                    //hdfUserRole.Value = masterRole;
                }


                if (!currRole.Contains(Const.RoleSadmin))
                {
                    if (currRole.Count == 0) { Response.Redirect(Const.AccessDeniedURL); }

                    if (currPage != Const.AccessDenied)
                    {
                        if (redirectURL == "")
                        {
                            if(UA.isFirstLoad == null)
                            {
                                redirectURL = Const.DefaultPage(currRole);

                                if (!redirectURL.Contains("/" + currPage))
                                {
                                    UA.isFirstLoad = "True";
                                    Response.Redirect(redirectURL, true);
                                }
                            }
                         


                        }
                        if (currPage == Const.PatientPage) {
                            if(!currRole.Contains(Const.Receptionist))
                            {
                                From = From + currRole[0];
                                redirectURL = Const.AccessDeniedURL + From;
                            }
                        }
                        if (currPage == Const.TokenPage) {
                           if(!currRole.Contains(Const.Receptionist))
                            {
                                From = From + currRole[0];
                                redirectURL = Const.AccessDeniedURL + From;
                            }
                        }
                        if (currPage == Const.DoctorPage)
                        {
                            if (!currRole.Contains(Const.RoleDoctor))
                            {
                                From = From + Const.Receptionist;
                                redirectURL = Const.AccessDeniedURL + From;
                            }
                        }
                        if (currPage == Const.PharmacyPage) {
                            if(!currRole.Contains(Const.Pharmacy))
                            {
                                From = From + currRole[0];
                                redirectURL = Const.AccessDeniedURL + From;
                            }
                        }
                        if (currPage == Const.StockPage) {
                            if(!currRole.Contains(Const.StockRole))
                            {
                                From = From + currRole[0];
                                redirectURL = Const.AccessDeniedURL + From;
                            }
                        }
                        if(currPage==Const.AppointmentPage)
                        {
                            if((!currRole.Contains(Const.Doctor))&&(!currRole.Contains(Const.Receptionist)))
                                {
                                From = From + currRole[0];
                                redirectURL = Const.AccessDeniedURL + From;
                            }
                            if ((currRole.Contains(Const.Doctor)) && !(currRole.Contains(Const.Receptionist)))
                            {
                                UA.userInRole = Const.Doctor;
                            }
                            if (!(currRole.Contains(Const.Doctor)) && (currRole.Contains(Const.Receptionist)))
                            {
                                UA.userInRole = Const.Receptionist;
                            }
                        }

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
                            if (!currRole.Contains(Const.Report))
                            {
                                From = From + Const.RolePharmacist;
                                redirectURL = Const.AccessDeniedURL + From;
                            }
                        }


                        if (currPage == Const.MasterPage)
                        {
                            if ( !(currRole.Contains(Const.StockRole)))
                            {
                                From = From + Const.Admin;
                                redirectURL = Const.AccessDeniedURL + From;
                            }
                        }
                        if (redirectURL != "") { Response.Redirect(redirectURL, true); }



                    }
                }

                if (currRole.Count == 1) {
                   

                    if (currRole[0] == Const.Pharmacy)
                    {
                        
                        if (currPage == Const.PatientPage)
                        {
                            From = From + Const.Pharmacy;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.StockPage)
                        {

                            From = From + Const.Pharmacy;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.TokenPage)
                        {
                            From = From + Const.Pharmacy;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.DoctorPage)
                        {
                            From = From + Const.Pharmacy;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.ReportPageURL)
                        {
                            From = From + Const.Pharmacy;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.MasterPage)
                        {
                            From = From + Const.Pharmacy;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.AppointmentPage)
                        {
                            From = From + Const.Pharmacy;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if(currPage==Const.AdminPage)
                        {
                            From = From + Const.Pharmacy;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                    }
                        if (currRole[0] == Const.Receptionist)
                    {
                        masterRole = currRole[0].ToString();
                        UA.userInRole = masterRole;
                        // hdfUserRole.Value = masterRole;
                        if (currPage == Const.PharmacyPage)
                        {

                            From = From + Const.Receptionist;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.StockPage)
                        {

                            From = From + Const.Receptionist;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
              
                        //if (currPage == Const.TokenPage)
                        //{
                        //    From = From + Const.Receptionist;
                        //    redirectURL = Const.AccessDeniedURL + From;
                        //    if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        //}
                        if (currPage == Const.DoctorPage)
                        {
                            From = From + Const.Receptionist;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.ReportPageURL)
                        {
                            From = From + Const.Receptionist;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.MasterPage)
                        {
                            From = From + Const.Receptionist;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if(currPage==Const.AdminPage)
                        {
                            From = From + Const.Receptionist;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                    }
                        if (currRole[0] == Const.RoleAdministrator)
                {
                    if (currPage == Const.PatientPage)
                    {
                        From = From + Const.RoleAdministrator;
                        redirectURL = Const.AccessDeniedURL + From;
                        if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                    }
                    if (currPage == Const.TokenPage)
                    {
                        From = From + Const.RoleAdministrator;
                        redirectURL = Const.AccessDeniedURL + From;
                        if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                    }
                    if (currPage == Const.DoctorPage)
                    {
                        From = From + Const.RoleAdministrator;
                        redirectURL = Const.AccessDeniedURL + From;
                        if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                    }
                    if (currPage == Const.PharmacyPage)
                    {
                        From = From + Const.RoleAdministrator;
                        redirectURL = Const.AccessDeniedURL + From;
                        if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                    }
                    if (currPage == Const.StockPage)
                    {
                        From = From + Const.RoleAdministrator;
                        redirectURL = Const.AccessDeniedURL + From;
                        if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                    }
                    if (currPage == Const.ReportPageURL)
                    {
                        From = From + Const.RoleAdministrator;
                        redirectURL = Const.AccessDeniedURL + From;
                        if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                    }
                    if (currPage == Const.MasterPage)
                    {
                        From = From + Const.RoleAdministrator;
                        redirectURL = Const.AccessDeniedURL + From;
                        if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                    }
                    if (currPage == Const.AppointmentPage)
                    {
                        From = From + Const.RoleAdministrator;
                        redirectURL = Const.AccessDeniedURL + From;
                        if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                    }
                }
                    if (currRole[0] == Const.StockRole)
                    {
                        if (currPage == Const.PatientPage)
                        {
                            From = From + Const.Stock;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.TokenPage)
                        {
                            From = From + Const.Stock;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.DoctorPage)
                        {
                            From = From + Const.Stock;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.PharmacyPage)
                        {
                            From = From + Const.Stock;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.ReportPageURL)
                        {
                            From = From + Const.Stock;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.AppointmentPage)
                        {
                            From = From + Const.Stock;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if(currPage==Const.AdminPage)
                        {
                            From = From + Const.Stock;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                    }
                    if (currRole[0] == Const.Report)
                    {
                        if (currPage == Const.PatientPage)
                        {
                            From = From + Const.Report;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.TokenPage)
                        {
                            From = From + Const.Report;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.DoctorPage)
                        {
                            From = From + Const.Report;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.StockPage)
                        {

                            From = From + Const.Report;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.PharmacyPage)
                        {
                            From = From + Const.Report;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.AppointmentPage)
                        {
                            From = From + Const.Report;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if(currPage==Const.AdminPage)
                        {
                            From = From + Const.Report;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.MasterPage)
                        {
                            From = From + Const.Report;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                    }
                        if (currRole[0] == Const.Doctor)
                    {
                        masterRole = currRole[0].ToString();
                        UA.userInRole = masterRole;
                        // hdfUserRole.Value = masterRole;
                        if (currPage == Const.PatientPage)
                        {
                            From = From + Const.Doctor;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.TokenPage)
                        {
                            From = From + Const.Doctor;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.PharmacyPage)
                        {
                            From = From + Const.Doctor;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.StockPage)
                        {
                            From = From + Const.Doctor;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.ReportPageURL)
                        {
                            From = From + Const.Doctor;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if (currPage == Const.MasterPage)
                        {
                            From = From + Const.Doctor;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                        if(currPage==Const.AdminPage)
                        {
                            From = From + Const.Doctor;
                            redirectURL = Const.AccessDeniedURL + From;
                            if (redirectURL != "") { Response.Redirect(redirectURL, true); }
                        }
                    }
            }



                //hdfUserRole.Value = masterRole;
                ////if (redirectURL != "")
                ////{
                ////    redirectURL = Const.DefaultPage(currRole);
                
                ////    if (!redirectURL.Contains("/" + currPage))
                ////    {
                ////        Response.Redirect(redirectURL, true);
                ////    }


                ////}

            }
            catch (Exception ex)
            {

             //   Response.Redirect(Const.AccessDeniedURL);
            }
        }
    }
}