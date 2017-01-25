using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheClinicApp1._1.UIClasses
{
    public class common
    {
    }
    public class Const
    {
        //   public string LoginSession = "LoginDetails";



        public string LoginSession
        {
            get
            {
                return "LoginDetails";

            }
        }
        public string LogoutSession
        {
            get
            {
                return "logout";

            }
        }


        public string HomePage
        {
            get
            {
                return "../Registration/Patients.aspx";
            }
        }

        public string LoginPage
        {
            get
            {
                return "Default.aspx";
            }
        }

        public string HomePageURL
        {
            get
            {
                return "~/Home.aspx";
            }
        }

        public string LoginPageURL
        {
            get
            {
                return "~/Login/Login.aspx";
            }
        }

      
         public string ReportPageURL
        {
            get
            {
                return "ReportsList.aspx";
            }
        }



        public string PatientPage
        {
            get
            {
                return "Patients.aspx";
            }
        }

        public string Report
        {
            get
            {
                return "Report";
            }
        }

        public string ReportPage
        {
            get
            {
                return "ReportsList.aspx";
            }
        }

        public string TokenPage
        {
            get
            {
                return "Tokens.aspx";
            }
        }


        public string DoctorPage
        {
            get
            {
                return "Doctors.aspx";
            }
        }


        public string PharmacyPage
        {
            get
            {
                return "Pharmacy.aspx";
            }
        }


        public string StockPage
        {
            get
            {
                return "Stock.aspx";
            }
        }


        public string AdminPage
        {
            get
            {
                return "Admin.aspx";
            }
        }
        public string MasterPage
        {
            get
            {
                return "Categories.aspx";
            }
        }
        public string AdminPageUrl
        {
            get
            {
                return "/Admin/Admin.aspx";
            }
        }
        public string AssRolePage
        {
            get
            {
                return "/Admin/AssignRoles.aspx";
            }
        }
        public string Patient
        {
            get
            {
                return "patients";
            }
        }


        public string Token
        {
            get
            {
                return "token";
            }
        }


        public string Doctor
        {
            get
            {
                return "Doctor";
            }
        }

        public string AppointmentPage
        {
            get
            {
                return "Appointment.aspx";
            }
        }
        public string Pharmacy
        {
            get
            {
                return "Pharmacist";
            }
        }


        public string Stock
        {
            get
            {
                return "stock";
            }
        }


        public string Admin
        {
            get
            {
                return "admin";
            }
        }
        public string Receptionist
        {
            get
            {
                return "Receptionist";
            }
        }
       public string StockRole
        {
            get
            {
                return "Stock";
            }
        }

        public string RoleDoctor
        {
            get
            {
                return "Doctor";
            }
        }

        public string RoleAdministrator
        {
            get
            {
                return "Administrator";
            }
        }
        public string RoleSadmin
        {
            get
            {
                return "SuperAdmin";
            }
        }

        public string RolePharmacist
        {
            get
            {
                return "Pharmacist";
            }
        }

        public string AccessDenied
        {
            get
            {
                return "AccessDenied.aspx";
            }
        }

        public string AccessDeniedURL
        {
            get
            {
                return "../Login/AccessDenied.aspx";
            }
        }



        //----------------* Messages For Mobile App *--------------//
        public string NoItems
        {
            get
            {
                return "No Results Found."; 
            }
        }


        public string DefaultPage(List<string> currRole)
        {
            string redirectURL = "";
            if (currRole.Contains(Doctor))
            {
                redirectURL ="../Doctor/"+ DoctorPage;
            }
            else if(currRole.Contains(RoleAdministrator))
            {
                redirectURL = AdminPageUrl;
            }
            else if(currRole.Contains(Receptionist))
            {
                redirectURL = "../Appointment/" + AppointmentPage;
            }
            else if(currRole.Contains(RolePharmacist))
            {
                redirectURL = "../Pharmacy/" + PharmacyPage;
            }
            else if(currRole.Contains(StockRole))
            {
                redirectURL ="../Stock/"+ StockPage;
            }
            else if(currRole.Contains(Report))
            {
                redirectURL ="../Report/"+ ReportPage;
            }
            else
            {
                redirectURL = AccessDeniedURL;
            }
            return redirectURL;
        }

        public string GetCurrentPageName(HttpRequest Request )
        {
            string sPath = Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }


    }
}